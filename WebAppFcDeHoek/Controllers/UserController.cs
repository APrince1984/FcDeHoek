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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(GetUsers());
        }

        

        public ActionResult EditUser(int idUser)
        {
            return View("Detail", GetUser(idUser));
        }

        [HttpPost]
        public ActionResult SaveUser(UserModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                var user = UserQueries.GetById(context, model.IdUser);
                if (user == null)
                    user = new User();

                user.UserName = model.UserName;
                user.IsAdmin = model.IsAdmin;
                user.Obsolete = model.Obsolete;

                context.Users.AddOrUpdate(user);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "User");
        }

        private List<UserModel> GetUsers()
        {
            using (var context = new FcDeHoekContext())
            {
                var userModels = new List<UserModel>();
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    userModels.Add(new UserModel
                    {
                        IdUser = user.IdUser,
                        UserName = user.UserName,
                        IsAdmin = user.IsAdmin,
                        Obsolete = user.Obsolete
                    });
                }

                return userModels;
            }
        }

        private UserModel GetUser(int idUser)
        {
            using (var context = new FcDeHoekContext())
            {
                var user = UserQueries.GetById(context, idUser);
                return new UserModel
                {
                    IdUser = user.IdUser,
                    UserName = user.UserName,
                    IsAdmin = user.IsAdmin,
                    Obsolete = user.Obsolete
                };
            }
        }
    }
}