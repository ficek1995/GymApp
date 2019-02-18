using GymApp.Models;
using GymApp.Models.Dto;
using GymApp.Models.ViewsModels;
using GymApp.Services;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GymApp.Controllers
{
	public partial class HomeController : Controller
	{
		public virtual ActionResult Index()
		{
            using (var context = new ApplicationDbContext())
            {
               var entity = context.Messages.SingleOrDefault();
               if(entity == null)
                {
                    ViewBag.info = string.Empty; 

                }
                else
                {
                    ViewBag.info= entity.Text;
                }
            }
            return View();
		}

		public virtual ActionResult About()
		{
			ViewBag.Message = "O aplikacji";

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


        [AjaxOnly]
        public virtual ActionResult UserJoinedToLesson(long lessonId)
        {
            var joined = LessonsService.JoinedToLesson(lessonId, User.Identity.GetUserId());
            return Json(new { msg = joined }, JsonRequestBehavior.AllowGet);
        }

		public virtual ActionResult Contact()
		{

			ViewBag.Message = "Kontakt";

            var contacts = UserService.GetAll().Where(x => x.IsTrainer && x.Deleted == false).Select(x => new TrainerContactViewModel {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.PhoneNumber })
                .ToList();

            return View(MVC.Home.Views.Contact, contacts);
		}
	}
}