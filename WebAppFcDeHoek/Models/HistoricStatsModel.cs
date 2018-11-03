
using System.Collections.Generic;

namespace WebAppFcDeHoek.Models
{
    public class HistoricPlayer
    {
        public int IdPlayer { get; set; }
        public string Player { get; set; }
    }

    public class HistoricSeason
    {
        public int IdSeason { get; set; }
        public string Season { get; set; }
    }

    public class HistoricStatsModel
    {
        public int IdHistoricStats { get; set; }
        public int IdSeason { get; set; }
        public string Season { get; set; }
        public int IdPlayer { get; set; }
        public string Player { get; set; }
        public int? Goals { get; set; }
        public int? Assists { get; set; }
        public int? Penalties { get; set; }

        public List<HistoricPlayer> AllPlayers { get; set; }

        public List<HistoricSeason> AllSeasons { get; set; }
    }
}