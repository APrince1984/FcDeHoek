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
                    models.Add(MapPersonToPersonModel(person));

                return models;
            }
        }

        private PersonModel MapPersonToPersonModel(Person person)
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
                Picture = picture
            };
        }
        

        private List<PersonModel> GetPlayers()
        {
            using (var context = new FcDeHoekContext())
            {
                var models = new List<PersonModel>();
                var persons = PersonQueries.GetPlayers(context).ToList();
                foreach (var person in persons)
                    models.Add(MapPersonToPersonModel(person));

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