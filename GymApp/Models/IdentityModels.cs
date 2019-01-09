using GymApp.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymApp.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{

        public ApplicationUser()
        {
            this.Lessons = new HashSet<Lesson>();
        }

        [Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public long? TickedId { get; set; }

        public bool IsTrainer { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            var entity = TicketsService.GetByUserId(this.Id);
            string ticket;
            if(entity == null)
            {
                ticket = string.Empty;
            }
            else
            {
                ticket = entity.AssignTo.ToString();
            }
            userIdentity.AddClaim(new Claim("ticket", ticket ));


            return userIdentity;
		}
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<TicketType> TicketTypes { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Lesson>()
                        .HasMany(s => s.Users)
                        .WithMany(c => c.Lessons)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("LessonRefId");
                            cs.MapRightKey("UserRefId");
                            cs.ToTable("LessonsUsers");
                        });

        }

        public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}