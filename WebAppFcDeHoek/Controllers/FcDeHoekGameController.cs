using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Models;

namespace WebAppFcDeHoek.Controllers
{
    public class FcDeHoekGameController : Controller
    {
        // GET: FcDeHoekGame
        public ActionResult FcDeHoekGameDetail(int? idGame)
        {
            using (var context = new FcDeHoekContext())
            {
                Game game = null;
                if (idGame == null)
                    game = GameQueries.GetPreviousGame(context);
                else
                    game = GameQueries.GetById(context, (int) idGame);

                var fcDeHoekGame = GetGameModel(context, game);
                return View("FcDeHoekGameDetail", fcDeHoekGame);
            }
            
        }

        [HttpPost]
        public ActionResult SaveFcDeHoekGame(FcDeHoekGameModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                var statsToRemove = context.PersonStatistics.Where(ps => ps.IdGame == model.IdGame).ToList();
                if (statsToRemove.Any())
                {
                    context.PersonStatistics.RemoveRange(statsToRemove);
                    context.SaveChanges();
                }
               
                foreach (var player in model.PlayersInGame)
                {
                    if (player.IdPlayer != -1)
                    {
                        var stat = new PersonStats
                        {
                            IdGame = model.IdGame,
                            IdPerson = player.IdPlayer,
                            Assists = player.AssistsGiven,
                            Goals = player.GoalsScored,
                            Played = true
                        };
                        context.PersonStatistics.AddOrUpdate(stat);
                        context.SaveChanges();
                    }
                }
                
                return FcDeHoekGameDetail(model.IdGame);
            }
           
        }

        private object GetGameModel(FcDeHoekContext context, Game game)
        {
            var model = new FcDeHoekGameModel();

            model.MatchDate = game.MatchDate;
            model.IdGame = game.IdGame;
            model.GameFixture = $"{game.GameHomeTeam.Name} - {game.GameAwayTeam.Name}";
            model.GoalsHome = game.GoalsHomeTeam ?? 0;
            model.GoalsAway = game.GoalsAwayTeam ?? 0;

            model.AllGames = GetAllFcDeHoekGames(context, game.MatchDate);

            var players = PersonQueries.GetPlayers(context).ToList();
            model.AllPlayers = SetAllPlayers(players);
            model.PlayersInGame = GetPlayersInGame(context, players, game.IdGame);

            return model;
        }

        private List<FcDeHoekGame> GetAllFcDeHoekGames(FcDeHoekContext context, DateTime matchDate)
        {
            var gameModelList = new List<FcDeHoekGame>();
            var season = SeasonQueries.GetSeasonByDate(context, matchDate) ?? SeasonQueries.GetCurrentSeason(context);
            var games = GameQueries.GetAllGamesByIdSeasonAndIdTeam(context, season.IdSeason, 1).ToList();
            
            foreach (var game in games)
            {
                gameModelList.Add(new FcDeHoekGame
                {
                    IdGame = game.IdGame,
                    MatchDate = game.MatchDate,
                    GameFixture = $"{game.GameHomeTeam.Name} - {game.GameAwayTeam.Name}"
                });
            }

            return gameModelList;

        }

        private List<Player> GetPlayersInGame(FcDeHoekContext context, List<Person> players, int idGame)
        {
            var playerModel = new List<Player>();
            playerModel.Add(new Player
            {
                IdPlayer = -1,
                PlayerFullName = "Choose Player",
                AssistsGiven = 0,
                GoalsScored = 0
            });
            foreach (var player in players)
            {
                var stats = PersonStatisticsQueries.GetByIdPersonAndIdGame(context, player.IdPerson, idGame);
                if (stats != null)
                {
                    playerModel.Add(new Player
                    {
                        IdPlayer = stats.IdPerson,
                        PlayerFullName = $"{player.Name} {player.FirstName}",
                        GoalsScored = stats.Goals ?? 0,
                        AssistsGiven = stats.Assists
                    });
                }
            }

            return playerModel;
        }

        private List<Player> SetAllPlayers(List<Person> players)
        {
            var modelList = new List<Player>();
            modelList.Add(new Player
            {
                IdPlayer = -1,
                PlayerFullName = "Choose Player",
                AssistsGiven = 0,
                GoalsScored = 0
            });
            foreach (var player in players)
            {
                modelList.Add(new Player
                {
                    IdPlayer = player.IdPerson,
                    PlayerFullName = $"{player.Name} {player.FirstName}",
                    GoalsScored =  0,
                    AssistsGiven = 0
                });
            }

            return modelList;
        }
    }
}