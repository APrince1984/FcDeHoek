namespace WebAppFcDeHoek.Models
{
    public class StatisticsModel
    {
        public string SeasonDescription { get; set; }
        public int IdSeason { get; set; }

        public int FriendlyGamesPlayed { get; set; }
        public int GoalsInFriendly { get; set; }
        public int AssistsInFriendly { get; set; }

        public int CupGamesPlayed { get; set; }
        public int GoalsInCup { get; set; }
        public int AssistsInCup { get; set; }

        public int LeagueGamesPlayed { get; set; }
        public int GoalsInLeague { get; set; }
        public int AssistsInLeague { get; set; }

        public int TotalGamesPlayed { get; set; }
        public int TotalGoalsScored { get; set; }
        public int TotalAssists { get; set; }
    }
}