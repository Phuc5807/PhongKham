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
    public class THUOCsController : Controller
    {
        private phongkham1Entities db = new phongkham1Entities();

        // GET: THUOCs
        public ActionResult Index()
        {
            return View(db.THUOCs.ToList());
        }

        // GET: THUOCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        // GET: THUOCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: THUOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThuoc,TenThuoc,SoLuong")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.THUOCs.Add(tHUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHUOC);
        }

        // GET: THUOCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        // POST: THUOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThuoc,TenThuoc,SoLuong")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHUOC);
        }

        // GET: THUOCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        // POST: THUOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THUOC tHUOC = db.THUOCs.Find(id);
            db.THUOCs.Remove(tHUOC);
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
