using Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace Course.Controllers
{
    public class HomeController : Controller
    {
        private ModelContext db = new ModelContext();
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.AspNetUser).Include(c => c.Category);
            return View(courses.ToList());
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