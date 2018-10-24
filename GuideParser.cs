using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BuildImporter
{
    class GuideParser
    {
        public string Url { get; set; }

        public GuideParser()
        {
        }

        public async Task<List<ChampionBuild>> FindTopBuilds(string champName,string role = "all")
        {
            var client = new HttpClient();
            var doc = new HtmlDocument();
            var resultsHtml = await client.GetStringAsync($"https://www.mobafire.com/league-of-legends/browse?champion={WebUtility.UrlEncode(champName)}&role=all&category={role}&depth=guide&sort=top&order=descending&author=all&page=1");
            doc.LoadHtml(resultsHtml);
            var buildLinks = doc.DocumentNode.Descendants().Where(x => x.HasClass("browse-list__item")).Take(3)
                .Select(x => x.Attributes["href"].Value);

            var tasks = new List<Task<List<ChampionBuild>>>();

            foreach (var buildLink in buildLinks)
            {
                tasks.Add(Task.Run(() =>
                {
                    async Task<List<ChampionBuild>> Temp()
                    {
                        List<ChampionBuild> builds = null;
                        if (buildLink != null)
                        {
                            builds = await GetBuilds($"https://www.mobafire.com{buildLink}");
                        }
                        else
                            Console.WriteLine("Cant find builds for this champion.");
                        return builds;
                    }

                    return Temp();
                }));
            }
            await Task.WhenAll(tasks);
            return tasks.SelectMany(x => x.Result).ToList();
        }

        private List<string> GetRunes(HtmlNode buildNode)
        {
            var runes = new List<string>();
            var primaryRunesElements =
                buildNode.Descendants().FirstOrDefault(x => x.HasClass("new-runes__primary"))?.ChildNodes;

            var secondaryRunesElements =
                buildNode.Descendants().FirstOrDefault(x => x.HasClass("new-runes__secondary"))?.ChildNodes;

            var runeCategories = buildNode.Descendants().Where(x => x.HasClass("new-runes__title")).Take(2)
                .Select(x => x.InnerText).ToList();

            runes.AddRange(runeCategories);
            if (primaryRunesElements != null)
                foreach (var primaryRunesElement in primaryRunesElements)
                {
                    var runeName = primaryRunesElement.InnerText.Replace("\t", "").Replace("\n", "");
                    ;
                    runes.Add(runeName);
                }


            if (secondaryRunesElements != null)
                foreach (var secondaryRunesElement in secondaryRunesElements)
                {
                    var runeName = secondaryRunesElement.InnerText.Replace("\t", "").Replace("\n", "");
                    ;
                    runes.Add(runeName);
                }

            return runes;
        }

        private List<ItemsBuild> GetItems(HtmlNode buildNode)
        {
            var itemBuilds = new List<ItemsBuild>();
            var itemGroups = buildNode.Descendants().Where(x => x.HasClass("item-wrap"));

            foreach (var itemGroup in itemGroups)
            {
                var itemBuild = new ItemsBuild();
                var title = itemGroup.ChildNodes.FirstOrDefault(x => x.Name == "h2")?.InnerText;
                itemBuild.Name = WebUtility.HtmlDecode(title?.Replace("\t", "").Replace("\n", ""));

                var itemNames = itemGroup.Descendants()
                    .Where(x => x.HasClass("item-title"))?
                    .Select(x => WebUtility.HtmlDecode(x.InnerText.Replace("\t", "").Replace("\n", "")));

                List<Item> foundItems = new List<Item>();
                foreach (var itemName in itemNames)
                {
                    if (this.items.ContainsKey(itemName))
                    {
                        foundItems.Add(items[itemName]);
                    }
                    else Console.WriteLine($"Failed to find item: {itemName}");
                }

                itemBuild.Items = foundItems;
                itemBuilds.Add(itemBuild);
            }

            return itemBuilds;
        }

        private string GetBuildTitle(HtmlNode buildNode)
        {
           var title = buildNode.Descendants().FirstOrDefault(x => x.HasClass("build-title"))
                ?.ChildNodes
                .FirstOrDefault(x => x.Name == "h2")
                ?.InnerText;

            return WebUtility.HtmlDecode(title?.Replace("\t", "").Replace("\n", ""));
        }

        public Champion GetChamp(HtmlNode htmlNode)
        {
            var stickyMenu = htmlNode.OwnerDocument.GetElementbyId("scroll-follower-container");
            var champName = stickyMenu?.Descendants().FirstOrDefault(x => x.HasClass("title"))?
                .Descendants().FirstOrDefault(x => x.Name == "h3")?.InnerText.Replace("\t", "").Replace("\n", "");

            champName = WebUtility.HtmlDecode(champName);
            return this.champions.FirstOrDefault(x => x.name == champName);
        }


        public async Task<List<ChampionBuild>> GetBuilds(string url)
        {
            var builds = new List<ChampionBuild>();

            HttpClient client = new HttpClient();
            var pageSource = await client.GetStringAsync(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);

            var buildNodes = doc.DocumentNode.Descendants().Where(x => x.HasClass("build-gradient"));
            var Champ = GetChamp(doc.DocumentNode);

            foreach (var buildNode in buildNodes)
            {
                var runes = GetRunes(buildNode);
                var title = GetBuildTitle(buildNode);
                var items = GetItems(buildNode);

                var build = new ChampionBuild
                {
                    Runes = runes,
                    Name = title,
                    ItemBuilds = items,
                    Champion = Champ,
                    Url = url
                };

                builds.Add(build);
            }
            return builds;
        }

        public void SetItems(Dictionary<string, Item> items)
        {
            this.items = items;
        }

        public Dictionary<string, Item> items { get; set; }

        public void SetChampions(Champion[] champions)
        {
            this.champions = champions;
        }

        public Champion[] champions { get; set; }
    }
}
