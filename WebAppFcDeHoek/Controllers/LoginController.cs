using System.Security.Authentication;
using System.Web.Mvc;
using System.Web.Security;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Models;

namespace WebAppFcDeHoek.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                var user = UserQueries.ValidateUser(context, model.UserName, model.Password);
                if (user == null)
                    throw new AuthenticationException("Incorrect Credentials!");

                Session.Add("IsAdmin", user.IsAdmin);
                Session.Add("IdPerson", user.IdPerson);

                FormsAuthentication.SetAuthCookie(user.UserName, false);
                
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}