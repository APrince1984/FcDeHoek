using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Extensions;
using WebAppFcDeHoek.Models;

namespace WebAppFcDeHoek.Controllers
{
    public class ScoreController : Controller
    {
        // GET: Score
        public ActionResult EditScores(DateTime? fixtureDate = null)
        {
            return View(GetScores(fixtureDate));
        }

        [HttpPost]
        public ActionResult Save(ScoreModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                foreach (var gameModel in model.Games)
                {
                    var game = GameQueries.GetById(context, gameModel.IdGame);
                    game.GoalsHomeTeam = gameModel.GoalsHomeTeam;
                    game.GoalsAwayTeam = gameModel.GoalsAwayTeam;
                    context.Games.AddOrUpdate(game);
                    context.SaveChanges();
                }

                return RedirectToAction("EditScores", new {fixtureDate = model.FixtureDate});
            }
        }

        private ScoreModel GetScores(DateTime? fixtureDate)
        {
            using (var context = new FcDeHoekContext())
            {
                if (fixtureDate == null)
                {
                    fixtureDate = DateTime.Now.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
                }

                var allGames = GameQueries.GetAllGamesByIdSeason(context, SeasonQueries.GetSeasonByDate(context, (DateTime) fixtureDate)?.IdSeason ?? SeasonQueries.GetCurrentSeason(context).IdSeason).OrderBy(g => g.MatchDate).ToList();
                var allPlayWeeks = new List<DateTime>();
                foreach (var game in allGames)
                {
                    if (!allPlayWeeks.Contains(game.MatchDate))
                        allPlayWeeks.Add(game.MatchDate);
                }

                var model = new ScoreModel
                {
                    AllFixtureDates = allPlayWeeks,
                    Games = GetGameModels(context, (DateTime) fixtureDate)
                };

                model.FixtureDate = model.Games.FirstOrDefault()?.MatchDay ?? DateTime.Now;

                return model;
            }
            
        }

        private List<GameModel> GetGameModels(FcDeHoekContext context, DateTime fixtureDate)
        {
            var gameModels = new List<GameModel>();
            var gamesToProcess = GameQueries.GetAllGamesBetweenDates(context, fixtureDate.AddDays(-3), fixtureDate.AddDays(4)).ToList();
            foreach (var game in gamesToProcess)
            {
                gameModels.Add(new GameModel
                {
                    AwayTeam = game.GameAwayTeam.Name,
                    Competition = game.GameCompetition.Description,
                    Forfait = game.Forfait,
                    GoalsAwayTeam = game.GoalsAwayTeam,
                    GoalsHomeTeam = game.GoalsHomeTeam,
                    HomeTeam = game.GameHomeTeam.Name,
                    IdHomeTeam = game.IdHomeTeam,
                    IdAwayTeam = game.IdAwayTeam,
                    IdCompetition = game.IdCompetition,
                    IdGame = game.IdGame,
                    IsPostPoned = game.NotPlayed,
                    MatchDay = game.MatchDate
                });
            }

            return gameModels;
        }
    }
}