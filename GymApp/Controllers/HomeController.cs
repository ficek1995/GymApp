using GymApp.Models.Dto;
using GymApp.Services;
using System.Collections.Generic;
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

        public virtual JsonResult GetEvents()
        {
            var lessonsResponse = new List<LessonResponse>();
            var lessons = LessonsService.GetList();
            foreach (var item in lessons)
            {
                var user = UserService.Get(item.UserId);
                var userName = $"{user.FirstName} {user.LastName}";
                var color = user.Color;
                lessonsResponse.Add(new LessonResponse
                {
                    id = item.Id,
                    editable = false,
                    title = $"{userName} - {item.Title}",
                    start = item.Start.ToString("s"),
                    end = item.End.Value.ToString("s"),
                    color = color

                });
            }
            return Json(lessonsResponse, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Schedule()
        {
            return View(MVC.Schedule.Views.Calendar);
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