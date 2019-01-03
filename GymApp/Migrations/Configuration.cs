using GymApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace GymApp.Migrations
{
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<GymApp.Models.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}


		protected override void Seed(GymApp.Models.ApplicationDbContext context)
		{
			if (!context.Roles.Any(r => r.Name == "Administrator"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "Administrator" };
                manager.Create(role);

			}

            if (!context.Roles.Any(r => r.Name == "Trener"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                 var trainerRole = new IdentityRole {Name = "Trener" };
                manager.Create(trainerRole);

            }

			var ticketTypes = new List<TicketType>();
			ticketTypes.Add(new TicketType
			{
				Name = "Miesiêczny",
				TicketCategory = TicketTypeEnum.Monthly,
				Price = 100
			});

			ticketTypes.Add(new TicketType
			{
				Name = "Dzienny",
				TicketCategory = TicketTypeEnum.Daily,
				Price = 20
			});

			ticketTypes.Add(new TicketType
			{
				Name = "Tygodniowy",
				TicketCategory = TicketTypeEnum.Weekly,
				Price = 70
			});

			if (!context.TicketTypes.Any())
			{
				context.TicketTypes.AddRange(ticketTypes);
				context.SaveChanges();
			}
			if (!context.Users.Any(u => u.UserName == "admin@gmail.com"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", LastName = "admin", FirstName = "admin",  IsTrainer = false};

				manager.Create(user, "zaq1@WSX");
				manager.AddToRole(user.Id, "Administrator");
			}
		}

	}
}
