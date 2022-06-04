using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels
{
	public class SavePokemonViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Enter a name.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Enter an image.")]
		public string ImageUrl { get; set; }

		[Required(ErrorMessage = "Select a primary type.")]
		public int PrimaryTypeId { get; set; }

		public int? SecondaryTypeId { get; set; }

		[Required(ErrorMessage = "Select a region.")]
		public int RegionId { get; set; }
	}
}
