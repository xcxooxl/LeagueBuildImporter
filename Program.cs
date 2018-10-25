using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuildImporter
{
    internal class Program
    {
        private static LolApi _lolApi;

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Connecting to league of legends..");
            _lolApi = new LolApi();
            await _lolApi.Init();
            Console.WriteLine("Waiting for champion lock..");
            while (true)
            {
                var champ = await _lolApi.GetCurrentChampion();
                if (champ != null)
                {
                    var role = (await _lolApi.GetCurrentRole()).ToLower();
                    role = role == "utility" ? "support" : role;
                    Console.WriteLine($"Found champion: {champ.name}, Role: {role}");

                    var builds = await _lolApi.GetBuildsFromChamp(champ, role);

                    var validatedBuilds = builds.Select(x => new {build = x, buildJson = _lolApi.PrepareRuneBuild(x)})
                        .Where(x => x.buildJson != null).ToList();
                    var buildIndex = PickBuild(validatedBuilds.Select(x => x.build).ToList());
                    var selectedBuild = validatedBuilds[buildIndex];
                    _lolApi.AddRuneBuild(selectedBuild.buildJson);
                    await _lolApi.AddItemsBuild(selectedBuild.build);
                    Process.Start(selectedBuild.build.Url);
                    break;
                }

                Thread.Sleep(500);
            }

            //GetBuilds();
            Console.ReadLine();
        }

        public static async Task GetBuildsAsync()
        {
            Console.WriteLine("Which build do you wish to import?");
            var url = Console.ReadLine();
            var champBuilds =
                await _lolApi.GetBuildsFromUrl(url);

            var buildIndex = PickBuild(champBuilds);

            var selectedBuild = champBuilds[buildIndex];
            await _lolApi.AddItemsBuild(selectedBuild);
            var runeBuildJson = _lolApi.PrepareRuneBuild(selectedBuild);
            _lolApi.AddRuneBuild(runeBuildJson);
            Console.WriteLine("Rune page added !");
        }

        public static int PickBuild(List<ChampionBuild> champBuilds)
        {
            Console.WriteLine($"Found {champBuilds.Count} builds" +
                              (champBuilds.Count > 1 ? ", Which one do you want?" : ""));
            var buildIndex = 0;
            if (champBuilds.Count > 1)
            {
                for (var i = 0; i < champBuilds.Count; i++)
                {
                    var champBuild = champBuilds[i];
                    Console.WriteLine($"{i}. {champBuild.Name}");
                }
                var selectedBuildIndex = int.TryParse(Console.ReadLine(), out buildIndex);
                if (!selectedBuildIndex)
                    Console.WriteLine("You are an idiot.");
            }

            Console.WriteLine($"Importing build: {champBuilds[buildIndex].Name}");
            return buildIndex;
        }
    }
}