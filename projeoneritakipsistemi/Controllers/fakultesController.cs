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
    public class fakultesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: fakultes
        public ActionResult Index()
        {
            return View(db.fakultes.ToList());
        }

        // GET: fakultes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fakulte fakulte = db.fakultes.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // GET: fakultes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fakultes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fakulte_id,fakulte_adi,fakulte_tel,fakulte_faks,fak_uni_id")] fakulte fakulte)
        {
            if (ModelState.IsValid)
            {
                db.fakultes.Add(fakulte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fakulte);
        }

        // GET: fakultes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fakulte fakulte = db.fakultes.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // POST: fakultes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fakulte_id,fakulte_adi,fakulte_tel,fakulte_faks,fak_uni_id")] fakulte fakulte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakulte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fakulte);
        }

        // GET: fakultes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fakulte fakulte = db.fakultes.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // POST: fakultes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fakulte fakulte = db.fakultes.Find(id);
            db.fakultes.Remove(fakulte);
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
