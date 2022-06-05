using Database.Models;

namespace Business.ViewModels
{
	public class PokemonViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public Type PrimaryType { get; set; }
		public Type SecondaryType { get; set; }
		public Region Region { get; set; }

		public int RegionId { get; set; }
	}
}
