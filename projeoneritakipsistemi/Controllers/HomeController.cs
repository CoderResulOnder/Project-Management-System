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