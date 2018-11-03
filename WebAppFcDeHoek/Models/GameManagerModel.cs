using System;
using System.Collections.Generic;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Structs;

namespace WebAppFcDeHoek.Models
{
    public class GameManagerModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<GameModel> Games { get; set; }
        public List<Team> Teams { get; set; }
        public List<CompetitionType> CompetitionTypes { get; set; }
        public bool AddAnother { get; set; }
    }
}