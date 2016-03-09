using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projeoneritakipsistemi.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace projeoneritakipsistemi.Controllers
{
    public class kaynaksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CloudQueue thumbnailRequestQueue;
        private static CloudBlobContainer imagesBlobContainer;

        public kaynaksController()
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


        // GET: kaynaks
        public async Task<ActionResult> Index()
        {
            // This code executes an unbounded query; don't do this in a production app,
            // it could return too many rows for the web app to handle. For an example
            // of paging code, see:
            // http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
            var adsList = db.kaynaks.AsQueryable();
           
            return View(await adsList.ToListAsync());
        }

        // GET: kaynaks/Details/5
  

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kaynak ad = await db.kaynaks.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }




        // GET: kaynaks/Create
        public ActionResult Create()
        {
            ViewBag.proje_id = new SelectList(db.projes, "proje_id", "proje_adi");
            return View();
        }

        // POST: kaynaks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "kaynak_id,kaynak_name,kaynak_aciklamasi,kaynak_tarih,kaynak_url,proje_id,kaynak_yukleyen_id,kaynak_yukleyen_statu")] kaynak kaynak, HttpPostedFileBase imageFile)

        {

            CloudBlockBlob imageBlob = null;
            // A production app would implement more robust input validation.
            // For example, validate that the image file size is not too large.
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength != 0)
                {
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    kaynak.kaynak_url = imageBlob.Uri.ToString();
                }
                kaynak.kaynak_tarih = DateTime.Now;
                db.kaynaks.Add(kaynak);
                await db.SaveChangesAsync();
                Trace.TraceInformation("Created AdId {0} in database", kaynak.kaynak_id);

                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = kaynak.kaynak_id, BlobUri = new Uri(kaynak.kaynak_url) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", kaynak.kaynak_id);
                }
                return RedirectToAction("Index");
              }

            ViewBag.proje_id = new SelectList(db.projes, "proje_id", "proje_adi", kaynak.proje_id);
            return View(kaynak);
        }

        // GET: kaynaks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kaynak ad = await db.kaynaks.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }


        // POST: kaynaks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "kaynak_id,kaynak_name,kaynak_aciklamasi,kaynak_tarih,kaynak_url,proje_id,kaynak_yukleyen_id,kaynak_yukleyen_statu")] kaynak kaynak,HttpPostedFileBase imageFile)
        {
            CloudBlockBlob imageBlob = null;
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength != 0)
                {
                    // User is changing the image -- delete the existing
                    // image blobs and then upload and save a new one.
                    await DeleteAdBlobsAsync(kaynak);
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    kaynak.kaynak_url= imageBlob.Uri.ToString();
                }
                db.Entry(kaynak).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Trace.TraceInformation("Updated AdId {0} in database", kaynak.kaynak_id);

                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = kaynak.kaynak_id, BlobUri = new Uri(kaynak.kaynak_url) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", kaynak.kaynak_id);
                }
                return RedirectToAction("Index");
            }
                ViewBag.proje_id = new SelectList(db.projes, "proje_id", "proje_adi", kaynak.proje_id);
            return View(kaynak);
        }

        // GET: kaynaks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kaynak ad = await db.kaynaks.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            kaynak ad = await db.kaynaks.FindAsync(id);

            await DeleteAdBlobsAsync(ad);

            db.kaynaks.Remove(ad);
            await db.SaveChangesAsync();
            Trace.TraceInformation("Deleted ad {0}", ad.kaynak_id);
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

        private async Task DeleteAdBlobsAsync(kaynak ad)
        {
            if (!string.IsNullOrWhiteSpace(ad.kaynak_url))
            {
                Uri blobUri = new Uri(ad.kaynak_url);
                await DeleteAdBlobAsync(blobUri);
            }
            if (!string.IsNullOrWhiteSpace(ad.kaynak_url))
            {
                Uri blobUri = new Uri(ad.kaynak_url);
                await DeleteAdBlobAsync(blobUri);
            }
        }

        private static async Task DeleteAdBlobAsync(Uri blobUri)
        {
            string blobName = blobUri.Segments[blobUri.Segments.Length - 1];
            Trace.TraceInformation("Deleting image blob {0}", blobName);
            CloudBlockBlob blobToDelete = imagesBlobContainer.GetBlockBlobReference(blobName);
            await blobToDelete.DeleteAsync();
        }

    }
}
