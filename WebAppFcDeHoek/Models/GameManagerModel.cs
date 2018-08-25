using System;
using System.Collections.Generic;

namespace WebAppFcDeHoek.Models
{
    public class GameManagerModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<GameModel> Games { get; set; }
    }
}