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
    public class akademisyensController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
        akademisyen akd = new akademisyen();
       
        // GET: akademisyens
        public ActionResult Index()
        {
            return View(db.akademisyens.ToList());
        }
       

        public ActionResult devam(int? id)
        {

            List<akademisyen> a = db.akademisyens.Where(m => m.akademisyen_id >= id).Take(5).ToList();


            return View(a);
        }
        public ActionResult geri(int? id)
        {
            List<akademisyen> c = db.akademisyens.Where(m => m.akademisyen_id == id).ToList();
            List<akademisyen> a = db.akademisyens.Where(m => m.akademisyen_id <= id).OrderByDescending(m => m.akademisyen_id).Take(5).ToList();
            a = a.OrderBy(m => m.akademisyen_id).ToList();


            return View(a);
        }

        public ActionResult akademisyenarama()
        {

            List<akademisyen> a = db.akademisyens.ToList();
            List<akademisyen> b = new List<akademisyen>();
            int i = 0;
            foreach (var item in a)
            {
                b.Add(item);
                i++;
                if (i == 5)
                {
                    break;
                }

            }


            return View(b);

        }


        [HttpPost]

        public ActionResult arama(string akademisyenbilgisi, string turu)
        {

            List<akademisyen> result = new List<akademisyen>();

            if (turu == "akademisyen_adi")
            {
                if (akademisyenbilgisi == "")
                {


                    var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                    return sonuc1;


                }


                //result = db.projes.Where(x => x.proje_adi.Contains(projeadi)).ToList();
                result = db.akademisyens.Where(x => x.akademisyen_adi.StartsWith(akademisyenbilgisi)).ToList();
            }
            else if (turu == "akademisyen_soyadi")
            {

                if (akademisyenbilgisi == "")
                {

                    var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                    return sonuc1;


                }
                result = db.akademisyens.Where(x => x.akademisyen_soyadi.StartsWith(akademisyenbilgisi)).ToList();
            }


            List<akademisyen> prjm = new List<akademisyen>();

            foreach (akademisyen prj in result)
            {


                prjm.Add(prj);
            }

            var sso = Json(result, JsonRequestBehavior.AllowGet);
            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;
        }

        [HttpPost]

        public ActionResult secilenuniversite(int universiteid)
        {
          

            ViewData["akademisyen_kayitli_old_uid"] = universiteid;

            List<fakulte> result = new List<fakulte>();

            if (universiteid == 0)
            {


                var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                return sonuc1;


            }


            //result = db.projes.Where(x => x.proje_adi.Contains(projeadi)).ToList();
            result = db.fakultes.Where(m => m.fak_uni_id == universiteid).ToList();
            List<SelectListItem> fkt = new List<SelectListItem>();

            SelectListItem a;
            foreach (fakulte i in result)
            {
                a = new SelectListItem();
                a.Text = i.fakulte_adi;
                a.Value = i.fakulte_id.ToString();
                a.Selected = false;
                fkt.Add(a);
            }

            var sso = Json(fkt, JsonRequestBehavior.AllowGet);
            return sso;
        }


        [HttpPost]

        public ActionResult sec(int fakulteid)
        {
           

            ViewData["akademisyen_kayitli_old_fid"] = fakulteid;

            List<bolum> result = new List<bolum>();

            if (fakulteid == 0)
            {


                var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                return sonuc1;


            }


            //result = db.projes.Where(x => x.proje_adi.Contains(projeadi)).ToList();
            result = db.bolums.Where(m => m.bol_fakulte_id == fakulteid).ToList();
            List<SelectListItem> fkt = new List<SelectListItem>();

            SelectListItem a;
            foreach (bolum i in result)
            {
                a = new SelectListItem();
                a.Text = i.bolum_adi;
                a.Value = i.bolum_id.ToString();
                a.Selected = false;
                fkt.Add(a);
            }

            var sso = Json(fkt, JsonRequestBehavior.AllowGet);
            return sso;
        }


       
        [HttpPost]

        public ActionResult sec1(int bolumid)
        {
            

            if (bolumid == 0)
            {
                return RedirectToAction("Create");
            }


            akademisyen a = new akademisyen();

            a.aka_bolum_id = bolumid;
            Session["akademisyen"] = a;


            List<SelectListItem> fkt = new List<SelectListItem>();

            var sso = Json(fkt, JsonRequestBehavior.AllowGet);
            return sso;
        }




      






        // GET: akademisyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            akademisyen akademisyen = db.akademisyens.Find(id);
            if (akademisyen == null)
            {
                return HttpNotFound();
            }
            return View(akademisyen);
        }

        // GET: akademisyens/Create
        public ActionResult Create()
        {   //view data controller ile view arası baglantıda TempData ise contoller ile controller arası baglantıda kullanılır.
            ViewData["universiteler"] = db.universites.ToList();
            ViewData["fakulteler"] = db.fakultes.ToList();
            ViewData["bolumler"] = db.bolums.ToList();
            Session["users"] = db.Users.ToList();
            return View();
        }

        // POST: akademisyens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "akademisyen_id,akademisyen_adi,akademisyen_soyadi,akademisyen_tc,akademisyen_tel,akademisyen_sitesi,akademisyen_mail,akademisyen_parola,akademisyen_kullanici_adi,akademisyen_unvani,akademisyen_uzmanlik_alani,akademisyen_oda_no,aka_bolum_id")] akademisyen akademisyen)
        {

            if (ModelState.IsValid)
            {
                
                akademisyen b= (akademisyen)Session["akademisyen"];


                if (b.aka_bolum_id != 0 && User.Identity.GetUserName()!="")
                {
                    string kullanici_adi_bu = User.Identity.GetUserName();
                    ApplicationUser user =db.Users.FirstOrDefault(x=>x.Email==kullanici_adi_bu);

                   
                    //ApplicationUser user = (ApplicationUser)Session["mevcut_kullanici"];
                    //var parola = Session["parola_acık"];


                    akademisyen.aka_bolum_id = b.aka_bolum_id;
                    akademisyen.akademisyen_kullanici_adi = user.UserName;
                    akademisyen.akademisyen_mail = user.Email;
                    akademisyen.akademisyen_parola = user.PasswordHash;
                    db.akademisyens.Add(akademisyen);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
               
             
                    return RedirectToAction("Create");
                

                
            }

            return View(akademisyen);
        }

        // GET: akademisyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            akademisyen akademisyen = db.akademisyens.Find(id);
            if (akademisyen == null)
            {
                return HttpNotFound();
            }
            return View(akademisyen);
        }

        // POST: akademisyens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "akademisyen_id,akademisyen_adi,akademisyen_soyadi,akademisyen_tc,akademisyen_tel,akademisyen_sitesi,akademisyen_mail,akademisyen_parola,akademisyen_kullanici_adi,akademisyen_unvani,akademisyen_uzmanlik_alani,akademisyen_oda_no,aka_bolum_id")] akademisyen akademisyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(akademisyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(akademisyen);
        }

        // GET: akademisyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            akademisyen akademisyen = db.akademisyens.Find(id);
            if (akademisyen == null)
            {
                return HttpNotFound();
            }
            return View(akademisyen);
        }

        // POST: akademisyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            akademisyen akademisyen = db.akademisyens.Find(id);
            db.akademisyens.Remove(akademisyen);
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
