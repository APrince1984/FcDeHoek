using System;

namespace WebAppFcDeHoek.Models
{
    public class GameModel
    {
        public string Competition { get; set; }
        public DateTime MatchDay { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Result { get; set; }
        public bool IsNextGame { get; set; }
    }
}