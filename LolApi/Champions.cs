namespace BuildImporter
{
    public class Champion
    {
        public bool active { get; set; }
        public string alias { get; set; }
        public string banVoPath { get; set; }
        public bool botEnabled { get; set; }
        public string chooseVoPath { get; set; }
        public string[] disabledQueues { get; set; }
        public bool freeToPlay { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Ownership ownership { get; set; }
        public string purchased { get; set; }
        public bool rankedPlayEnabled { get; set; }
        public string[] roles { get; set; }
        public string squarePortraitPath { get; set; }
        public string stingerSfxPath { get; set; }
        public string title { get; set; }
    }

    public class Ownership
    {
        public bool freeToPlayReward { get; set; }
        public bool owned { get; set; }
        public Rental rental { get; set; }
    }

    public class Rental
    {
        public string endDate { get; set; }
        public string purchaseDate { get; set; }
        public bool rented { get; set; }
        public int winCountRemaining { get; set; }
    }
}