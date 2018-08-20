using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Models;
using WebAppFcDeHoek.Structs;

namespace WebAppFcDeHoek.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new FcDeHoekContext())
            {
                var currentSeason = SeasonQueries.GetCurrentSeason(context);
                var calendars = GameQueries.GetAllGamesByIdSeasonAndIdTeam(context, currentSeason.IdSeason, 1).OrderBy(g => g.MatchDate).ToList();
                var model = new CalendarModel
                {
                    SeasonDescription =
                        $"{currentSeason.SeasonStartYear} - {currentSeason.SeasonEndYear}",
                    Games = GetGames(context, calendars)
                };
                return View(model);
            }

        }

        private List<GameModel> GetGames(FcDeHoekContext context, List<Game> calendars)
        {
            var games = new List<GameModel>();
            var nextGame = GameQueries.GetNextGame(context);
            foreach (var calendar in calendars)
            {
                games.Add(new GameModel
                {
                    IdGame = calendar.IdGame,
                    Competition = calendar.GameCompetition.Description,
                    MatchDay = calendar.MatchDate.Date,
                    IdHomeTeam = calendar.IdHomeTeam,
                    HomeTeam = calendar.GameHomeTeam.Name,
                    AwayTeam = calendar.GameAwayTeam.Name,
                    IdAwayTeam = calendar.IdAwayTeam,
                    GameResult = calendar.IdHomeTeam == 1 ? eResult.GetResult(calendar.GoalsHomeTeam, calendar.GoalsAwayTeam) : eResult.GetResult(calendar.GoalsAwayTeam, calendar.GoalsHomeTeam),
                    Result = calendar.NotPlayed
                        ? "Afgelast"
                        : !calendar.Forfait
                            ? calendar.GoalsHomeTeam != null
                                ? $"{calendar.GoalsHomeTeam} - {calendar.GoalsAwayTeam}"
                                : " - "
                            : calendar.GoalsHomeTeam != null
                                ? $"{calendar.GoalsHomeTeam} - {calendar.GoalsAwayTeam} (F.F.)"
                                : "F.F.",
                    IsNextGame = calendar.IdGame == nextGame.IdGame
                });
            }

            return games;
        }
    }
}