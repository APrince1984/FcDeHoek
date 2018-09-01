namespace WebAppFcDeHoek.Structs
{
    public struct eNavigation
    {
        public const int Login = 0;
        public const int LogOut = -1;

        public const int Home = 1;
        public const int Team = 2;
        public const int Ranking = 3;
        public const int Calendar = 4;
        public const int History = 5;
        public const int Links = 6;

        // admin manage functions

        public const int Editor = 7;
        public const int ManageTeams = 8;
        public const int ManageUsers = 9;
        public const int ManageGames = 10;
        public const int ManageScores = 11;
        public const int ManageSeasons = 12;
        public const int ManageHistory = 13;
        public const int ManagePersons = 14;
        public const int EditFcDeHoekGame = 15;

        // team split
        public const int Board = 20;
        public const int Players = 21;

        // calendar split
        public const int GamesDeHoek = 30;
        public const int AllGames = 31;

        // scores 
        public const int EditScores = 40;

        public static string GetNavigationDescription(int navigation)
        {
            switch (navigation)
            {
                case Login:
                    return "Login";
                case LogOut:
                    return "Log Out";
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
                case Editor:
                    return "Manage";
                case ManageTeams:
                    return "Teams";
                case ManageScores:
                    return "Scores";
                case ManageUsers:
                    return "Users";
                case ManageGames:
                    return "Games";
                case ManageHistory:
                    return "History";
                case ManageSeasons:
                    return "Seasons";
                case Board:
                    return "Board";
                case Players:
                    return "Players";
                case GamesDeHoek:
                    return "Games De Hoek";
                case AllGames:
                    return "Full Calendar";
                case EditScores:
                    return "Scores";
                case ManagePersons:
                    return "Persons";
                default:
                    return "Page Title Not found";
            }
        }
    }
}