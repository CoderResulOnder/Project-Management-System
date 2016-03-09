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
    public class grupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: grups
        public ActionResult Index()
        {
            return View(db.grups.ToList());
        }

        // GET: grups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grup grup = db.grups.Find(id);
            if (grup == null)
            {
                return HttpNotFound();
            }
            return View(grup);
        }

        // GET: grups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: grups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "grup_id,grup_adi,grup_uye_sayisi,grup_proje_id,grub_akademisyen_id")] grup grup)
        {
            if (ModelState.IsValid)
            {
                db.grups.Add(grup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grup);
        }

        // GET: grups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grup grup = db.grups.Find(id);
            if (grup == null)
            {
                return HttpNotFound();
            }
            return View(grup);
        }

        // POST: grups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "grup_id,grup_adi,grup_uye_sayisi,grup_proje_id,grub_akademisyen_id")] grup grup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grup);
        }

        // GET: grups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grup grup = db.grups.Find(id);
            if (grup == null)
            {
                return HttpNotFound();
            }
            return View(grup);
        }

        // POST: grups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            grup grup = db.grups.Find(id);
            db.grups.Remove(grup);
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
