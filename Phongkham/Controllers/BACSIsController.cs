using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phongkham.Models;

namespace Phongkham.Controllers
{
    public class BACSIsController : Controller
    {
        private phongkham1Entities db = new phongkham1Entities();

        // GET: BACSIs
        [Authorize]
        public ActionResult Index()
        {
            var bACSIs = db.BACSIs.Include(b => b.KHOA);
            return View(bACSIs.ToList());

        }
        

        [Authorize]
        // GET: BACSIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            return View(bACSI);
        }
        [Authorize]
        // GET: BACSIs/Create
        public ActionResult Create()
        {
            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa");
            return View();
        }

        // POST: BACSIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "TenBS,MaBS,GioiTinhBS,DiaChiBS,PhoneBS,NamSinhBS,Email,CCCDBS,TenKhoa,PasswordBS")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                if (db.BACSIs.Any(b => b.Email == bACSI.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã tồn tại.");
                }

                if (db.BACSIs.Any(b => b.PhoneBS == bACSI.PhoneBS))
                {
                    ModelState.AddModelError("PhoneBS", "Số điện thoại này đã tồn tại.");
                }
                if (db.BACSIs.Any(b => b.CCCDBS == bACSI.CCCDBS))
                {
                    ModelState.AddModelError("CCCDBS", "CCCD này đã tồn tại.");
                }
                if (!ModelState.IsValid)
                {
                    ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bACSI.TenKhoa);
                    return View(bACSI);
                }

                // Gán role là BACSI cho tài khoản bác sĩ
                bACSI.Role = "BACSI";

                db.BACSIs.Add(bACSI);
                db.SaveChanges();
                return RedirectToAction("Index","BACSIs");
            }
           

            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bACSI.TenKhoa);
   
            return View(bACSI);

        }

        [Authorize]
        // GET: BACSIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bACSI.TenKhoa);
            return View(bACSI);
        }

        // POST: BACSIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "TenBS,MaBS,GioiTinhBS,DiaChiBS,PhoneBS,NamSinhBS,Email,CCCDBS,TenKhoa,PasswordBS")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                if (db.BACSIs.Any(b => b.Email == bACSI.Email && b.MaBS != bACSI.MaBS))
                {
                    ModelState.AddModelError("Email", "Email này đã tồn tại.");
                }

                if (db.BACSIs.Any(b => b.PhoneBS == bACSI.PhoneBS && b.MaBS != bACSI.MaBS))
                {
                    ModelState.AddModelError("PhoneBS", "Số điện thoại này đã tồn tại.");
                }
                if (db.BACSIs.Any(b => b.CCCDBS == bACSI.CCCDBS))
                {
                    ModelState.AddModelError("CCCDBS", "CCCD này đã tồn tại.");
                }
                if (!ModelState.IsValid)
                {
                    ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bACSI.TenKhoa);
                    return View(bACSI);
                }
                // Gán role là BACSI cho tài khoản bác sĩ
                bACSI.Role = "BACSI";

                db.Entry(bACSI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bACSI.TenKhoa);
            return View(bACSI);
        }
        // AJAX check for email uniqueness
        [HttpPost]
        public JsonResult IsEmailUnique(string Email)
        {
            return Json(!db.BACSIs.Any(b => b.Email == Email), JsonRequestBehavior.AllowGet);
        }

        // AJAX check for phone uniqueness
        [HttpPost]
        public JsonResult IsPhoneUnique(string PhoneBS)
        {
            return Json(!db.BACSIs.Any(b => b.PhoneBS == PhoneBS), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsCCCDUnique(string CCCDBS)
        {
            return Json(!db.BACSIs.Any(b => b.CCCDBS == CCCDBS), JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        // GET: BACSIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            return View(bACSI);
        }

        // POST: BACSIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI != null)
            {
                // Kiểm tra nếu bác sĩ có lịch khám
                var lichKhams = db.LICHKHAMs.Where(l => l.MaBS == bACSI.MaBS).ToList();
                if (lichKhams.Count > 0)
                {
                    ModelState.AddModelError("", "Không thể xóa bác sĩ vì họ đang có lịch khám.");
                    return View("Delete", bACSI);
                }

                // Kiểm tra nếu bác sĩ là admin
                if (bACSI.TenKhoa == "Admin")
                {
                    ModelState.AddModelError("", "Không thể xóa bác sĩ có vai trò Admin.");
                    return View("Delete", bACSI);
                }

                // Nếu không có lịch khám và không phải là Admin, tiến hành xóa
                db.BACSIs.Remove(bACSI);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Authorize]
        public ActionResult Medicalrecord()
        {
            return View();
            
        }



        //PHÂN QUYỀN
        //public ActionResult AssignRole(int id)
        //{
        //    var user = db.BACSIs.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.Roles = new SelectList(new List<string> { "Admin", "BACSI", "BENHNHAN" });
        //    return View(user);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AssignRole(int id, string role)
        //{
        //    var user = db.BACSIs.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    user.Role = role;
        //    db.Entry(user).State = EntityState.Modified;
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}
    }
}
