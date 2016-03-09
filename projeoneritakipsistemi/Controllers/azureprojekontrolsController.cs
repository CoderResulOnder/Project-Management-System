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
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Configuration;

using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace projeoneritakipsistemi.Controllers
{
    [Authorize]
    public class azureprojekontrolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private CloudQueue thumbnailRequestQueue;
        private static CloudBlobContainer imagesBlobContainer;
        // GET: yazilimmuhs
        public azureprojekontrolsController()
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

        // GET: azureprojekontrols
        public async Task<ActionResult> Index()
        {
            Session["users"] = db.Users.ToList();
            return View(await db.azureprojekontrols.ToListAsync());
        }

        // GET: azureprojekontrols/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            azureprojekontrol azureprojekontrol = await db.azureprojekontrols.FindAsync(id);
            if (azureprojekontrol == null)
            {
                return HttpNotFound();
            }
            return View(azureprojekontrol);
        }

        // GET: azureprojekontrols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: azureprojekontrols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "odevid,kullanici,bulutSistemleriblogurl,sanalmakineblogurl,webservisleriblogurl,storageblogurl,kullanmadiginizbulutservisiblogurl,dataset,MachineLearningProjeadi,projeaciklama,projegrupuyeleri,kullanilanmodeltanit,bitmisproje,teslimtarihi")] azureprojekontrol azureprojekontrol, HttpPostedFileBase File, HttpPostedFileBase File1)
        {
            if (ModelState.IsValid)
            {

                azureprojekontrol.kullanici = User.Identity.GetUserName();
                CloudBlockBlob imageBlob = null;
                CloudBlockBlob imageBlob1 = null;

                if (File != null && File.ContentLength != 0)
                {
                    imageBlob = await UploadAndSaveBlobAsync(File);
                    azureprojekontrol.dataset = imageBlob.Uri.ToString();
                }

                if (File1 != null && File1.ContentLength != 0)
                {
                    imageBlob1 = await UploadAndSaveBlobAsync(File1);
                    azureprojekontrol.bitmisproje = imageBlob1.Uri.ToString();
                }


                azureprojekontrol.teslimtarihi = DateTime.Now;
                db.azureprojekontrols.Add(azureprojekontrol);
                await db.SaveChangesAsync();


                Trace.TraceInformation("Created AdId {0} in database", azureprojekontrol.odevid);
                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = azureprojekontrol.odevid, BlobUri = new Uri(azureprojekontrol.dataset) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", azureprojekontrol.odevid);

                }
                if (imageBlob1 != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId =azureprojekontrol.odevid, BlobUri = new Uri(azureprojekontrol.bitmisproje) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", azureprojekontrol.odevid);

                }


                return RedirectToAction("Index");
            }

            return View(azureprojekontrol);
        }

        // GET: azureprojekontrols/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            azureprojekontrol azureprojekontrol = await db.azureprojekontrols.FindAsync(id);
            if (azureprojekontrol == null)
            {
                return HttpNotFound();
            }
            return View(azureprojekontrol);
        }

        // POST: azureprojekontrols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "odevid,kullanici,bulutSistemleriblogurl,sanalmakineblogurl,webservisleriblogurl,storageblogurl,kullanmadiginizbulutservisiblogurl,dataset,MachineLearningProjeadi,projeaciklama,projegrupuyeleri,kullanilanmodeltanit,bitmisproje,teslimtarihi")] azureprojekontrol azureprojekontrol, HttpPostedFileBase File, HttpPostedFileBase File1)
        {
            if (ModelState.IsValid)
            {
                azureprojekontrol.kullanici = User.Identity.GetUserName();

                CloudBlockBlob imageBlob = null;
                CloudBlockBlob imageBlob1 = null;
               
                if (File != null && File.ContentLength != 0)
                {
                    await DeleteAdBlobsAsync(azureprojekontrol.dataset);
                    imageBlob = await UploadAndSaveBlobAsync(File);
                    azureprojekontrol.dataset = imageBlob.Uri.ToString();
                }

                if (File1 != null && File1.ContentLength != 0)
                {
                    await DeleteAdBlobsAsync(azureprojekontrol.bitmisproje);
                    imageBlob1 = await UploadAndSaveBlobAsync(File1);
                    azureprojekontrol.bitmisproje = imageBlob1.Uri.ToString();
                }




                azureprojekontrol.teslimtarihi = DateTime.Now;

                db.Entry(azureprojekontrol).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if (imageBlob != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = azureprojekontrol.odevid, BlobUri = new Uri(azureprojekontrol.dataset) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", azureprojekontrol.odevid);

                }
                if (imageBlob1 != null)
                {
                    BlobInformation blobInfo = new BlobInformation() { AdId = azureprojekontrol.odevid, BlobUri = new Uri(azureprojekontrol.bitmisproje) };
                    var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(blobInfo));
                    await thumbnailRequestQueue.AddMessageAsync(queueMessage);
                    Trace.TraceInformation("Created queue message for AdId {0}", azureprojekontrol.odevid);

                }



                return RedirectToAction("Index");
            }
            return View(azureprojekontrol);
        }

        // GET: azureprojekontrols/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            azureprojekontrol azureprojekontrol = await db.azureprojekontrols.FindAsync(id);
            if (azureprojekontrol == null)
            {
                return HttpNotFound();
            }
            return View(azureprojekontrol);
        }

        // POST: azureprojekontrols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            azureprojekontrol azureprojekontrol = await db.azureprojekontrols.FindAsync(id);
            db.azureprojekontrols.Remove(azureprojekontrol);
            await db.SaveChangesAsync();

            if (azureprojekontrol.dataset != null)
            {
                await DeleteAdBlobsAsync(azureprojekontrol.dataset);
            }
            if (azureprojekontrol.bitmisproje != null)
            {
                await DeleteAdBlobsAsync(azureprojekontrol.bitmisproje);
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
