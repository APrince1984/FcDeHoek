using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Models;

namespace WebAppFcDeHoek.Controllers
{
    public class PersonManagerController : Controller
    {
        // GET: UserManager
        public ActionResult Index()
        {
            return View(GetPersons());
        }

        public ActionResult EditPerson(int idPerson)
        {
            using (var context = new FcDeHoekContext())
            {
                if (idPerson == 0)
                    return View("Detail", new PersonModel());

                var person = PersonQueries.GetById(context, idPerson);
                var personModel = new PersonModel
                {
                    IdPerson = person.IdPerson,
                    BirthDate = person.BirthDate,
                    FirstName = person.FirstName,
                    IsStaff = person.IsStaff,
                    IsPlayer = person.IsPlayer,
                    Name = person.Name,
                    PerkezNumber = person.PerkezNumber,
                    PhoneNumber = person.PhoneNumber,
                    Picture = person.PicturePath
                };
                return View("Detail", personModel);
            }
        }

        public ActionResult Save(PersonModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                Person person;
                if (model.IdPerson == 0)
                    person = new Person();
                else
                    person = PersonQueries.GetById(context, model.IdPerson);

                person.BirthDate = model.BirthDate;
                person.FirstName = model.FirstName;
                person.IsPlayer = model.IsPlayer;
                person.IsStaff = model.IsStaff;
                person.Name = model.Name;
                person.PerkezNumber = model.PerkezNumber;
                person.PhoneNumber = model.PhoneNumber;
                person.PicturePath = model.Picture;

                context.Persons.AddOrUpdate(person);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private List<PersonModel> GetPersons()
        {
            using (var context = new FcDeHoekContext())
            {
                var models = new List<PersonModel>();
                var persons = context.Persons.ToList();
                foreach (var person in persons)
                {
                    models.Add(new PersonModel
                    {
                        IdPerson = person.IdPerson,
                        Name = person.Name,
                        FirstName = person.FirstName,
                        IsPlayer = person.IsPlayer,
                        IsStaff = person.IsStaff,
                        PerkezNumber = person.PerkezNumber,
                        Picture = person.PicturePath,
                        BirthDate = person.BirthDate,
                        PhoneNumber = person.PhoneNumber
                    });
                }

                return models;
            }
        }
    }
}