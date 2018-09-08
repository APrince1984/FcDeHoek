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
    public class HistoricStatsController : Controller
    {
        // GET: HistoricStats
        public ActionResult Index()
        {
            return View(GetHistoricStats());
        }

        public ActionResult Detail(int idStat)
        {
            using (var context = new FcDeHoekContext())
            {
                if (idStat == 0)
                    return View(EmptyModel(context));

                var stat = context.HistoricStatses.FirstOrDefault(hs => hs.IdHistoricStats == idStat);
                if (stat == null)
                    return View(EmptyModel(context));

                var model = MapHistoricStatToModel(stat);
                model.AllPlayers = GetAllPlayers(context);
                model.AllSeasons = GetAllSeasons(context);


                return View(model);
            }
        }

        private HistoricStatsModel EmptyModel(FcDeHoekContext context)
        {
            return new HistoricStatsModel
            {
                AllPlayers = GetAllPlayers(context),
                AllSeasons = GetAllSeasons(context)
            };
        }

        public ActionResult Save(HistoricStatsModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                var historicStat = new HistoricStats();
                historicStat.IdHistoricStats = model.IdHistoricStats;
                historicStat.IdSeason = model.IdSeason;
                historicStat.IdPerson = model.IdPlayer;
                historicStat.Goals = model.Goals;
                historicStat.Assists = model.Assists;
                historicStat.Penalties = model.Penalties;

                context.HistoricStatses.AddOrUpdate(historicStat);
                context.SaveChanges();

                return RedirectToAction("Index");

            }
        }

        private List<HistoricSeason> GetAllSeasons(FcDeHoekContext context)
        {
            var seasons = context.Seasons.ToList();
            var models = new List<HistoricSeason>();
            foreach (var season in seasons)
            {
                models.Add(new HistoricSeason
                {
                    IdSeason = season.IdSeason,
                    Season = $"{season.SeasonStartYear} - {season.SeasonEndYear}"
                });
            }

            return models;
        }

        private List<HistoricPlayer> GetAllPlayers(FcDeHoekContext context)
        {
            var players = context.Persons.Where(p => p.IsPlayer).ToList();
            var models = new List<HistoricPlayer>();
            foreach (var player in players)
            {
                models.Add(new HistoricPlayer
                {
                    IdPlayer = player.IdPerson,
                    Player = $"{player.Name} {player.FirstName}"
                });
            }

            return models;
        }

        private List<HistoricStatsModel> GetHistoricStats()
        {
            using (var context = new FcDeHoekContext())
            {
                var models = new List<HistoricStatsModel>();
                var historicStats = context.HistoricStatses.ToList();
                foreach (var stat in historicStats)
                {
                    models.Add(MapHistoricStatToModel(stat));
                }

                return models;
            }
        }

        private HistoricStatsModel MapHistoricStatToModel(HistoricStats stat)
        {
            return new HistoricStatsModel
            {
                Assists = stat.Assists,
                Goals = stat.Goals,
                IdPlayer = stat.IdPerson,
                IdSeason = stat.IdSeason,
                IdHistoricStats = stat.IdHistoricStats,
                Penalties = stat.Penalties,
                Player = GetPlayerName(stat.IdPerson),
                Season = GetSeason(stat.IdSeason)
            };
        }

        private string GetSeason(int idSeason)
        {
            using (var context = new FcDeHoekContext())
            {
                var season = SeasonQueries.GetById(context, idSeason);
                return season == null ? string.Empty : $"{season.SeasonStartYear} - {season.SeasonEndYear}";
            }
        }

        private string GetPlayerName(int idPerson)
        {
            using (var context = new FcDeHoekContext())
            {
                var player = PersonQueries.GetById(context, idPerson);
                return player == null ? string.Empty : $"{player.Name} {player.FirstName}";
            }
        }
    }
}