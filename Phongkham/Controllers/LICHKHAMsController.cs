using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Phongkham.Models;

namespace Phongkham.Controllers
{
    public class LICHKHAMsController : Controller
    {
        private phongkham1Entities db = new phongkham1Entities();

        // GET: LICHKHAMs
        [Authorize]
        public ActionResult Index(string doctorName, DateTime? startDate, DateTime? endDate)
        {

            var lICHKHAMs = db.LICHKHAMs.Include(l => l.BACSI).Include(l => l.BENHNHAN);


            if (!string.IsNullOrEmpty(doctorName))
            {
                lICHKHAMs = lICHKHAMs.Where(l => l.BACSI.TenBS.Contains(doctorName));
            }


            if (startDate.HasValue)
            {
                lICHKHAMs = lICHKHAMs.Where(l => l.NgayHen >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                lICHKHAMs = lICHKHAMs.Where(l => l.NgayHen <= endDate.Value);
            }


            var orderedLichKhams = lICHKHAMs.OrderByDescending(l => l.DichVu == "Khám dịch vụ")
                                             .ThenBy(l => l.NgayHen);

            return View(orderedLichKhams.ToList());
        }
        public ActionResult Index1()
        {
            var currentUserEmail = User.Identity.Name;
            var currentUser = db.BENHNHANs.FirstOrDefault(b => b.Phone == currentUserEmail);

            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lICHKHAMs = db.LICHKHAMs.Include(l => l.BENHNHAN).Include(l => l.BACSI)
                                         .Where(l => l.MaBN == currentUser.MaBN && l.TinhTrang == "Chấp nhận")
                                         .OrderByDescending(l => l.DichVu == "Khám dịch vụ")
                                        .ThenBy(l => l.NgayHen); // Sắp xếp "Khám dịch vụ" trước "Khám thường" và sau đó theo ngày hẹn;



            return View(lICHKHAMs.ToList());
        }
        public ActionResult IndexBS()
        {
            var currentUserEmail = User.Identity.Name;
            var currentUser = db.BACSIs.FirstOrDefault(b => b.Email == currentUserEmail);

            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lICHKHAMs = db.LICHKHAMs.Include(l => l.BACSI).Include(l => l.BENHNHAN)
                                         .Where(l => l.MaBS == currentUser.MaBS && l.TinhTrang == "Chấp nhận")
                                         .OrderByDescending(l => l.DichVu == "Khám dịch vụ")
                                        .ThenBy(l => l.NgayHen); // Sắp xếp "Khám dịch vụ" trước "Khám thường" và sau đó theo ngày hẹn;
            
                                

            return View(lICHKHAMs.ToList());
        }

        private string GetCurrentUserPhone()
        {
            //return User.Identity.Name;
            var username = User.Identity.Name;
            var benhnhan = db.BENHNHANs.FirstOrDefault(b => b.Phone == username);
            return benhnhan != null ? benhnhan.Phone : string.Empty;
        }
        //public ActionResult IndexBS()
        //{
        //    var lICHKHAMs = db.LICHKHAMs.Include(l => l.BACSI).Include(l => l.BENHNHAN);
        //    var currentUserPhone = GetCurrentUserPhone();
        //    if (User.IsInRole("BACSI"))
        //    {
        //        var bacSi = db.BACSIs.SingleOrDefault(b => b.PhoneBS == currentUserPhone);
        //        if (bacSi != null)
        //        {
        //            lICHKHAMs = lICHKHAMs.Where(l => l.MaBS == bacSi.MaBS && l.TinhTrang == "Chấp nhận");
        //        }
        //    }
        //    return View(lICHKHAMs.ToList());
        //}
        // GET: LICHKHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICHKHAM lICHKHAM = db.LICHKHAMs.Find(id);
            if (lICHKHAM == null)
            {
                return HttpNotFound();
            }
            return View(lICHKHAM);
        }

        // GET: LICHKHAMs/Create
        
        public ActionResult Create()
        {
            if (!IsUserLoggedIn())
            {
                TempData["Message"] = "Bạn cần đăng nhập để đặt lịch khám.";
                return RedirectToAction("Index", "LoginBN");
            }

            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS");
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN");
            return View();
        }

        // POST: LICHKHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "IDKham,NgayHen,DichVu,NoteBN,MaBS,MaBN")] LICHKHAM lICHKHAM)
        {
            if (ModelState.IsValid)
            {
                lICHKHAM.TinhTrang = "Đang xử lý"; // Set initial status
                db.LICHKHAMs.Add(lICHKHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", lICHKHAM.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", lICHKHAM.MaBN);
            return View(lICHKHAM);
        }
        [Authorize]
        public ActionResult Create1()
        {
            if (!IsUserLoggedIn())
            {
                TempData["Message"] = "Bạn cần đăng nhập để đặt lịch khám.";
                return RedirectToAction("LoginBN", "LoginBN");
            }
            // Lọc danh sách bác sĩ để loại bỏ bác sĩ có tên "Admin"
            var doctors = db.BACSIs.Where(b => b.TenBS != "Admin").ToList();
            ViewBag.MaBS = new SelectList(doctors, "MaBS", "TenBS");

            //ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS");

            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN");
            ViewBag.DichVuList = GetDichVuList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create1([Bind(Include = "IDKham,NgayHen,DichVu,NoteBN,MaBS,MaBN")] LICHKHAM lICHKHAM)
        {
            if (ModelState.IsValid)
            {
                var currentUserPhone = GetCurrentUserPhone();
                var benhNhan = db.BENHNHANs.SingleOrDefault(b => b.Phone == currentUserPhone);
                if (benhNhan != null)
                {
                    lICHKHAM.MaBN = benhNhan.MaBN;
                }
                lICHKHAM.TinhTrang = "Đang xử lý";  // Set initial status
                db.LICHKHAMs.Add(lICHKHAM);
                db.SaveChanges();
                return RedirectToAction("Index1");
            }


            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", lICHKHAM.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", lICHKHAM.MaBN);
            ViewBag.DichVuList = GetDichVuList();
            return View(lICHKHAM);
        }
        private SelectList GetDichVuList()
        {
            var dichVuList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Khám thường", Text = "Khám thường" },
                new SelectListItem { Value = "Khám dịch vụ", Text = "Khám dịch vụ" }
            };

            return new SelectList(dichVuList, "Value", "Text");
        }
        // GET: LICHKHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICHKHAM lICHKHAM = db.LICHKHAMs.Find(id);
            if (lICHKHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", lICHKHAM.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", lICHKHAM.MaBN);
            return View(lICHKHAM);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "BACSI")]
        //public ActionResult ConfirmAppointment(int id)
        //{
        //    var appointment = db.LICHKHAMs.Find(id);
        //    if (appointment != null)
        //    {
        //        appointment.TinhTrang = "Thành công";
        //        db.Entry(appointment).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index1");
        //}

        // POST: LICHKHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKham,NgayHen,DichVu,NoteBN,MaBS,MaBN")] LICHKHAM lICHKHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lICHKHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", lICHKHAM.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", lICHKHAM.MaBN);
            return View(lICHKHAM);
        }

        // GET: LICHKHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICHKHAM lICHKHAM = db.LICHKHAMs.Find(id);
            if (lICHKHAM == null)
            {
                return HttpNotFound();
            }
            return View(lICHKHAM);
        }

        // POST: LICHKHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LICHKHAM lICHKHAM = db.LICHKHAMs.Find(id);
            db.LICHKHAMs.Remove(lICHKHAM);
            db.SaveChanges();
            return RedirectToAction("Index1");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool IsUserLoggedIn()
        {
            return User.Identity.IsAuthenticated;
        }

        

        // New action to update status to "Thành công"
        [Authorize]
        public ActionResult Accept(int id)
        {
            var lICHKHAM = db.LICHKHAMs.Find(id);
            if (lICHKHAM != null)
            {
                lICHKHAM.TinhTrang = "Chấp nhận";
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Cancel(int id)
        {
            var lICHKHAM = db.LICHKHAMs.Find(id);
            if (lICHKHAM != null)
            {
                lICHKHAM.TinhTrang = "Đã hủy";
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
