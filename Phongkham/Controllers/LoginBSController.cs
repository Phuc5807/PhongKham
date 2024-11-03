using Phongkham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Phongkham.Controllers
{
    public class LoginBSController : Controller
    {
        phongkham1Entities db = new phongkham1Entities();
        // GET: LoginBS
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginBS()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LoginBS(BACSI _user) /*,string email,string password*/
        {



            var doctor = db.BACSIs.FirstOrDefault(b => b.Email == _user.Email && b.PasswordBS == _user.PasswordBS);

            if (doctor != null)
            {
                FormsAuthentication.SetAuthCookie(doctor.Email, false);
                // You can store additional user info in a cookie if needed
                var authTicket = new FormsAuthenticationTicket(
                    1,
                    doctor.Email,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    doctor.TenBS,
                    FormsAuthentication.FormsCookiePath
                );

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return RedirectToAction("CN", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid login attempt.";
                return View("Index");
            }








            /*
             * LoginBS(BACSI _user)
             var check = db.BACSIs.Where(s => s.Email == _user.Email && s.PasswordBS == _user.PasswordBS).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["Email"] = _user.Email;

                return RedirectToAction("CN", "Home");
            }
            */
        }
        [Authorize]
        public ActionResult LogoutBS()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            return RedirectToAction("Test", "Home");
        }
        
    }
}