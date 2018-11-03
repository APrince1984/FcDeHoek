using System;
using System.Collections.Generic;

namespace WebAppFcDeHoek.Models
{
    public class ScoreModel
    {
        public DateTime FixtureDate { get; set; }
        public List<GameModel> Games { get; set; }
        public List<DateTime> AllFixtureDates { get; set; }
    }
}