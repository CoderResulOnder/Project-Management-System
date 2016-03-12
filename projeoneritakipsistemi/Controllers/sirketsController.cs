using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projeoneritakipsistemi.Models;
using Microsoft.AspNet.Identity;

namespace projeoneritakipsistemi.Controllers
{
    public class sirketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: sirkets
        public async Task<ActionResult> Index()
        {
            return View(await db.sirkets.ToListAsync());
        }

        // GET: sirkets/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirket sirket = await db.sirkets.FindAsync(id);
            if (sirket == null)
            {
                return HttpNotFound();
            }
            return View(sirket);
        }

        // GET: sirkets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sirkets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "sirket_adi,sirket_adresi,sirket_tel,sirket_site,sirket_mail,sirket_fax_adresi,sirketcalismaalani,sirket_hakkinda,sirketi_ekleyen_kullaniciid")] sirket sirket)
        {
            if (ModelState.IsValid)
            {
                sirket.sirketi_ekleyen_kullaniciid = User.Identity.GetUserName();
                db.sirkets.Add(sirket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sirket);
        }

        // GET: sirkets/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirket sirket = await db.sirkets.FindAsync(id);
            if (sirket == null)
            {
                return HttpNotFound();
            }
            return View(sirket);
        }

        // POST: sirkets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "sirket_adi,sirket_adresi,sirket_tel,sirket_site,sirket_mail,sirket_fax_adresi,sirketcalismaalani,sirket_hakkinda,sirketi_ekleyen_kullaniciid")] sirket sirket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sirket).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sirket);
        }

        // GET: sirkets/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirket sirket = await db.sirkets.FindAsync(id);
            if (sirket == null)
            {
                return HttpNotFound();
            }
            return View(sirket);
        }

        // POST: sirkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            sirket sirket = await db.sirkets.FindAsync(id);
            db.sirkets.Remove(sirket);
            await db.SaveChangesAsync();
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
