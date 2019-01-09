using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymApp.Models
{
    public class Lesson
    {
        public Lesson()
        {
            this.Users= new HashSet<ApplicationUser>();
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

    }
}