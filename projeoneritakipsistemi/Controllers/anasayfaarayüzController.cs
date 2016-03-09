using projeoneritakipsistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projeoneritakipsistemi.Controllers
{
    public class anasayfaarayüzController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: anasayfaarayüz
        public ActionResult Index()
        {
                       
            ViewData["universiteler"] = db.universites.ToList();
            ViewData["fakulteler"] = db.fakultes.ToList();
            ViewData["bolumler"] = db.bolums.ToList();
            Session["users"] = db.Users.ToList();
            return View();
        }
    }
}