using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuildImporter
{
    internal class LolApi
    {
        private static string _token;
        private static int _port;

        public LolApi()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;

            Leagueconnect();
            Client = new HttpClient {BaseAddress = new Uri("https://127.0.0.1:" + _port)};
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(
                    $"riot:{_token}")));
        }

        public HttpClient Client { get; set; }

        public RuneCategory[] Categories { get; set; }
        public Dictionary<string, RunePerk> Perks { get; set; }
        public int SummonerId { get; set; }
        public Champion[] Champions { get; set; }
        public Dictionary<string, Item> Items { get; set; }

        public GuideParser parser { get; set; }

        public ItemSetRequest ItemSets { get; set; }

        public async Task Init()
        {
            await GetRunesData();
            await GetSummonerId();

            Champions = GetSummonerChampions(SummonerId).Result;
            Items = GetGameItems().Result;

            parser = new GuideParser();
            parser.SetItems(Items);
            parser.SetChampions(Champions);
        }

        private async Task<ItemSetRequest> GetItemSets()
        {
            var response = await Client.GetStringAsync($"/lol-item-sets/v1/item-sets/{SummonerId}/sets");
            return JsonConvert.DeserializeObject<ItemSetRequest>(response);
        }


        public async Task<Dictionary<string, Item>> GetGameItems()
        {
            var client = new HttpClient();
            var itemsJson =
                await client.GetStringAsync("https://ddragon.leagueoflegends.com/cdn/8.19.1/data/en_US/item.json");
            var itemsData = JObject.Parse(itemsJson)["data"];
            var items = new Dictionary<string, Item>();

            foreach (JProperty itemData in itemsData)
            {
                var itemName = itemData.Value["name"].Value<string>();

                var item = new Item
                {
                    id = itemData.Name,
                    Name = itemName
                };
                if (!items.ContainsKey(item.Name))
                    items.Add(item.Name, item);
            }

            return items;
        }

        private async Task GetSummonerId()
        {
            var response = await Client.GetStringAsync("/lol-summoner/v1/current-summoner");
            var summmoner = JsonConvert.DeserializeObject<Summoner>(response);
            SummonerId = summmoner.summonerId;
        }

        public async Task<Champion[]> GetSummonerChampions(int summonerId)
        {
            var response = await Client.GetStringAsync($"/lol-champions/v1/inventories/{summonerId}/champions");
            return JsonConvert.DeserializeObject<Champion[]>(response);
        }

        public async Task GetRunesData()
        {
            var runeCats = await Client.GetStringAsync("/lol-perks/v1/styles");
            var runePerks = await Client.GetStringAsync("/lol-perks/v1/perks");
            Categories = JsonConvert.DeserializeObject<RuneCategory[]>(runeCats);

            var perks = JsonConvert.DeserializeObject<RunePerk[]>(runePerks);
            Perks = perks.ToDictionary(x => x.name, x => x);
            Console.WriteLine($"Loaded {Perks.Count} Runes !");
        }

        public async Task<List<ChampionBuild>> GetBuildsFromChamp(Champion champ, string role = "all")
        {
            var builds = await parser.FindTopBuilds(champ.name, role);
            return builds.Where(x => x.Runes != null && x.Runes.Any()).ToList();
        }


        public async Task<List<ChampionBuild>> GetBuildsFromUrl(string url)
        {
            return await parser.GetBuilds(url);
        }

        public async Task<string> GetCurrentRole()
        {
            try
            {
                var result = await Client.GetAsync("/lol-champ-select/v1/session");
                var session = JsonConvert.DeserializeObject<Session>(await result.Content.ReadAsStringAsync());
                var myPosition = session.myTeam.FirstOrDefault(x => x.summonerId == SummonerId)?.assignedPosition;
                return myPosition;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get current session.");
            }
            return "all";
        }

        public async Task<Champion> GetCurrentChampion()
        {
            var result = await Client.GetAsync("/lol-champ-select/v1/current-champion");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var champIdString = await result.Content.ReadAsStringAsync();
                return Champions.FirstOrDefault(x => x.id == Convert.ToInt32(champIdString));
            }
            return null;
        }

        public async Task AddItemsBuild(ChampionBuild build)
        {
            var itemSet = new ItemSet(build);

            var itemSetRequest = await GetItemSets();
            itemSetRequest.itemSets.Add(itemSet);
            var json = JsonConvert.SerializeObject(itemSetRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync($"/lol-item-sets/v1/item-sets/{SummonerId}/sets", content);
        }

        public JObject PrepareRuneBuild(ChampionBuild champBuild)
        {
            var runes = champBuild.Runes.Where(x => x.Length > 0).ToList();
            var mainCat = Categories.FirstOrDefault(x => x.name == runes[0]);
            var subCat = Categories.FirstOrDefault(x => x.name == runes[1]);
            // Delete categories from runes array.
            runes.RemoveRange(0, 2);
            var perkIds = new List<int>();
            foreach (var rune in runes)
                if (Perks.ContainsKey(rune))
                    perkIds.Add(Perks[rune].id);
                else return null;

            var json = new JObject
            {
                {"name", champBuild.Name},
                {"primaryStyleId", mainCat.id},
                {"selectedPerkIds", new JArray(perkIds)},
                {"subStyleId", subCat.id}
            };

            return json;
        }

        public void AddRuneBuild(JObject json)
        {
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            Client.PostAsync("/lol-perks/v1/pages", content);
        }

        public static void Leagueconnect()
        {
            var process = Process.GetProcessesByName("LeagueClientUx");
            if (process.Length != 0)
                foreach (var getid in process)
                    using (var mos = new ManagementObjectSearcher(
                        "SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + getid.Id))
                    {
                        foreach (ManagementObject mo in mos.Get())
                            if (mo["CommandLine"] != null)
                            {
                                var data = mo["CommandLine"].ToString();
                                var CommandlineArray = data.Split('"');

                                foreach (var attributes in CommandlineArray)
                                {
                                    if (attributes.Contains("token") || attributes.Contains("remoting-auth-token"))
                                    {
                                        var tokens = attributes.Split('=');
                                        _token = tokens[1];
                                    }
                                    if (attributes.Contains("port") || attributes.Contains("app-port"))
                                    {
                                        var ports = attributes.Split('=');
                                        _port = int.Parse(ports[1]);
                                    }
                                }
                                return;
                            }
                    }
        }
    }
}