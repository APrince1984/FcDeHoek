using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Models;

namespace WebAppFcDeHoek.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new FcDeHoekContext())
            {
                var currentSeason = SeasonQueries.GetCurrentSeason(context);
                var calendars = CalendarQueries.GetCalendarsByIdSeason(context, currentSeason.IdSeason).OrderBy(c => c.MatchDay).ToList();
                var model = new CalendarModel
                {
                    SeasonDescription =
                        $"{calendars.FirstOrDefault()?.CalendarSeason.SeasonStartYear} - {calendars.FirstOrDefault()?.CalendarSeason.SeasonEndYear}",
                    Games = GetGames(context, calendars)
                };
                return View(model);
            }

        }

        private List<GameModel> GetGames(FcDeHoekContext context, List<Calendar> calendars)
        {
            var games = new List<GameModel>();
            var nextGame = CalendarQueries.GetNextCalendarGame(context);
            foreach (var calendar in calendars)
            {
                games.Add(new GameModel
                {
                    Competition = calendar.CalendarCompetition.Description,
                    MatchDay = calendar.MatchDay.Date,
                    HomeTeam = calendar.CalendarHomeTeam.Name,
                    AwayTeam = calendar.CalendarAwayTeam.Name,
                    Result = calendar.Postponed
                        ? "Afgelast"
                        : !calendar.Forfait
                            ? calendar.GoalsHomeTeam != null
                                ? $"{calendar.GoalsHomeTeam} - {calendar.GoalsAwayTeam}"
                                : " - "
                            : calendar.GoalsHomeTeam != null
                                ? $"{calendar.GoalsHomeTeam} - {calendar.GoalsAwayTeam} (F.F.)"
                                : "F.F.",
                    IsNextGame = calendar.IdCalendar == nextGame.IdCalendar
                });
            }

            return games;
        }
    }
}