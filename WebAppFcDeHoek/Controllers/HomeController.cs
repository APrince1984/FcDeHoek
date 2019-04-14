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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new FcDeHoekContext())
            {
                var model = new HomeModel
                {
                    CurrentDivision = GetCurrentDivision(context),
                    PreviousGame = GameQueries.GetPreviousGame(context),
                    NextGame = GameQueries.GetNextGame(context),
                    NextGames = GameQueries.GetNextGames(context) == null ? new List<Game>() : GameQueries.GetNextGames(context).ToList()
                };

                if (model.PreviousGame != null)
                {
                    model.PreviousGameHomeTeam = TeamQueries.GetTeamById(context, model.PreviousGame.IdHomeTeam);
                    model.PreviousGameAwayTeam = TeamQueries.GetTeamById(context, model.PreviousGame.IdAwayTeam);
                    model.PreviousGameResult = model.PreviousGameHomeTeam.IdTeam == 1 ? eResult.GetResult(model.PreviousGame.GoalsHomeTeam, model.PreviousGame.GoalsAwayTeam) : eResult.GetResult(model.PreviousGame.GoalsAwayTeam, model.PreviousGame.GoalsHomeTeam);
                }


                if (model.NextGame != null)
                {
                    model.NextGameHomeTeam = TeamQueries.GetTeamById(context, model.NextGame.IdHomeTeam);
                    model.NextGameAwayTeam = TeamQueries.GetTeamById(context, model.NextGame.IdAwayTeam);

                }

               
                model.AllTeams = context.Teams.ToList();

                return View(model);
            }
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Fc ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private string GetCurrentDivision(FcDeHoekContext context)
        {
            var currentSeason = SeasonQueries.GetCurrentSeason(context);
            return $"{currentSeason.Division}de Reeks";
        }
    }
}