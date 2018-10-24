namespace BuildImporter
{
    public class Rune
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Rune(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}   