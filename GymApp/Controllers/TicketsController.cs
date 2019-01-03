using GymApp.Models;
using GymApp.Models.ViewsModels;
using GymApp.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GymApp.Controllers
{
	[Authorize(Roles = "Administrator")]
	public partial class TicketsController : Controller
	{

		// GET: Tickets/Details/5
		public virtual ActionResult Edit(string userId)
		{
			var user = UserService.Get(userId);
			var model = new TicketViewModel();
			var types = TicketsService.GetTypes();

			if (user.TickedId != null)
			{
                var ticket = TicketsService.GetById(user.TickedId.Value);
                var ticketCategory = types.Where(x => x.Id == ticket.TicketTypeId).Single().Name;
                model.Username = user.UserName;
                model.UserId = user.Id;
                model.TicketId = ticket.Id;
				model.AssignTime = ticket.AssignTime;
				model.AssignTo = ticket.AssignTo;
                model.TicketTypeName = ticketCategory;
			}

			model.TicketTypes =  types;
			return View(MVC.Tickets.Views.Edit, model);
		}

        [HttpPost]
        public virtual ActionResult Edit(TicketViewModel ticket)
        {
            if(ticket.TicketId == 0)
            {
                DateTime? assignedToDate = null;
                var types = TicketsService.GetTypes();
                var ticketCategory = types.Where(x => x.Id == ticket.TicketTypeId).Single().TicketCategory;

                switch (ticketCategory)
                {
                    case TicketTypeEnum.Daily:
                        assignedToDate = DateTime.Now.AddDays(1);
                        break;

                    case TicketTypeEnum.Weekly:
                        assignedToDate = DateTime.Now.AddDays(7);
                        break;

                    case TicketTypeEnum.Monthly:
                        assignedToDate = DateTime.Now.AddMonths(1);
                        break;
                }
                if(assignedToDate != null)
                {
                    var ticketModel = new Ticket
                    {
                        ApplicationUserId = ticket.UserId,
                        AssignTime = DateTime.Now,
                        TicketTypeId = ticket.TicketTypeId,
                        AssignTo = assignedToDate.Value
                    
                    };
                    TicketsService.CreateTicket(ticketModel, ticketModel.ApplicationUserId);
                    return RedirectToAction(MVC.Users.Index());
                }
            }
            else
            {
                TicketsService.Delete(ticket.TicketId, ticket.UserId);
            }
            return RedirectToAction(MVC.Users.Index());
        }


        // GET: Tickets/Delete/5
        public virtual ActionResult Delete(int id)
		{
			return View();
		}
        
	}
}
