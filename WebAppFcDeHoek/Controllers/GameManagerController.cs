using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Models;

namespace WebAppFcDeHoek.Controllers
{
    public class GameManagerController : Controller
    {
        // GET: GameManager
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                var date = DateTime.Now;
                startDate = GetFirstDayOfWeek(date, CultureInfo.CurrentCulture);
                endDate = ((DateTime)startDate).AddDays(7);
            }
            
            return View(new GameManagerModel{StartDate = (DateTime) startDate, EndDate = (DateTime) endDate, Games = GetGames((DateTime)startDate, (DateTime)endDate)});
        }

        private List<GameModel> GetGames(DateTime startDate, DateTime endDate)
        {
            using (var context = new FcDeHoekContext())
            {
                var gameModels = new List<GameModel>();
                var games = GameQueries.GetAllGamesBetweenDates(context, startDate, endDate).ToList();
                foreach (var game in games)
                {
                    //var season = SeasonQueries.GetSeasonByDate(context, startDate);
                    gameModels.Add(new GameModel
                    {
                        IdGame = game.IdGame,
                        AwayTeam = game.GameAwayTeam.Name,
                        Competition = game.GameCompetition.Description,
                        HomeTeam = game.GameHomeTeam.Name,
                        MatchDay = game.MatchDate,
                        IdAwayTeam = game.IdAwayTeam,
                        IdHomeTeam = game.IdHomeTeam,
                        IdCompetition = game.IdCompetition,
                        IdSeason = game.IdSeason,
                        IsPostPoned = game.NotPlayed,
                        Forfait = game.Forfait,
                        IdPostPonedGame = game.IdPostPonedGame
                    });
                }

                return gameModels;
            }
            
            
            
        }

        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }


    }
}