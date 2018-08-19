using System.Collections.Generic;

namespace WebAppFcDeHoek.Models
{
    public class CalendarModel
    {
        public string SeasonDescription { get; set; }
        public List<GameModel> Games { get; set; }
    }
}