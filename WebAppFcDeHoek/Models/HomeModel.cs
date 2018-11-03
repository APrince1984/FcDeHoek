using System.Collections.Generic;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Structs;

namespace WebAppFcDeHoek.Models
{
    public class HomeModel
    {
        public string TeamName = "FC De Hoek";
        public string ConvenantName = "P.E.R.K.E.Z.";
        public string SportPark = "De Shorre";
        public string Region = "Oostende";
        public string TimePeriod = "zondagvoormiddag";
        public string CurrentDivision { get; set; }
        public Game NextGame { get; set; }
        public Game PreviousGame { get; set; }
        public Team NextGameHomeTeam { get; set; }
        public Team NextGameAwayTeam { get; set; }
        public Team PreviousGameHomeTeam { get; set; }
        public Team PreviousGameAwayTeam { get; set; }
        
        public List<Game> NextGames { get; set; }
        
        public int PreviousGameResult { get; set; }

        public List<Team> AllTeams { get; set; }

    }
}