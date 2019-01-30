using GymApp.Helpers;
using GymApp.Models;
using GymApp.Models.Dto;
using GymApp.Models.ViewsModels;
using GymApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GymApp.Controllers
{
	public partial class SettingsController : Controller
	{
		public virtual ActionResult Index()
		{
			return RedirectToAction(MVC.Settings.Form());
		}

		public virtual ActionResult Form()
		{
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Messages.SingleOrDefault();
                if(entity != null)
                {
                    return View(MVC.Settings.Views.Form, entity.MapTo<MessageViewModel>());
                }
                else
                {
                    return View(MVC.Settings.Views.Form, new MessageViewModel());
                }
            }
        }

        [HttpPost]
        public virtual ActionResult Form(MessageViewModel model)
        {
            
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Messages.SingleOrDefault();
                if (entity != null)
                {
                    entity.Text = model.Text;
                    entity.Date = DateTime.Now;
                    context.SaveChanges();
                    return View(MVC.Settings.Views.Form, entity.MapTo<MessageViewModel>());
                }
                else
                {
                    var message = model.MapTo<Message>();
                    message.Date = DateTime.Now; 
                    context.Messages.Add(message);
                    context.SaveChanges();
                }
            }
            return View(MVC.Settings.Views.Form,model);
        }


    }
}