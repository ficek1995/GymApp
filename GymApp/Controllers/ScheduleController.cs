using System.Web.Mvc;

namespace GymApp.Controllers
{
	public partial class ScheduleController : Controller
	{
		public virtual ActionResult Index()
		{
            return RedirectToAction(MVC.Schedule.Calendar());
		}

		public virtual ActionResult Calendar()
		{
			return View(MVC.Schedule.Views.Calendar);
		}

	}
}