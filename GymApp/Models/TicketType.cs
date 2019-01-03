namespace GymApp.Models
{
	public class TicketType
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public TicketTypeEnum TicketCategory { get; set; }
	}

	public enum TicketTypeEnum
	{
		Monthly = 30,
		Weekly = 20,
		Daily = 10,

	}

}