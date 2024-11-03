using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phongkham.Models;

namespace Phongkham.Controllers
{
    public class HOSOBENHANsController : Controller
    {
        private phongkham1Entities db = new phongkham1Entities();

        // GET: HOSOBENHANs
        public ActionResult Index()
        {
            var hOSOBENHANs = db.HOSOBENHANs.Include(h => h.BACSI).Include(h => h.BENHNHAN);
            return View(hOSOBENHANs.ToList());
        }

        // GET: HOSOBENHANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSOBENHAN hOSOBENHAN = db.HOSOBENHANs.Find(id);
            if (hOSOBENHAN == null)
            {
                return HttpNotFound();
            }
            return View(hOSOBENHAN);
        }

        // GET: HOSOBENHANs/Create
        public ActionResult Create()
        {
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS");
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN");
            return View();
        }

        // POST: HOSOBENHANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChanDoan,IDHoSo,Note,NgayKham,MaBN,MaBS")] HOSOBENHAN hOSOBENHAN)
        {
            if (ModelState.IsValid)
            {
                db.HOSOBENHANs.Add(hOSOBENHAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", hOSOBENHAN.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", hOSOBENHAN.MaBN);
            return View(hOSOBENHAN);
        }

        // GET: HOSOBENHANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSOBENHAN hOSOBENHAN = db.HOSOBENHANs.Find(id);
            if (hOSOBENHAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", hOSOBENHAN.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", hOSOBENHAN.MaBN);
            return View(hOSOBENHAN);
        }

        // POST: HOSOBENHANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChanDoan,IDHoSo,Note,NgayKham,MaBN,MaBS")] HOSOBENHAN hOSOBENHAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOSOBENHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", hOSOBENHAN.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", hOSOBENHAN.MaBN);
            return View(hOSOBENHAN);
        }

        // GET: HOSOBENHANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSOBENHAN hOSOBENHAN = db.HOSOBENHANs.Find(id);
            if (hOSOBENHAN == null)
            {
                return HttpNotFound();
            }
            return View(hOSOBENHAN);
        }

        // POST: HOSOBENHANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOSOBENHAN hOSOBENHAN = db.HOSOBENHANs.Find(id);
            db.HOSOBENHANs.Remove(hOSOBENHAN);
            db.SaveChanges();
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
    }
}
