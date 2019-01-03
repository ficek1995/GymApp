using System.Web.Mvc;

namespace GymApp.Controllers
{
	public partial class HomeController : Controller
	{
		public virtual ActionResult Index()
		{
			return View();
		}

		public virtual ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public virtual ActionResult Payments()
		{

			return View(MVC.Home.Views.Payments);
		}

		public virtual ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}