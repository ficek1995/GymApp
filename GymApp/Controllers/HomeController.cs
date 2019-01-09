using GymApp.Models.Dto;
using GymApp.Services;
using Microsoft.AspNet.Identity;
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
                var usercount = LessonsService.GetLessonUsersCount(item.Id);
                var user = UserService.Get(item.UserId);
                var userName = $"{user.FirstName} {user.LastName}";
                var color = user.Color;
                lessonsResponse.Add(new LessonResponse
                {
                    id = item.Id,
                    editable = false,
                    title = $"{userName} - {item.Title} \n Na zajęcia zapisało się {usercount} osób",
                    start = item.Start.ToString("s"),
                    end = item.End.Value.ToString("s"),
                    color = color,
                });
            }
            return Json(lessonsResponse, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Schedule()
        {
            return View(MVC.Schedule.Views.Calendar);
        }

        [AjaxOnly]
        public virtual ActionResult JoinLesson(long lessonId)
        {
            LessonsService.JoinToLesson(lessonId, User.Identity.GetUserId());
            return Json(true, JsonRequestBehavior.AllowGet);
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