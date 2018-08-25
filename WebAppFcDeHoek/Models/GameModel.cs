using System;

namespace WebAppFcDeHoek.Models
{
    public class GameModel
    {
        public int IdGame { get; set; }
        public string Competition { get; set; }
        public DateTime MatchDay { get; set; }
        public string HomeTeam { get; set; }
        public int IdHomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int IdAwayTeam { get; set; }
        public string Result { get; set; }
        public bool IsNextGame { get; set; }
        public int GameResult { get; set; }

        public bool IsPostPoned { get; set; }
        public bool Forfait { get; set; }
        public int IdSeason { get; set; }
        public int IdCompetition { get; set; }
        public int? IdPostPonedGame { get; set; }
    }
}