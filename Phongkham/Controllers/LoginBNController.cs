using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Phongkham.Models;

namespace Phongkham.Controllers
{
    public class LoginBNController : Controller
    {
        phongkham1Entities db = new phongkham1Entities();
        // GET: LoginBN
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginBN()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LoginBN(BENHNHAN _user)
        {
            var check = db.BENHNHANs.Where(s => s.Phone == _user.Phone && s.PasswordBN == _user.PasswordBN).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Thông tin đăng nhập không đúng";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["Phone"] = _user.Phone;
                Session["TenBN"] = check.TenBN; // Lưu tên bệnh nhân vào session
                FormsAuthentication.SetAuthCookie(_user.Phone, false); // Thêm dòng này để kích hoạt Forms Authentication

                //Thông báo yêu cầu đăng nhập để đặt lịch khám đối với bệnh nhân
                if (TempData["Message"] != null)
                {
                    TempData["Message"] = null; // Xóa thông báo đăng nhập sau khi đã đăng nhập thành công
                }

                return RedirectToAction("BN", "Home");
            }

        }
        [Authorize]
        public ActionResult LogoutBN()
        {
            //Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Test", "Home");
        }
    }
}