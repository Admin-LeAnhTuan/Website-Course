using Course.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace Course.Controllers
{
    public class HomeController : Controller
    {
        ModelContext db = new ModelContext();
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