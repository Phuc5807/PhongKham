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
    public class BENHNHANsController : Controller
    {

        private phongkham1Entities db = new phongkham1Entities();

        // GET: BENHNHANs
        public ActionResult Index()
        {
            var bENHNHANs = db.BENHNHANs.Include(b => b.BAOHIEM).Include(b => b.KHOA);
            return View(bENHNHANs.ToList());
        }

        // GET: BENHNHANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENHNHAN bENHNHAN = db.BENHNHANs.Find(id);
            if (bENHNHAN == null)
            {
                return HttpNotFound();
            }
            return View(bENHNHAN);
        }

        
        
        // GET: BENHNHANs/Create
        public ActionResult Create()
        {
            ViewBag.MaBH = new SelectList(db.BAOHIEMs, "MaBH", "LoaiBH");
            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa");
            return View();
        }

        // POST: BENHNHANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBN,TenBN,NamSinh,GioiTinh,DiaChi,Phone,CCCD,TenKhoa,MaBH,PasswordBN")] BENHNHAN bENHNHAN)
        {
            if (ModelState.IsValid)
            {
                if (db.BENHNHANs.Any(b => b.Phone == bENHNHAN.Phone))
                {
                    ModelState.AddModelError("Phone", "Số điện thoại này đã tồn tại.");
                }

                if (db.BENHNHANs.Any(b => b.CCCD == bENHNHAN.CCCD))
                {
                    ModelState.AddModelError("CCCD", "CCCD này đã tồn tại.");
                }
                if (!ModelState.IsValid)
                {
                    ViewBag.MaBH = new SelectList(db.BAOHIEMs, "MaBH", "LoaiBH", bENHNHAN.MaBH);
                    ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bENHNHAN.TenKhoa);
                    return View(bENHNHAN);
                }

                // Gán role là BENHNHAN cho tài khoản bệnh nhân
                
                bENHNHAN.Role = "BENHNHAN";             
                db.BENHNHANs.Add(bENHNHAN);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đăng ký thành công!";
          
                return RedirectToAction("Index", "LoginBN");
            }

            ViewBag.MaBH = new SelectList(db.BAOHIEMs, "MaBH", "LoaiBH", bENHNHAN.MaBH);
            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bENHNHAN.TenKhoa);
            return View(bENHNHAN);

        }

        // GET: BENHNHANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENHNHAN bENHNHAN = db.BENHNHANs.Find(id);
            if (bENHNHAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBH = new SelectList(db.BAOHIEMs, "MaBH", "LoaiBH", bENHNHAN.MaBH);
            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bENHNHAN.TenKhoa);
            return View(bENHNHAN);
        }

        // POST: BENHNHANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NamSinh,TenBN,MaBN,GioiTinh,DiaChi,Phone,CCCD,TenKhoa,MaBH")] BENHNHAN bENHNHAN)
        {
            if (ModelState.IsValid)
            {
                if (db.BENHNHANs.Any(b => b.CCCD == bENHNHAN.CCCD && b.MaBN != bENHNHAN.MaBN))
                {
                    ModelState.AddModelError("CCCD", "CCCD đã tồn tại.");
                }

                // Kiểm tra trùng lặp số điện thoại
                if (db.BENHNHANs.Any(b => b.Phone == bENHNHAN.Phone && b.MaBN != bENHNHAN.MaBN))
                {
                    ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.MaBH = new SelectList(db.BAOHIEMs, "MaBH", "LoaiBH", bENHNHAN.MaBH);
                    ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bENHNHAN.TenKhoa);
                    return View(bENHNHAN);
                }

                db.Entry(bENHNHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBH = new SelectList(db.BAOHIEMs, "MaBH", "LoaiBH", bENHNHAN.MaBH);
            ViewBag.TenKhoa = new SelectList(db.KHOAs, "TenKhoa", "TenKhoa", bENHNHAN.TenKhoa);
            return View(bENHNHAN);
        }

        // GET: BENHNHANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENHNHAN bENHNHAN = db.BENHNHANs.Find(id);
            if (bENHNHAN == null)
            {
                return HttpNotFound();
            }
            return View(bENHNHAN);
        }

        // POST: BENHNHANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BENHNHAN bENHNHAN = db.BENHNHANs.Find(id);
            db.BENHNHANs.Remove(bENHNHAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonalInformation(int id)
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
