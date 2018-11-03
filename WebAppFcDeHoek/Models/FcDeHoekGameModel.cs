
using System;
using System.Collections.Generic;

namespace WebAppFcDeHoek.Models
{
    public class Player
    {
        public int IdPlayer { get; set; }
        public string PlayerFullName { get; set; }
        public int GoalsScored { get; set; }
        public int AssistsGiven { get; set; }
    }

    public class FcDeHoekGame
    {
        public int IdGame { get; set; }
        public DateTime MatchDate { get; set; }
        public string GameFixture { get; set; }
    }

    public class FcDeHoekGameModel
    {
        public List<FcDeHoekGame> AllGames { get; set; }
        public int IdGame { get; set; }
        public DateTime MatchDate { get; set; }
        public string GameFixture { get; set; }
        public int GoalsHome { get; set; }
        public int GoalsAway { get; set; }

        public List<Player> AllPlayers { get; set; }
        public List<Player> PlayersInGame { get; set; }
    }
}