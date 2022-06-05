using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels
{
	public class SaveRegionViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Enter a name.")]
		public string Name { get; set; }
	}
}
