using System;

namespace GymApp.Models
{
	public class Ticket
	{
		public long Id { get; set; }
		public int TicketTypeId { get; set; }
		public TicketType TicketType { get; set; }

		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public DateTime AssignTime { get; set; }
		public DateTime AssignTo { get; set; }
	}
}