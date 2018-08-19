namespace WebAppFcDeHoek.Structs
{
    public struct eNavigation
    {
        public const int Home = 1;
        public const int Team = 2;
        public const int Ranking = 3;
        public const int Calendar = 4;
        public const int History = 5;
        public const int Links = 6;

        public static string GetNavigationDescription(int navigation)
        {
            switch (navigation)
            {
                case Home:
                    return "Home";
                case Team:
                    return "Team";
                case Ranking:
                    return "Ranking";
                case Calendar:
                    return "Calendar";
                case History:
                    return "History";
                case Links:
                    return "Links";
                default:
                    return "Page Title Not found";
            }
        }
    }
}