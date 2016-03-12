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
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

namespace projeoneritakipsistemi.Controllers
{
    public class ogrencisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CloudQueue thumbnailRequestQueue;
        private static CloudBlobContainer imagesBlobContainer;

        public ogrencisController()
        {
            InitializeStorage();
        }

        private void InitializeStorage()
        {
            // Open storage account using credentials from .cscfg file.
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());

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



        // GET: ogrencis
        public ActionResult Index()
        {
            var ogrencis = db.ogrencis.Include(o => o.bolum).Include(o => o.fakulte);
            return View(ogrencis.ToList());
        }


        [HttpPost]

        public ActionResult arama(string ogrenci, string arama)
        {

            List<ogrenci> result = new List<ogrenci>();

            if (arama == "ogrenci_tc")
            {

                if (ogrenci == "")
                {
                    result = db.ogrencis.ToList();
                    var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                    return sonuc1;
                    
                   
                }
                //result = db.projes.Where(x => x.proje_adi.Contains(projeadi)).ToList();
                result = db.ogrencis.Where(x => x.ogrenci_tc.StartsWith(ogrenci)).ToList();
            }
            else if (arama == "ogrenci_no")
            {
                if (ogrenci == "")
                {
                    result = db.ogrencis.ToList();
                    var sonuc2 = Json(result, JsonRequestBehavior.AllowGet);
                    return sonuc2;
                    
                }
                result = db.ogrencis.Where(x => x.ogrenci_no.StartsWith(ogrenci)).ToList();
            }


            List<ogrenci> prjm = new List<ogrenci>();

            foreach (ogrenci prj in result)
            {


                prjm.Add(prj);
            }

            //var sso=Json(result, JsonRequestBehavior.AllowGet);
            var sonuc = Json(prjm, JsonRequestBehavior.AllowGet);
            return sonuc;
        }


        // GET: ogrencis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ogrenci ogrenci = db.ogrencis.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // GET: ogrencis/Create
        public ActionResult Create()
        {
            ViewData["universiteler"] = db.universites.ToList();
            ViewData["fakulteler"] = db.fakultes.ToList();
            ViewData["bolumler"] = db.bolums.ToList();
            Session["users"] = db.Users.ToList();
      
            ViewBag.bolum_id = new SelectList(db.bolums, "bolum_id", "bolum_adi");
            ViewBag.fakulte_id = new SelectList(db.fakultes, "fakulte_id", "fakulte_adi");
            
            return View();
        }

        // POST: ogrencis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ogrenci_id,ogrenci_adi,ogrenci_soyadi,ogrenci_no,ogrenci_adresi,ogrenci_tel,ogrenci_email,ogrenci_tc,ogrenci_kullanici_adi,ogrenci_parola,ogrenci_sinif,calısma_grub_id,bolum_id,fakulte_id,kayit_tarihi")] ogrenci ogrenci, HttpPostedFileBase ogrenci_profil_foto)
        {
            if (ModelState.IsValid)
            {
                CloudBlockBlob imageBlob = null;


                if (ogrenci_profil_foto != null && ogrenci_profil_foto.ContentLength != 0)
                {
                    imageBlob = await UploadAndSaveBlobAsync(ogrenci_profil_foto);
                    ogrenci.ogrenci_resimurl = imageBlob.Uri.ToString();
                }


                int bid = (int)Session["ogrencibolumid"];
                int aa = (int)Session["fid"];

                if (bid != 0)
                {
                    //ApplicationUser user = (ApplicationUser)Session["mevcut_kullanici"];
                    //var parola = Session["parola_acık"];
                    ApplicationUser user = new ApplicationUser();
                    string kullanici_emaili = User.Identity.GetUserName();
                    user = db.Users.Where(x => x.Email ==kullanici_emaili ).First();
                    
                    ogrenci.fakulte_id = (int)Session["fid"];

                    ogrenci.kayit_tarihi = DateTime.Now;
                    
                    ogrenci.bolum_id = bid;
                    ogrenci.ogrenci_kullanici_adi = user.kullaniciadi;
                    ogrenci.ogrenci_email = user.Email;
                    ogrenci.ogrenci_parola = user.PasswordHash;
                    //ogrenci.bolum = db.bolums.Where(m => m.bolum_id == bid).First();
                    //ogrenci.fakulte = db.fakultes.Where(m => m.fakulte_id ==aa ).First();
                    db.ogrencis.Add(ogrenci);
                    await db.SaveChangesAsync();
                    Trace.TraceInformation("Created AdId {0} in database", ogrenci.ogrenci_resimurl);
                    if (imageBlob != null)
                    {
                        BlobInformation blobInfo = new BlobInformation() { AdId = ogrenci.ogrenci_id, BlobUri = new Uri(ogrenci.ogrenci_resimurl) };
                        var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                        await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                        Trace.TraceInformation("Created queue message for AdId {0}", ogrenci.ogrenci_id);

                    }



                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Home");
                }


                return RedirectToAction("Create");
                
            }

            ViewBag.bolum_id = new SelectList(db.bolums, "bolum_id", "bolum_adi", ogrenci.bolum_id);
            ViewBag.fakulte_id = new SelectList(db.fakultes, "fakulte_id", "fakulte_adi", ogrenci.fakulte_id);
            return View(ogrenci);
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


           

            List<bolum> result = new List<bolum>();

            if (fakulteid == 0)
            {


                var sonuc1 = Json(result, JsonRequestBehavior.AllowGet);
                return sonuc1;


            }

            Session["fid"] = fakulteid;

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


            //ogrenci a = new ogrenci();

            //a.bolum_id = bolumid;
            Session["ogrencibolumid"] = bolumid;


            List<SelectListItem> fkt = new List<SelectListItem>();

            var sso = Json(fkt, JsonRequestBehavior.AllowGet);
            return sso;
        }


        // GET: ogrencis/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ogrenci ogrenci = await db.ogrencis.FindAsync(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            ViewBag.bolum_id = new SelectList(db.bolums, "bolum_id", "bolum_adi", ogrenci.bolum_id);
            ViewBag.fakulte_id = new SelectList(db.fakultes, "fakulte_id", "fakulte_adi", ogrenci.fakulte_id);
            return View(ogrenci);
        }

        // POST: ogrencis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ogrenci_id,ogrenci_adi,ogrenci_soyadi,ogrenci_no,ogrenci_adresi,ogrenci_tel,ogrenci_email,ogrenci_tc,ogrenci_kullanici_adi,ogrenci_parola,ogrenci_sinif,calısma_grub_id,bolum_id,fakulte_id,kayit_tarihi")] ogrenci ogrenci, HttpPostedFileBase ogrenci_profil_foto_edit)
        {
            if (ModelState.IsValid)
            {
                CloudBlockBlob imageBlob = null;
                if (ogrenci_profil_foto_edit != null && ogrenci_profil_foto_edit.ContentLength != 0)
                {
                    await DeleteAdBlobsAsync(ogrenci.ogrenci_resimurl);
                    imageBlob = await UploadAndSaveBlobAsync(ogrenci_profil_foto_edit);
                    ogrenci.ogrenci_resimurl = imageBlob.Uri.ToString();
                }

                db.Entry(ogrenci).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = ogrenci.ogrenci_id, BlobUri = new Uri(ogrenci.ogrenci_resimurl) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", ogrenci.ogrenci_id);

                }



                return RedirectToAction("Index");
            }
            ViewBag.bolum_id = new SelectList(db.bolums, "bolum_id", "bolum_adi", ogrenci.bolum_id);
            ViewBag.fakulte_id = new SelectList(db.fakultes, "fakulte_id", "fakulte_adi", ogrenci.fakulte_id);
            return View(ogrenci);
        }

        // GET: ogrencis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ogrenci ogrenci = await db.ogrencis.FindAsync(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // POST: ogrencis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ogrenci ogrenci = await db.ogrencis.FindAsync(id);
            db.ogrencis.Remove(ogrenci);
            await db.SaveChangesAsync();
            if (ogrenci.ogrenci_resimurl != null)
            {
                await DeleteAdBlobsAsync(ogrenci.ogrenci_resimurl);
            }

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


    }
}
