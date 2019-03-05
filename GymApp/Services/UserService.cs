using GymApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace GymApp.Services
{
	public class UserService
	{
		public static List<ApplicationUser> GetAll()
		{
			using (var context = new ApplicationDbContext())
			{
				return context.Users.Where(x => x.Deleted == false).ToList();
			}
		}

		public static ApplicationUser Get(string userId)
		{
			using (var context = new ApplicationDbContext())
			{
				return context.Users.Where(x => x.Id == userId).SingleOrDefault();
			}
		}

        public static void SendHelloMail(string username, string email)
        {
            using (var context = new ApplicationDbContext())
            {
                {
                    MailService.SendMail("GymApp Powiadomienie", $"Witaj {username} zostałeś nowym użytkownikiem ", email);
                }
            }
        }

        public static void Remove(string id)
		{
			using (var context = new ApplicationDbContext())
			{
				var user = context.Users.SingleOrDefault(x => x.Id == id);
				var adminRole = context.Roles.Where(x => x.Name == "Administrator");
                user.Deleted = true;
				context.SaveChanges();

			}
		}
	}
}