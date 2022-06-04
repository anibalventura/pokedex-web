namespace Database.Models
{
	public class Pokemon
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }

		public int PrimaryTypeId { get; set; }
		public Type PrimaryType { get; set; }

		public int? SecondaryTypeId { get; set; }
		public Type SecondaryType { get; set; }

		public int RegionId { get; set; }
		public Region Region { get; set; }
	}
}
