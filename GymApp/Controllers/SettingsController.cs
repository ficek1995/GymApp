using AutoMapper;
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

            using (var context = new ApplicationDbContext())
            {
                var entities = context.Messages.ToList();
                var model = Mapper.Map<List<MessageViewModel>>(entities);
                return View(MVC.Settings.Views.List, model);
            }
        }

		public virtual ActionResult Form()
		{
            using (var context = new ApplicationDbContext())
            {
                    return View(MVC.Settings.Views.Form, new MessageViewModel());
            }
        }

        [HttpPost]
        public virtual ActionResult Form(MessageViewModel model)
        {
            
            using (var context = new ApplicationDbContext())
            {
               var message =  new Message
                {
                    Text = model.Text,
                    Date = DateTime.Now
                };
                context.Messages.Add(message);
                context.SaveChanges();

            }
            return RedirectToAction(MVC.Settings.Index());

        }

        public virtual ActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var message = context.Messages.SingleOrDefault(x => x.Id == id);
                context.Messages.Remove(message);
                context.SaveChanges();

                return RedirectToAction(MVC.Settings.Index());
            }
        }
    }
}