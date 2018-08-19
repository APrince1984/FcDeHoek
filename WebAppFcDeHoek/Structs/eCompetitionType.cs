namespace WebAppFcDeHoek.Structs
{
    public struct eCompetitionType
    {
        public const int League = 2;
        public const int Cup = 3;
        public const int Friendly = 4;

        public static string GetCompetitionDescription(int competitionType)
        {
            switch (competitionType)
            {
                case League:
                    return "League";
                case Cup:
                    return "Cup";
                default:
                    return "Friendly";
            }
        }
    }
}