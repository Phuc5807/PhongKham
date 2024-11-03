using Phongkham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phongkham.Controllers
{
    public class DichVuController : Controller
    {
        // GET: DichVu
        private phongkham1Entities db = new phongkham1Entities();
        public ActionResult Index()
        {
            return View(db.HOADONs.ToList());
        }
    }
}