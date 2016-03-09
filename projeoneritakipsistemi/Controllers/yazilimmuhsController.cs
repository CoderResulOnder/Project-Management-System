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
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNet.Identity;

namespace projeoneritakipsistemi.Controllers
{
    [Authorize]
    public class yazilimmuhsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CloudQueue thumbnailRequestQueue;
        private static CloudBlobContainer imagesBlobContainer;
        // GET: yazilimmuhs
        public yazilimmuhsController()
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





        public async Task<ActionResult> Index()
        {
            Session["users"] = db.Users.ToList();
            return View(await db.yazilimmuhs.ToListAsync());
        }

        // GET: yazilimmuhs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yazilimmuh yazilimmuh = await db.yazilimmuhs.FindAsync(id);
            if (yazilimmuh == null)
            {
                return HttpNotFound();
            }
            return View(yazilimmuh);
        }

        // GET: yazilimmuhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: yazilimmuhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "projeid,Projeadi,projeaciklama,projegrupuyeleri,projedil,projeide,surumkontsisadi,teslimtarihi,baclocimgurl,sprintimgurl,checkinimgurl,youtubecalismavideourl")] yazilimmuh yazilimmuh, HttpPostedFileBase imageFile, HttpPostedFileBase imageFile1, HttpPostedFileBase imageFile2)
        {




            if (ModelState.IsValid)
            {


                yazilimmuh.kullaniciadi = User.Identity.GetUserName();
                CloudBlockBlob imageBlob = null;
                CloudBlockBlob imageBlob1 = null;
                CloudBlockBlob imageBlob2 = null;
                if (imageFile != null && imageFile.ContentLength != 0)
                {
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    yazilimmuh.baclocimgurl = imageBlob.Uri.ToString();
                }

                if (imageFile1 != null && imageFile1.ContentLength != 0)
                {
                    imageBlob1 = await UploadAndSaveBlobAsync(imageFile1);
                    yazilimmuh.sprintimgurl = imageBlob1.Uri.ToString();
                }
                if (imageFile2 != null && imageFile2.ContentLength != 0)
                {
                    imageBlob2 = await UploadAndSaveBlobAsync(imageFile2);
                    yazilimmuh.checkinimgurl = imageBlob2.Uri.ToString();
                }
                yazilimmuh.teslimtarihi = DateTime.Now;
                db.yazilimmuhs.Add(yazilimmuh);
                await db.SaveChangesAsync();
                Trace.TraceInformation("Created AdId {0} in database", yazilimmuh.projeid);
                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = yazilimmuh.projeid, BlobUri = new Uri(yazilimmuh.baclocimgurl) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", yazilimmuh.projeid);

                }
                if (imageBlob1 != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = yazilimmuh.projeid, BlobUri = new Uri(yazilimmuh.sprintimgurl) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", yazilimmuh.projeid);

                }
                if (imageBlob2 != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = yazilimmuh.projeid, BlobUri = new Uri(yazilimmuh.checkinimgurl) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", yazilimmuh.projeid);

                }

                return RedirectToAction("Index");
            }

            return View(yazilimmuh);
        }

        // GET: yazilimmuhs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yazilimmuh yazilimmuh = await db.yazilimmuhs.FindAsync(id);
            if (yazilimmuh == null)
            {
                return HttpNotFound();
            }
            return View(yazilimmuh);
        }

        // POST: yazilimmuhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "projeid,Projeadi,projeaciklama,projegrupuyeleri,projedil,projeide,surumkontsisadi,teslimtarihi,baclocimgurl,sprintimgurl,checkinimgurl,youtubecalismavideourl")] yazilimmuh yazilimmuh, HttpPostedFileBase imageFile, HttpPostedFileBase imageFile1, HttpPostedFileBase imageFile2)
        {
            if (ModelState.IsValid)
            {

                yazilimmuh.kullaniciadi = User.Identity.GetUserName();

                CloudBlockBlob imageBlob = null;
                CloudBlockBlob imageBlob1 = null;
                CloudBlockBlob imageBlob2 = null;
                if (imageFile != null && imageFile.ContentLength != 0)
                {
                    await DeleteAdBlobsAsync(yazilimmuh.baclocimgurl);
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    yazilimmuh.baclocimgurl = imageBlob.Uri.ToString();
                }

                if (imageFile1 != null && imageFile1.ContentLength != 0)
                {
                    await DeleteAdBlobsAsync(yazilimmuh.sprintimgurl);
                    imageBlob1 = await UploadAndSaveBlobAsync(imageFile1);
                    yazilimmuh.sprintimgurl = imageBlob1.Uri.ToString();
                }
                if (imageFile2 != null && imageFile2.ContentLength != 0)
                {
                    await DeleteAdBlobsAsync(yazilimmuh.checkinimgurl);
                    imageBlob2 = await UploadAndSaveBlobAsync(imageFile2);
                    yazilimmuh.checkinimgurl = imageBlob2.Uri.ToString();
                }
                yazilimmuh.teslimtarihi = DateTime.Now;
                db.Entry(yazilimmuh).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = yazilimmuh.projeid, BlobUri = new Uri(yazilimmuh.baclocimgurl) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", yazilimmuh.projeid);
                   
                }
                if (imageBlob1 != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = yazilimmuh.projeid, BlobUri = new Uri(yazilimmuh.sprintimgurl) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", yazilimmuh.projeid);

                }
                if (imageBlob2 != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = yazilimmuh.projeid, BlobUri = new Uri(yazilimmuh.checkinimgurl) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", yazilimmuh.projeid);

                }

                return RedirectToAction("Index");
            }
            return View(yazilimmuh);
        }

        // GET: yazilimmuhs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yazilimmuh yazilimmuh = await db.yazilimmuhs.FindAsync(id);
            if (yazilimmuh == null)
            {
                return HttpNotFound();
            }
            return View(yazilimmuh);
        }

        // POST: yazilimmuhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            yazilimmuh yazilimmuh = await db.yazilimmuhs.FindAsync(id);
            db.yazilimmuhs.Remove(yazilimmuh);
            await db.SaveChangesAsync();

            if (yazilimmuh.baclocimgurl != null)
            {
                await DeleteAdBlobsAsync(yazilimmuh.baclocimgurl);
            }
            if (yazilimmuh.sprintimgurl != null)
            {
                await DeleteAdBlobsAsync(yazilimmuh.sprintimgurl);
            }
            if (yazilimmuh.checkinimgurl != null)
            {
                await DeleteAdBlobsAsync(yazilimmuh.checkinimgurl);
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
