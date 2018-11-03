using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Extensions;
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
                    Games = GetGameModels(context, calendars)
                };
                return View(model);
            }

        }

        public ActionResult AllGames()
        {
            using (var context = new FcDeHoekContext())
            {
                var currentSeason = SeasonQueries.GetCurrentSeason(context);
                var calendars = GameQueries.GetAllGamesByIdSeason(context, currentSeason.IdSeason).OrderBy(g => g.MatchDate).ToList();
                var model = new FullCalendarModel
                {
                    SeasonDescription =
                        $"{currentSeason.SeasonStartYear} - {currentSeason.SeasonEndYear}",
                    GamesPerDate = GetGamesPerDate(context, calendars)
                };
                return View(model);
            }

        }

        private Dictionary<DateTime, List<GameModel>> GetGamesPerDate(FcDeHoekContext context, List<Game> calendars)
        {
            var dictionary = new Dictionary<DateTime, List<GameModel>>();
            var currentSeason = SeasonQueries.GetCurrentSeason(context);
            var gamesDeHoek = GameQueries.GetAllGamesByIdSeasonAndIdTeam(context, currentSeason.IdSeason, 1).OrderBy(g => g.MatchDate).ToList();
            foreach (var deHoekGame in gamesDeHoek)
            {
                var gamesForMatchDate = calendars.Where(c => c.MatchDate == deHoekGame.MatchDate).ToList();
                dictionary.Add(deHoekGame.MatchDate, GetGameModels(context, gamesForMatchDate, false));
            }

            return dictionary;
        }

        private List<GameModel> GetGameModels(FcDeHoekContext context, List<Game> calendars, bool setIsNextGame = true)
        {
            var games = new List<GameModel>();
            Game nextGame = null;
            if (setIsNextGame)
                nextGame = GameQueries.GetNextGame(context);



            foreach (var calendar in calendars)
            {
                if (calendar.GameCompetition == null)
                    calendar.GameCompetition = context.BaseDomains.FirstOrDefault(bd => bd.IdBaseDomain == calendar.IdCompetition);

                if (calendar.GameHomeTeam == null)
                    calendar.GameHomeTeam = TeamQueries.GetTeamById(context, calendar.IdHomeTeam);

                if (calendar.GameAwayTeam == null)
                    calendar.GameAwayTeam = TeamQueries.GetTeamById(context, calendar.IdAwayTeam);
                

                games.Add(new GameModel
                {
                    IdGame = calendar.IdGame,
                    Competition = calendar.GameCompetition?.Description,
                    MatchDay = calendar.MatchDate.Date,
                    IdHomeTeam = calendar.IdHomeTeam,
                    HomeTeam = calendar.GameHomeTeam?.Name,
                    AwayTeam = calendar.GameAwayTeam?.Name,
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
                    IsNextGame = calendar.IdGame == nextGame?.IdGame
                });
            }

            return games;
        }
    }
}