using System;
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
    public class TeamController : Controller
    {
        private static Random _random = new Random();
        // GET: Team
        public ActionResult Board()
        {
            return View(GetStaff());
        }

        public ActionResult Players()
        {
            return View(GetPlayers());
        }

        private List<PersonModel> GetStaff()
        {
            using (var context = new FcDeHoekContext())
            {
                var models = new List<PersonModel>();
                var persons = PersonQueries.GetStaff(context).ToList();
                foreach (var person in persons)
                    models.Add(MapPersonToPersonModel(context, person));

                return models;
            }
        }

        private PersonModel MapPersonToPersonModel(FcDeHoekContext context, Person person)
        {
            var picture = person.PicturePath;
            if (string.IsNullOrEmpty(picture))
                picture = GetRandomPicture();
           
            return new PersonModel
            {
                BirthDate = person.BirthDate,
                FirstName = person.FirstName,
                IdPerson = person.IdPerson,
                IsPlayer = person.IsPlayer,
                IsStaff = person.IsStaff,
                Name = person.Name,
                PhoneNumber = person.PhoneNumber,
                PerkezNumber = person.PerkezNumber,
                Picture = picture,
                Stats = person.IsPlayer ? GetPlayerStats(context, person.IdPerson) : null
            };
        }

        private StatisticsModel GetPlayerStats(FcDeHoekContext context, int idPerson)
        {
            var stats = new StatisticsModel
            {
                FriendlyGamesPlayed = 0,
                GoalsInFriendly = 0,
                AssistsInFriendly = 0,

                LeagueGamesPlayed = 0,
                GoalsInLeague = 0,
                AssistsInLeague = 0,

                CupGamesPlayed = 0,
                GoalsInCup = 0,
                AssistsInCup = 0,
            };

            var allPlayerStats = PersonStatisticsQueries.GetByIdPerson(context, idPerson).ToList();
            var season = SeasonQueries.GetCurrentSeason(context);
            var leagueGameIds = GameQueries.GetAllGamesByIdSeasonAndIdCompetition(context, season.IdSeason, eCompetitionType.League).Select(lg => lg.IdGame).ToList();
            var friendlyGameIds = GameQueries.GetAllGamesByIdSeasonAndIdCompetition(context, season.IdSeason, eCompetitionType.Friendly).Select(lf => lf.IdGame).ToList();
            var cupGameIds = GameQueries.GetAllGamesByIdSeasonAndIdCompetition(context, season.IdSeason, eCompetitionType.Cup).Select(lc => lc.IdGame).ToList();

            foreach (var playerStat in allPlayerStats)
            {
                if (leagueGameIds.Contains(playerStat.IdGame))
                {
                    if (playerStat.Played ?? false)
                        stats.LeagueGamesPlayed++;

                    stats.GoalsInLeague += playerStat.Goals ?? 0;
                    stats.AssistsInLeague += playerStat.Assists;
                }

                if (friendlyGameIds.Contains(playerStat.IdGame))
                {
                    if (playerStat.Played ?? false)
                        stats.FriendlyGamesPlayed++;

                    stats.GoalsInFriendly += playerStat.Goals ?? 0;
                    stats.AssistsInFriendly += playerStat.Assists;
                }

                if (cupGameIds.Contains(playerStat.IdGame))
                {
                    if (playerStat.Played ?? false)
                        stats.CupGamesPlayed++;

                    stats.GoalsInCup += playerStat.Goals ?? 0;
                    stats.AssistsInCup += playerStat.Assists;
                }
            }

            stats.TotalGamesPlayed = stats.FriendlyGamesPlayed + stats.LeagueGamesPlayed + stats.CupGamesPlayed;
            stats.TotalGoalsScored = stats.GoalsInFriendly + stats.GoalsInLeague + stats.GoalsInCup;
            stats.TotalAssists = stats.AssistsInFriendly + stats.AssistsInLeague + stats.AssistsInCup;

            return stats;
        }

        private List<PersonModel> GetPlayers()
        {
            using (var context = new FcDeHoekContext())
            {
                var models = new List<PersonModel>();
                var persons = PersonQueries.GetPlayers(context).ToList();
                foreach (var person in persons)
                    models.Add(MapPersonToPersonModel(context, person));

                return models;
            }
        }

        private static string GetRandomPicture()
        {
            var randomPics = new List<string>()
            {
                "http://gdurl.com/M9Mg",
                "http://gdurl.com/qdgN",
                "http://gdurl.com/eDc4t",
                "http://gdurl.com/ZC3x",
                "http://gdurl.com/bd37",
                "http://gdurl.com/wJdr",
                "http://gdurl.com/D7Ml",
                "http://gdurl.com/yk34",
                "http://gdurl.com/2-54",
                "http://gdurl.com/tLx3"
            };
            return randomPics[_random.Next(randomPics.Count)];
        }
    }
}