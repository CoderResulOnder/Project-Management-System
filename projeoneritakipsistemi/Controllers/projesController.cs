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
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace projeoneritakipsistemi.Controllers
{
    public class projesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private CloudQueue thumbnailRequestQueue;

        private static CloudBlobContainer imagesBlobContainer;

        public projesController()
        {
            InitializeStorage();
        }

        private void InitializeStorage()
        {
            // Open storage account using credentials from .cscfg file.
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorageGenel"].ToString());

            // Get context object for working with blobs, and 
            // set a default retry policy appropriate for a web user interface.
            var blobClient = storageAccount.CreateCloudBlobClient();
            //blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            // Get a reference to the blob container.
            imagesBlobContainer = blobClient.GetContainerReference("resulonderblobs");

            // Get context object for working with queues, and 
            // set a default retry policy appropriate for a web user interface.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            //queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            // Get a reference to the queue.
            thumbnailRequestQueue = queueClient.GetQueueReference("thumbnailrequest");
        }

        private async Task<CloudBlockBlob> UploadAndSaveBlobAsync(HttpPostedFileBase imageFile)
        {
            Trace.TraceInformation("Uploading image file {0}", imageFile.FileName);

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            // Retrieve reference to a blob. 
            CloudBlockBlob imageBlob = imagesBlobContainer.GetBlockBlobReference(blobName);
            // Create the blob by uploading a local file.
            using (var fileStream = imageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }

            Trace.TraceInformation("Uploaded image file to {0}", imageBlob.Uri.ToString());

            return imageBlob;
        }

        private async Task DeleteAdBlobsAsync(string ad)
        {
            if (!string.IsNullOrWhiteSpace(ad))
            {
                Uri blobUri = new Uri(ad);
                await DeleteAdBlobAsync(blobUri);
            }

        }

        private static async Task DeleteAdBlobAsync(Uri blobUri)
        {
            //string blobName = Guid.NewGuid().ToString() + Path.GetExtension(blobUri.ToString().FileName);
            // Retrieve reference to a blob. 



            string blobName = blobUri.Segments[blobUri.Segments.Length - 1];
            Trace.TraceInformation("Deleting image blob {0}", blobName);
            CloudBlockBlob blobToDelete = imagesBlobContainer.GetBlockBlobReference(blobName);
            await blobToDelete.DeleteAsync();
        }





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


            List<projeler> prjm = new List<projeler>();
            projeler newprj;
            foreach (proje prj in result)
            {
                newprj = new projeler();
                newprj.akademisyen_id = prj.akademisyen_id;
                newprj.diger_kullanicilar_id = prj.diger_kullanicilar_id;
                newprj.bolum_id = prj.bolum_id;
                newprj.ogrenci_id = prj.ogrenci_id;
                newprj.projeolusturanid = prj.projeolusturanid;
                newprj.proje_aciklamasi = prj.proje_aciklamasi;
                newprj.proje_adi = prj.proje_adi;
                newprj.proje_begeni_sayisi = prj.proje_begeni_sayisi;
                newprj.proje_durumu = prj.proje_durumu;
                newprj.proje_id = prj.proje_id;
                newprj.proje_kisi_siniri = prj.proje_kisi_siniri;
                newprj.proje_teslim_tarihi = prj.proje_teslim_tarihi;
                newprj.proje_turu = prj.proje_turu;
                newprj.proje_yapimiyla_ilgili_oneri = prj.proje_yapimiyla_ilgili_oneri;
                newprj.proje_yayin_tarihi = prj.proje_yayin_tarihi;
              

                prjm.Add(newprj);
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

        public ActionResult begenmemesebebi(int projeid, string yorum, string kullanici)
        {
            var begenme = db.begenmes.Where(x => x.begenenid == kullanici && x.projeid == projeid).First();
            begenme.begenmemesebebi = yorum;

            db.Entry(begenme).State = EntityState.Modified;

            db.SaveChanges();

            List<begenme> prjm = new List<begenme>();
            //prjm.Add(yeni_yorum);
            

                prjm.Add(begenme);
            

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
        [Authorize]
        // GET: projes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: projes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "proje_id,proje_adi,proje_begeni_sayisi,proje_yapimiyla_ilgili_oneri,proje_teslim_tarihi,proje_durumu,proje_aciklamasi,proje_turu,proje_kisi_siniri,proje_ogrenci_id,proje_akademisyen_id,proje_bolum_id,proje_diger_kul_id,proje_yayin_tarihi")] proje proje, HttpPostedFileBase new_kaynak)
        {
            if (ModelState.IsValid)
            {

               

                proje.proje_begeni_sayisi = 0;
                proje.projeolusturanid = User.Identity.GetUserName();


                proje.proje_yayin_tarihi= DateTime.Now;

                if (User.Identity.GetUserName() == null) { ViewBag.message = "no_user"; return View(); }
                ApplicationUser kullanici = db.Users.Where(x=>x.UserName==proje.projeolusturanid).FirstOrDefault();


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
                await db.SaveChangesAsync();

                CloudBlockBlob imageBlob = null;
                projeoneritakipsistemi.Models.kaynak yenikaynak = new kaynak();

                if (new_kaynak != null && new_kaynak.ContentLength != 0)
                {
                   
                    imageBlob = await UploadAndSaveBlobAsync(new_kaynak);
                    yenikaynak.kaynak_url = imageBlob.Uri.ToString();
                    yenikaynak.kaynak_tarih = DateTime.Now;
                    yenikaynak.kaynak_aciklamasi = "proje_oluşturma sırasında eklenen kaynak";
                    yenikaynak.kaynak_name = "proje tanımı ve acıklaması";
                    yenikaynak.kaynak_yukleyen_id = User.Identity.GetUserName();
                    yenikaynak.proje_id = proje.proje_id;

                    db.kaynaks.Add(yenikaynak);
                    await db.SaveChangesAsync();
                    proje.kaynaks.Add(yenikaynak);
                    db.Entry(proje).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                }
                Trace.TraceInformation("Created AdId {0} in database",yenikaynak.kaynak_url );
                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = yenikaynak.kaynak_id, BlobUri = new Uri(yenikaynak.kaynak_url) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", yenikaynak.kaynak_id);

                }

               


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
        public ActionResult Edit([Bind(Include = "proje_id,proje_adi,proje_begeni_sayisi,proje_teslim_tarihi,proje_yapimiyla_ilgili_oneri,proje_durumu,proje_aciklamasi,proje_turu,proje_kisi_siniri,proje_ogrenci_id,proje_akademisyen_id,proje_bolum_id,proje_diger_kul_id,proje_yayin_tarihi")] proje proje, HttpPostedFileBase new_kaynak)
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
