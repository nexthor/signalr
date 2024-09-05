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
    }
}
