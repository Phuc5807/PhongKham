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
    public class XETNGHIEMsController : Controller
    {
        private phongkham1Entities db = new phongkham1Entities();

        // GET: XETNGHIEMs
        public ActionResult Index()
        {
            var xETNGHIEMs = db.XETNGHIEMs.Include(x => x.BACSI).Include(x => x.BENHNHAN);
            return View(xETNGHIEMs.ToList());
        }

        // GET: XETNGHIEMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XETNGHIEM xETNGHIEM = db.XETNGHIEMs.Find(id);
            if (xETNGHIEM == null)
            {
                return HttpNotFound();
            }
            return View(xETNGHIEM);
        }

        // GET: XETNGHIEMs/Create
        public ActionResult Create()
        {
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS");
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN");
            return View();
        }

        // POST: XETNGHIEMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaXN,LoaiXN,ChanDoanXN,MaBN,MaBS")] XETNGHIEM xETNGHIEM)
        {
            if (ModelState.IsValid)
            {
                db.XETNGHIEMs.Add(xETNGHIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", xETNGHIEM.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", xETNGHIEM.MaBN);
            return View(xETNGHIEM);
        }

        // GET: XETNGHIEMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XETNGHIEM xETNGHIEM = db.XETNGHIEMs.Find(id);
            if (xETNGHIEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", xETNGHIEM.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", xETNGHIEM.MaBN);
            return View(xETNGHIEM);
        }

        // POST: XETNGHIEMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaXN,LoaiXN,ChanDoanXN,MaBN,MaBS")] XETNGHIEM xETNGHIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xETNGHIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", xETNGHIEM.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", xETNGHIEM.MaBN);
            return View(xETNGHIEM);
        }

        // GET: XETNGHIEMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XETNGHIEM xETNGHIEM = db.XETNGHIEMs.Find(id);
            if (xETNGHIEM == null)
            {
                return HttpNotFound();
            }
            return View(xETNGHIEM);
        }

        // POST: XETNGHIEMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            XETNGHIEM xETNGHIEM = db.XETNGHIEMs.Find(id);
            db.XETNGHIEMs.Remove(xETNGHIEM);
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
