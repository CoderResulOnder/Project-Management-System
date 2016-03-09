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
    public class bolumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: bolums
        public ActionResult Index()
        {
            return View(db.bolums.ToList());
        }




        [HttpPost]

        public ActionResult fakulteara(int fak_id)
        {
            bolum blm = db.bolums.FirstOrDefault(x => x.bolum_id == fak_id);

            var fakultem = db.fakultes.Where(x => x.fakulte_id ==blm.bol_fakulte_id).ToList();


            List<fakulte> prjm = new List<fakulte>();

            foreach (fakulte yeni in fakultem)
            {

                prjm.Add(yeni);
            }


            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;

        }

        // GET: bolums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bolum bolum = db.bolums.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // GET: bolums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: bolums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bolum_id,bolum_adi,bolum_adresi,bolum_tel,bolum_faks,bolum_kodu,bolum_sitesi,bol_fakulte_id")] bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.bolums.Add(bolum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bolum);
        }

        // GET: bolums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bolum bolum = db.bolums.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // POST: bolums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bolum_id,bolum_adi,bolum_adresi,bolum_tel,bolum_faks,bolum_kodu,bolum_sitesi,bol_fakulte_id")] bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bolum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bolum);
        }

        // GET: bolums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bolum bolum = db.bolums.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // POST: bolums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bolum bolum = db.bolums.Find(id);
            db.bolums.Remove(bolum);
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
