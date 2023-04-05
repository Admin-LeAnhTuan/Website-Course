using Course.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Course.Controllers
{
    public class HomeController : Controller
    {

        private ModelContext db = new ModelContext();
        public ActionResult Index()
        {
            var user_id = User.Identity.GetUserId();
            var payments = db.Payments.Where(p => p.users_id == user_id).ToList();
            var courses = db.Courses.Include(c => c.AspNetUser).Include(c => c.Category).ToList();

            for(int i = 0; i < courses.Count;i++)
            {
                int course_id = courses[i].course_id;
                var unit = db.Units.FirstOrDefault(u => u.course_id == course_id);
                if (unit != null)
                {
                    courses[i].firstUnit_id = unit.Unit_id;
                }

                for(int j = 0; j < payments.Count; j++)
                {
                    if (payments[j].course_id == courses[i].course_id )
                    {
                        courses[i].isBuyed = true;
                        break;
                    } else
                    {
                        courses[i].isBuyed = false;

                    }
                }
            }

            var categories = db.Categories.ToList();

            foreach(var category in categories)
            {
                category.lstCourse = courses.Where(c => c.category_id == category.category_id).ToList();
            }

            ViewBag.categories = categories;
            //foreach (var c in courses)
            //{
            //    foreach (var p in payments)
            //    {
            //        if (p.course_id == c.course_id && p.users_id == user_id)
            //        {
            //            c.isBuyed = true;
            //            break;
            //        } else
            //        {
            //            c.isBuyed = false;
            //        }
            //     }
            //    var unit = db.Units.FirstOrDefault(u => u.course_id == c.course_id);
            //    if(unit != null)
            //    {
            //        c.firstUnit_id = unit.Unit_id;          
            //    }
            //}
            return View(courses);
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