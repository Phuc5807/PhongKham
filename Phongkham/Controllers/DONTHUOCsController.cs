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
    public class DONTHUOCsController : Controller
    {
        private phongkham1Entities db = new phongkham1Entities();

        // GET: DONTHUOCs
        public ActionResult Index()
        {
            var dONTHUOCs = db.DONTHUOCs.Include(d => d.BACSI).Include(d => d.BENHNHAN).Include(d => d.THUOC);
            return View(dONTHUOCs.ToList());
        }

        // GET: DONTHUOCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONTHUOC dONTHUOC = db.DONTHUOCs.Find(id);
            if (dONTHUOC == null)
            {
                return HttpNotFound();
            }
            return View(dONTHUOC);
        }

        // GET: DONTHUOCs/Create
        public ActionResult Create()
        {
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS");
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN");
            ViewBag.MaThuoc = new SelectList(db.THUOCs, "MaThuoc", "TenThuoc");
            return View();
        }

        // POST: DONTHUOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDT,SoLuongThuoc,NgayKeDon,MaBS,MaBN,MaThuoc")] DONTHUOC dONTHUOC)
        {
            if (ModelState.IsValid)
            {
                db.DONTHUOCs.Add(dONTHUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", dONTHUOC.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", dONTHUOC.MaBN);
            ViewBag.MaThuoc = new SelectList(db.THUOCs, "MaThuoc", "TenThuoc", dONTHUOC.MaThuoc);
            return View(dONTHUOC);
        }

        // GET: DONTHUOCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONTHUOC dONTHUOC = db.DONTHUOCs.Find(id);
            if (dONTHUOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", dONTHUOC.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", dONTHUOC.MaBN);
            ViewBag.MaThuoc = new SelectList(db.THUOCs, "MaThuoc", "TenThuoc", dONTHUOC.MaThuoc);
            return View(dONTHUOC);
        }

        // POST: DONTHUOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDT,SoLuongThuoc,NgayKeDon,MaBS,MaBN,MaThuoc")] DONTHUOC dONTHUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONTHUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", dONTHUOC.MaBS);
            ViewBag.MaBN = new SelectList(db.BENHNHANs, "MaBN", "TenBN", dONTHUOC.MaBN);
            ViewBag.MaThuoc = new SelectList(db.THUOCs, "MaThuoc", "TenThuoc", dONTHUOC.MaThuoc);
            return View(dONTHUOC);
        }

        // GET: DONTHUOCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONTHUOC dONTHUOC = db.DONTHUOCs.Find(id);
            if (dONTHUOC == null)
            {
                return HttpNotFound();
            }
            return View(dONTHUOC);
        }

        // POST: DONTHUOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DONTHUOC dONTHUOC = db.DONTHUOCs.Find(id);
            db.DONTHUOCs.Remove(dONTHUOC);
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
