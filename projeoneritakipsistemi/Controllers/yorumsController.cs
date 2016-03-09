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
    public class yorumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: yorums
        public ActionResult Index()
        {
            var yorums = db.yorums.Include(y => y.proje);
            return View(yorums.ToList());
        }

        // GET: yorums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: yorums/Create
        public ActionResult Create()
        {
            ViewBag.yor_proje_id = new SelectList(db.projes, "proje_id", "proje_adi");
            return View();
        }

        // POST: yorums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yorum_id,yorum_icerigi,yorumu_yapan_id,yorumu_yapan_statu,yor_proje_id,yorum_tarihi")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.yorums.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.yor_proje_id = new SelectList(db.projes, "proje_id", "proje_adi", yorum.yor_proje_id);
            return View(yorum);
        }

        // GET: yorums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.yor_proje_id = new SelectList(db.projes, "proje_id", "proje_adi", yorum.yor_proje_id);
            return View(yorum);
        }

        // POST: yorums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yorum_id,yorum_icerigi,yorumu_yapan_id,yorumu_yapan_statu,yor_proje_id,yorum_tarihi")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("projearama","projes");
            }
            ViewBag.yor_proje_id = new SelectList(db.projes, "proje_id", "proje_adi", yorum.yor_proje_id);
            return View(yorum);
        }

        // GET: yorums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: yorums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yorum yorum = db.yorums.Find(id);
            db.yorums.Remove(yorum);
            db.SaveChanges();
            return RedirectToAction("projearama","projes");
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
