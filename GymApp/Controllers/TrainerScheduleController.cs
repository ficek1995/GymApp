using AutoMapper;
using GymApp.Models;
using GymApp.Models.Dto;
using GymApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GymApp.Controllers
{
    [Authorize(Roles = "Trener")]
	public partial class TrainerScheduleController : Controller
	{
		public virtual ActionResult Index()
		{
            return RedirectToAction(MVC.TrainerSchedule.Schedule());
		}

        public virtual ActionResult Schedule()
        {
            return View(MVC.TrainerSchedule.Views.ViewNames.Edit);
        }

        public virtual JsonResult GetEvents()
        {
            var lessonsResponse = new List<LessonResponse>();
            var lessons = LessonsService.GetList();
            foreach (var item in lessons)
            {
                var user = UserService.Get(item.UserId);
                var userName =$"{user.FirstName} {user.LastName}";
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

        public virtual JsonResult CanDelete(long id)
        {
            var lesson = LessonsService.GetById(id);
            if(lesson.UserId == System.Web.HttpContext.Current.User.Identity.GetUserId())
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult Delete(long id)
        {
            LessonsService.Delete(id);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult CreateLesson(LessonDTO lesson)
        {
            var entity = Mapper.Map<Lesson>(lesson);

            if (lesson.start >= DateTime.Now)
            {
                LessonsService.Add(entity, System.Web.HttpContext.Current.User.Identity.GetUserId());
                return Json(true);
            }
            return Json(false);
        }

    }
}