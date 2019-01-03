using GymApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace GymApp.Services
{
	public class TicketsService
	{
		public static void CreateTicket(Ticket ticket, string id)
		{
			using (var context = new ApplicationDbContext())
			{
				var user = context.Users.SingleOrDefault(x => x.Id == id);
				ticket.ApplicationUserId = user.Id;

				var createdTicket = context.Tickets.Add(ticket);
				context.SaveChanges();
                user.TickedId = createdTicket.Id;
                context.SaveChanges();
            }
        }

		public static Ticket GetById(long id)
		{
			using (var context = new ApplicationDbContext())
			{
				return context.Tickets.Where(x => x.Id == id).SingleOrDefault();
			}
		}

        public static Ticket GetByUserId(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Tickets.FirstOrDefault(x => x.ApplicationUserId == userId);
            }
        }

        public static void Delete(long id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var ticketToRemove = context.Tickets.Where(x => x.Id == id).SingleOrDefault();

                context.Tickets.Remove(ticketToRemove);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.Where(x => x.Id == userId).SingleOrDefault();
                user.TickedId = null;
                context.SaveChanges();
            }

        }

        public static List<TicketType> GetTypes()
		{
			using (var context = new ApplicationDbContext())
			{
				return context.TicketTypes.ToList();
			}
		}
	}
}