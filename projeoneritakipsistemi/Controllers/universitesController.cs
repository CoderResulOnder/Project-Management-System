using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projeoneritakipsistemi.Models;

namespace projeoneritakipsistemi.Controllers
{
    public class universitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: universites
        public ActionResult Index()
        {
            return View(db.universites.ToList());
        }

        // GET: universites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            universite universite = db.universites.Find(id);
            if (universite == null)
            {
                return HttpNotFound();
            }
            return View(universite);
        }

        // GET: universites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: universites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "universite_id,universite_adi,universite_adresi,universite_faks,universite_tel,universite_sehir")] universite universite)
        {
            if (ModelState.IsValid)
            {
                db.universites.Add(universite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(universite);
        }

        // GET: universites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            universite universite = db.universites.Find(id);
            if (universite == null)
            {
                return HttpNotFound();
            }
            return View(universite);
        }

        // POST: universites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "universite_id,universite_adi,universite_adresi,universite_faks,universite_tel,universite_sehir")] universite universite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(universite);
        }

        // GET: universites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            universite universite = db.universites.Find(id);
            if (universite == null)
            {
                return HttpNotFound();
            }
            return View(universite);
        }

        // POST: universites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            universite universite = db.universites.Find(id);
            db.universites.Remove(universite);
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
