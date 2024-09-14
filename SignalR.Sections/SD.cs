namespace SignalR.Sections
{
    public static class SD
    {
        public const string Wand = nameof(Wand);
        public const string Stone = nameof(Stone);
        public const string Cloak = nameof(Cloak);

        public static Dictionary<string, int> DealthyHallowRace = new Dictionary<string, int>
        {
            { Wand, 0 },
            { Stone, 0 },
            { Cloak, 0 }
        };

        public const string Gryffindor = nameof(Gryffindor);
        public const string Slytherin = nameof(Slytherin);
        public const string Hufflepuff = nameof(Hufflepuff);
        public const string Ravenclaw = nameof(Ravenclaw);
    }
}
