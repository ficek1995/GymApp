using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GymApp.Models.ViewsModels
{
	public class TicketViewModel
	{
        public string Username { get; set; }
        public string UserId { get; set; }
        public long TicketId { get; set; }
		public IEnumerable<TicketType> TicketTypes { get; set; }
		public int TicketTypeId { get; set; }
		public string TicketTypeName { get; set; }
        public DateTime? AssignTime { get; set; }
		public DateTime? AssignTo { get; set; }
	}
}