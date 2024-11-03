using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phongkham.Models;
namespace Phongkham.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        phongkham1Entities db = new phongkham1Entities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Test()
        {
            return View();
        }

        [Authorize]
        public ActionResult CN()
        {
            //string doctorEmail = "doctor@example.com";
            //ViewBag.Email = doctorEmail;
            string currentUser = User.Identity.Name;

            using (var db = new phongkham1Entities())
            {
                var bacsi = db.BACSIs.FirstOrDefault(bs => bs.Email == currentUser);
                if (bacsi != null)
                {
                    
                    ViewBag.TenKhoa = bacsi.TenKhoa; // Assuming you have a column named TenKhoa
                }
                else
                {
                    
                    ViewBag.TenKhoa = null;
                }
                //System.Diagnostics.Debug.WriteLine($"Email: {currentUser}, Role: {ViewBag.TenKhoa}, IsAdmin: {ViewBag.IsAdmin}, IsBacSi: {ViewBag.IsBacSi}");
            }


            return View();

        }
        public ActionResult BN()
        {
            //string doctorEmail = "doctor@example.com";
            //ViewBag.Email = doctorEmail;
            return View();
        }

        public ActionResult Unauthorized()
        {
            //string doctorEmail = "doctor@example.com";
            //ViewBag.Email = doctorEmail;
            return View();
        }
    }
}