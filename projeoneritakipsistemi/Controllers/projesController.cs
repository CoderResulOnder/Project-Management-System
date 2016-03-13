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
    public class projesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: projes
        public ActionResult Index()
        {
            Session["projes"] = db.projes.ToList();

            var projes = db.projes.Include(a => a.yorums);
            return View(projes.ToList());
        }
        public ActionResult devam(int? id)
        {
           
            List<proje> a = db.projes.Where(m => m.proje_id >=id).Take(5).ToList();

           
            return View(a);
        }
       
        public ActionResult geri(int? id)
        {
           
            List<proje> c = db.projes.Where(m => m.proje_id == id).ToList();
            List<proje> a = db.projes.Where(m => m.proje_id <= id).OrderByDescending(m=>m.proje_id).Take(5).ToList();
            a = a.OrderBy(m=>m.proje_id).ToList();
           

            return View(a);
        }

        public ActionResult projearama()
        {
            
            
            List<proje> a = db.projes.ToList();
            List<proje> b = new List<proje>();
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

        public ActionResult arama(string projeadi,string projeturu)
        {

          List<proje> result = new List<proje>();

            if (projeturu == "proje_adi")
            {
                if (projeadi == "")
                {
                   
                  
                    var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                    return sonuc1;


                }


                //result = db.projes.Where(x => x.proje_adi.Contains(projeadi)).ToList();
                result = db.projes.Where(x => x.proje_adi.StartsWith(projeadi)).ToList();
            }
            else if (projeturu=="proje_turu") {

                if (projeadi == "")
                {
                    
                    var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                    return sonuc1;


                }
                result = db.projes.Where(x => x.proje_turu.StartsWith(projeadi)).ToList();
            }


            List<proje> prjm = new List<proje>();

            foreach (proje prj in result)
            {


                prjm.Add(prj);
            }

            var sso = Json(result, JsonRequestBehavior.AllowGet);
            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;
        }



        [HttpPost]

        public ActionResult yorumlar(int projeid)
        {

            List<yorum> result = new List<yorum>();

            if (projeid != 0)
            {
              
                //result = db.projes.Where(x => x.proje_adi.Contains(projeadi)).ToList();
                result = db.yorums.Where(z=>z.yor_proje_id==projeid).ToList();
                result.Reverse();
            }



            var sonuc = Json(result, JsonRequestBehavior.AllowGet);
            return sonuc;
        }

        [HttpPost]

        public ActionResult yorumyap(int projeid,string yorum,string kullanici)
        {
            yorum yeni_yorum = new yorum();
            yeni_yorum.yorum_icerigi = yorum;
            yeni_yorum.yorum_tarihi = DateTime.Now;
            yeni_yorum.yor_proje_id = projeid;
            yeni_yorum.yorumu_yapan_id = kullanici;
            ApplicationUser user = db.Users.Where(x => x.UserName == kullanici).First();
            yeni_yorum.yorumu_yapan_statu = user.kullanici_turu;
            db.yorums.Add(yeni_yorum);
            db.SaveChanges();
            List<yorum> prjm = new List<yorum>();
            //prjm.Add(yeni_yorum);
            var result = db.yorums.Where(z => z.yorumu_yapan_id == kullanici && z.yor_proje_id==projeid).ToList();
            foreach(yorum a in result)
            {

                prjm.Add(a);  
            }

            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;

        }


        [HttpPost]

        public ActionResult begenmedim(string kullanici, int projeid)
        {

            begenme yeni = new begenme();
            yeni.begenenid = kullanici;
            yeni.begenmedurumu = "begenmedi";
            yeni.projeid = projeid;


            proje projem = db.projes.Where(x => x.proje_id == projeid).First();

            List<proje> prjm = new List<proje>();
            List<begenme> begeni = db.begenmes.Where(x => x.projeid == projeid && x.begenenid == kullanici && x.begenmedurumu == "begenmedi").ToList();

            if (begeni.Count != 0)
            {
                var son = Json(prjm, JsonRequestBehavior.AllowGet);
                return son;

            }
            else
            {
                List<begenme> begenmedi = db.begenmes.Where(x => x.projeid == projeid && x.begenenid == kullanici && x.begenmedurumu == "begendi").ToList();
                if (begenmedi.Count != 0)
                {
                    projem.proje_begeni_sayisi = projem.proje_begeni_sayisi - 1;

                    foreach (begenme z in begenmedi)
                    {
                        db.begenmes.Remove(z);
                    }
                    db.SaveChanges();

                    projem.proje_begeni_sayisi--;
                    db.begenmes.Add(yeni);
                    db.SaveChanges();
                }
                else
                {
                    projem.proje_begeni_sayisi--;
                    db.begenmes.Add(yeni);
                    db.SaveChanges();

                }

                proje a = db.projes.Where(x => x.proje_id == projeid).First();
                

                    prjm.Add(a);
                

                var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
                return sonuc;
            }









        }



        [HttpPost]

        public ActionResult ben(string kullanici ,int projeid)
        {
            var yorumlarim = db.yorums.Where(x=>x.yorumu_yapan_id==kullanici && x.yor_proje_id==projeid).ToList();


            List<yorum> prjm = new List<yorum>();
           
            foreach(yorum yeni in yorumlarim)
            {

                prjm.Add(yeni);
            }


            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;

        }

        [HttpPost]

        public ActionResult kullaniciara(string kullanici)
        {
            var kullanicim = db.Users.Where(x => x.UserName==kullanici).ToList();


            List<projeoneritakipsistemi.Models.ApplicationUser> prjm = new List<projeoneritakipsistemi.Models.ApplicationUser>();

            foreach (var yeni in kullanicim)
            {

                prjm.Add(yeni);
            }


            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;

        }
        [HttpPost]

        public ActionResult butonlar(string kullanici, int projeid)
        {
            var begenim = db.begenmes.Where(x => x.begenenid == kullanici && x.projeid == projeid).ToList();


            List<begenme> prjm = new List<begenme>();

            foreach (begenme yeni in begenim)
            {

                prjm.Add(yeni);
            }


            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;

        }


        //[HttpPost]

        //public ActionResult kullaniciadibul(string kullanici)
        //{
        //    var begenim = db.Users.Where(x => x.UserName == kullanici ).ToList();


        //    List<ApplicationUser> prjm = new List<ApplicationUser>();

        //    foreach (ApplicationUser yeni in begenim)
        //    {

        //        prjm.Add(yeni);
        //    }


        //    var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
        //    return sonuc;

        //}



        [HttpPost]

        public ActionResult begen(string kullanici, int projeid)

        {

            begenme yeni = new begenme();
            yeni.begenenid = kullanici;
            yeni.begenmedurumu = "begendi";
            yeni.projeid = projeid;
           

            proje projem = db.projes.Where(x => x.proje_id == projeid).First();

            List<proje> prjm = new List<proje>();
          List<  begenme> begeni = db.begenmes.Where(x => x.projeid==projeid && x.begenenid == kullanici && x.begenmedurumu=="begendi").ToList();

            if (begeni.Count != 0)
            {
                var son = Json(prjm, JsonRequestBehavior.AllowGet);
                return son;

            }
            else
            {
                List<begenme> begenmedi = db.begenmes.Where(x => x.projeid == projeid && x.begenenid == kullanici && x.begenmedurumu == "begenmedi").ToList();
                if (begenmedi.Count != 0) {
                    projem.proje_begeni_sayisi = projem.proje_begeni_sayisi + 1;

                    foreach (begenme z in begenmedi)
                    {
                        db.begenmes.Remove(z);
                    }
                    db.SaveChanges();

                    projem.proje_begeni_sayisi++;
                    db.begenmes.Add(yeni);
                    db.SaveChanges();
                }
                else
                {
                    projem.proje_begeni_sayisi++;
                    db.begenmes.Add(yeni);
                    db.SaveChanges();

                }

                proje a = db.projes.Where(z => z.proje_id == projeid).First();
                

                    prjm.Add(a);
                

                var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
                return sonuc;
            }
        }
        [HttpPost]

        public ActionResult vazgec(string kullanici, int projeid)

        {

            proje projem = db.projes.Where(x => x.proje_id == projeid).First();

            List<proje> prjm = new List<proje>();
        
          
            List<begenme> begenmedi = db.begenmes.Where(x => x.projeid == projeid && x.begenenid == kullanici && x.begenmedurumu == "begenmedi").ToList();
                if (begenmedi.Count != 0)
                {
                    projem.proje_begeni_sayisi = projem.proje_begeni_sayisi + 1;

                    foreach (begenme z in begenmedi)
                    {
                        db.begenmes.Remove(z);
                    }
                    db.SaveChanges();

                
                }

            List<begenme> begendi = db.begenmes.Where(x => x.projeid == projeid && x.begenenid == kullanici && x.begenmedurumu == "begendi").ToList();
            if (begendi.Count != 0)
            {
                projem.proje_begeni_sayisi = projem.proje_begeni_sayisi -1;

                foreach (begenme z in begendi)
                {
                    db.begenmes.Remove(z);
                }
                db.SaveChanges();


            }

            proje a = db.projes.Where(z => z.proje_id == projeid).First();


                prjm.Add(a);


                var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
                return sonuc;
            }
        




        // GET: projes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proje proje = db.projes.Find(id);
           
            if (proje == null)
            {
                return HttpNotFound();
            }
            return View(proje);
        }

        // GET: projes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: projes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "proje_id,proje_adi,proje_begeni_sayisi,proje_teslim_tarihi,proje_durumu,proje_aciklamasi,proje_turu,proje_kisi_siniri,proje_ogrenci_id,proje_akademisyen_id,proje_bolum_id,proje_diger_kul_id,proje_yayin_tarihi")] proje proje)
        {
            if (ModelState.IsValid)
            {
                proje.proje_begeni_sayisi = 0;
                proje.projeolusturanid = User.Identity.GetUserName();
                proje.proje_yayin_tarihi = DateTime.Now;
                ApplicationUser kullanici = db.Users.Where(x=>x.UserName==proje.projeolusturanid).First();


                if (kullanici.kullanici_turu == "ogrenci")
                {
                    proje.proje_durumu = "Öneri/Ögrenci Önerisi";
                }

                if (kullanici.kullanici_turu == "diger_kullanici")
                {
                    proje.proje_durumu = "Öneri/Misafir Kullanıcı Önerisi";
                }
                if (kullanici.kullanici_turu == "akademisyen")
                {
                    proje.proje_durumu = "Öneri/Akademisyen Önerisi";
                }

                if (kullanici.kullanici_turu == "sirket")
                {
                    proje.proje_durumu = "Öneri/Sirket Önerisi";
                }

                db.projes.Add(proje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proje);
        }

        // GET: projes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proje proje = db.projes.Find(id);
            if (proje == null)
            {
                return HttpNotFound();
            }
            return View(proje);
        }

        // POST: projes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "proje_id,proje_adi,proje_begeni_sayisi,proje_teslim_tarihi,proje_durumu,proje_aciklamasi,proje_turu,proje_kisi_siniri,proje_ogrenci_id,proje_akademisyen_id,proje_bolum_id,proje_diger_kul_id,proje_yayin_tarihi")] proje proje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proje);
        }

        // GET: projes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proje proje = db.projes.Find(id);
            if (proje == null)
            {
                return HttpNotFound();
            }
            return View(proje);
        }

        // POST: projes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            proje proje = db.projes.Find(id);
            db.projes.Remove(proje);
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
