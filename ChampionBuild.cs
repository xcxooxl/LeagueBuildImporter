using System.Collections.Generic;

namespace BuildImporter
{
    public class ChampionBuild
    {
        public List<string> Runes { get; set; }
        public string Name { get; set; }
        public List<ItemsBuild> ItemBuilds { get; set; }
        public Champion Champion { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ItemsBuild
    {
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }
}