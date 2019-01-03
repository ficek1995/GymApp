using System.ComponentModel.DataAnnotations;

namespace GymApp.ViewsModels
{
	public class UserViewModel
	{
		public string Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
        public bool IsTrainer { get; set; }
    }
}