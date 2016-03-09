using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projeoneritakipsistemi.Models;
using Microsoft.AspNet.Identity;
namespace projeoneritakipsistemi.Controllers
{
    public class diger_kullanicilarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: diger_kullanicilar
        public ActionResult Index()
        {
            return View(db.diger_kullanicilar.ToList());
        }

        // GET: diger_kullanicilar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diger_kullanicilar diger_kullanicilar = db.diger_kullanicilar.Find(id);
            if (diger_kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(diger_kullanicilar);
        }

        // GET: diger_kullanicilar/Create
        public ActionResult Create()
        {
            Session["users"] = db.Users.ToList();

            return View();
        }

        // POST: diger_kullanicilar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "diger_kullanicilar_id,diger_kullanicilar_adi,diger_kullanicilar_soyadi,diger_kullanicilar_parola,diger_kullanicilar_kullanici_adi,diger_kullanicilar_mail,diger_kullanicilar_unvan,diger_kullanicilar_uzmanlik_alani,diger_kullanicilar_kayit_tarihi,diger_kullanicilar_acıklama")] diger_kullanicilar diger_kullanicilar)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                string kullanici_emaili = User.Identity.GetUserName();
                user = db.Users.Where(x => x.Email ==kullanici_emaili ).First() ;
                //ApplicationUser user = (ApplicationUser)Session["mevcut_kullanici"];
                //var parola = Session["parola_acık"];

                diger_kullanicilar.diger_kullanicilar_kayit_tarihi = DateTime.Now;
                diger_kullanicilar.diger_kullanicilar_kullanici_adi = user.kullaniciadi;
                diger_kullanicilar.diger_kullanicilar_mail = user.Email;
                diger_kullanicilar.diger_kullanicilar_parola = user.PasswordHash;
                db.diger_kullanicilar.Add(diger_kullanicilar);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(diger_kullanicilar);
        }





        // GET: diger_kullanicilar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diger_kullanicilar diger_kullanicilar = db.diger_kullanicilar.Find(id);
            if (diger_kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(diger_kullanicilar);
        }

        // POST: diger_kullanicilar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "diger_kullanicilar_id,diger_kullanicilar_adi,diger_kullanicilar_soyadi,diger_kullanicilar_parola,diger_kullanicilar_kullanici_adi,diger_kullanicilar_mail,diger_kullanicilar_unvan,diger_kullanicilar_uzmanlik_alani,diger_kullanicilar_kayit_tarihi,diger_kullanicilar_acıklama")] diger_kullanicilar diger_kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diger_kullanicilar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diger_kullanicilar);
        }

        // GET: diger_kullanicilar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            diger_kullanicilar diger_kullanicilar = db.diger_kullanicilar.Find(id);
            if (diger_kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(diger_kullanicilar);
        }

        // POST: diger_kullanicilar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            diger_kullanicilar diger_kullanicilar = db.diger_kullanicilar.Find(id);
            db.diger_kullanicilar.Remove(diger_kullanicilar);
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
