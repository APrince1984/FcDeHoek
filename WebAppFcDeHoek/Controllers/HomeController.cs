using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Models;

namespace WebAppFcDeHoek.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel { CurrentDivision = GetCurrentDivision()};
            return View(model);
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

        private string GetCurrentDivision()
        {
            using (var context = new FcDeHoekContext())
            {
                var currentSeason = SeasonQueries.GetCurrentSeason(context);
                return $"{currentSeason.Division}de Reeks";
            }
        }
    }
}