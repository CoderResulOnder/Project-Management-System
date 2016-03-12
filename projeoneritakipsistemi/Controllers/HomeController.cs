using projeoneritakipsistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace projeoneritakipsistemi.Controllers
{
   
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


     

        public ActionResult Index()
        {








            ViewData["universiteler"] = db.universites.ToList();
            ViewData["fakulteler"] = db.fakultes.ToList();
            ViewData["bolumler"] = db.bolums.ToList();
            Session["users"] = db.Users.ToList();
            Session["projes"] = db.projes.ToList();
            ApplicationUser user = new ApplicationUser();
            string kullanici_emaili = User.Identity.GetUserName();
            if (kullanici_emaili != "")
            {
                user = db.Users.Where(x => x.Email == kullanici_emaili).First();

                if (user.kullanici_turu == "ogrenci")
                {
                    var kont = db.ogrencis.Where(x => x.ogrenci_email == kullanici_emaili).ToList();
                    if (kont.Count() == 0)
                    {
                        return RedirectToAction("Create", "ogrencis");
                    }
                    //return RedirectToAction("Create", "yazilimmuhs");

                }

                else if (user.kullanici_turu == "akademisyen")
                {
                    var akd = db.akademisyens.Where(x => x.akademisyen_mail == user.UserName).ToList();
                    if (akd.Count() == 0)
                        return RedirectToAction("Create", "akademisyens");
                    //return RedirectToAction("Create", "yazilimmuhs");

                }
                else if (user.kullanici_turu == "diger_kullanici")
                {
                    var dgr = db.diger_kullanicilar.Where(x => x.diger_kullanicilar_mail == user.UserName).ToList();
                    if (dgr.Count() == 0)
                        //return RedirectToAction("Create", "yazilimmuhs");
                        return RedirectToAction("Create", "diger_kullanicilar");
                }
                else if (user.kullanici_turu == "sirket")
                {

                    var srk = db.sirkets.Where(x => x.sirketi_ekleyen_kullaniciid == user.UserName).ToList();
                    if (srk.Count() == 0)
                        return RedirectToAction("Create", "sirkets");
                }

                Session["users"] = db.Users.ToList();





            }




            if (kullanici_emaili != "")
            {
                user = db.Users.Where(x => x.Email == kullanici_emaili).First();
                if (user.kullanici_turu == "ogrenci")
                {
                    //return RedirectToAction("Create", "yazilimmuhs");
                    return RedirectToAction("Index", "ogrenciHome");
                }

                else if (user.kullanici_turu == "akademisyen")
                {
                    return RedirectToAction("Index", "AkademisyenHome");
                    //return RedirectToAction("Create", "yazilimmuhs");

                }
                else if (user.kullanici_turu == "diger_kullanici")
                {
                    //return RedirectToAction("Create", "yazilimmuhs");
                    return RedirectToAction("Index", "DigerKullanicilarHome");
                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}