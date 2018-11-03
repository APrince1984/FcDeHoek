using System;
using System.Collections.Generic;

namespace WebAppFcDeHoek.Models
{
    public class FullCalendarModel
    {
        public string SeasonDescription { get; set; }

        public Dictionary<DateTime, List<GameModel>> GamesPerDate { get; set; }
       
    }
}