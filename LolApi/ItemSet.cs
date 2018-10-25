using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuildImporter
{
    public class ItemSetRequest
    {
        [JsonIgnore]
        public int accountId { get; set; }

        public List<ItemSet> itemSets { get; set; }

        [JsonIgnore]
        public long timestamp { get; set; }
    }


    public class ItemSet
    {
        public ItemSet()
        {
        }

        public ItemSet(ChampionBuild build)
        {
            var buildBlocks = new List<Block>();
            foreach (var itemBuild in build.ItemBuilds)
            {
                var block = new Block
                {
                    type = itemBuild.Name,
                    items = itemBuild.Items.ToArray()
                };
                buildBlocks.Add(block);
            }
            // Summoner Rift ? 
            associatedMaps = new int[0];
            associatedChampions = new[] {build.Champion.id};
            map = "";
            mode = "any";
            type = "custom";
            startedFrom = "blank";
            preferredItemSlots = new Preferreditemslot[0];
            blocks = buildBlocks.ToArray();
            title = build.Name;
            uid = Guid.NewGuid().ToString();
        }

        public int[] associatedChampions { get; set; }
        public int[] associatedMaps { get; set; }
        public Block[] blocks { get; set; }
        public string map { get; set; }
        public string mode { get; set; }
        public Preferreditemslot[] preferredItemSlots { get; set; }
        public int sortrank { get; set; }
        public string startedFrom { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string uid { get; set; }
    }

    public class Block
    {
        public string hideIfSummonerSpell { get; set; } = "";
        public Item[] items { get; set; }
        public string showIfSummonerSpell { get; set; } = "";
        public string type { get; set; }
    }

    public class Item
    {
        public int count { get; set; } = 1;
        public string id { get; set; }

        [JsonIgnore]
        public string Name { get; set; }
    }

    public class Preferreditemslot
    {
        public string id { get; set; }
        public int preferredItemSlot { get; set; }
    }
}