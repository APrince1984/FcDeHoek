using System.Collections.Generic;

namespace WebAppFcDeHoek.Models
{
    public class RankingModel
    {
        public string Season { get; set; }

        public List<RankModel> Ranking { get; set; }
    }
}