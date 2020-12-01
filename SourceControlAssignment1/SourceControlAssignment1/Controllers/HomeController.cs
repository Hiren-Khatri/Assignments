using System.Web.Mvc;

namespace SourceControlAssignment1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.DataModel std)
        {
            if (ModelState.IsValid)
            { //checking model state

                //update student to db

                return RedirectToAction("Index");
            }
            return View(std);
        }
    }
}