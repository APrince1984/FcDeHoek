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
    public class SeasonController : Controller
    {
        // GET: Season
        public ActionResult Index()
        {
            return View(GetAllSeasons());
        }

        public ActionResult SeasonDetail(int idSeason)
        {
            using (var context = new FcDeHoekContext())
            {
                var season = new Season();
                if (idSeason != 0)
                    season = SeasonQueries.GetById(context, idSeason);


                return View(MapSeasonToSeasonModel(season, null));
            }
        }

        public ActionResult Save(SeasonModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                Season season;
                if (model.IdSeason == 0)
                    season = new Season();
                else
                    season = SeasonQueries.GetById(context, model.IdSeason);

                season.SeasonStartYear = model.StartYear;
                season.SeasonEndYear = model.EndYear;
                season.Division = model.Division;
               
                context.Seasons.AddOrUpdate(season);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private List<SeasonModel> GetAllSeasons()
        {
            using (var context = new FcDeHoekContext())
            {
                var seasons = context.Seasons.ToList();
                var currentSeason = SeasonQueries.GetCurrentSeason(context);
                var seasonModels = new List<SeasonModel>();
                foreach (var season in seasons)
                {
                    seasonModels.Add(MapSeasonToSeasonModel(season, currentSeason));
                }

                return seasonModels;
            }
        }

        private static SeasonModel MapSeasonToSeasonModel(Season season, Season currentSeason)
        {
            return new SeasonModel
            {
                Division = season.Division,
                IdSeason = season.IdSeason,
                EndYear = season.SeasonEndYear,
                StartYear = season.SeasonStartYear,
                IsCurrentSeason = currentSeason != null && season.IdSeason == currentSeason.IdSeason
            };
        }
    }
}