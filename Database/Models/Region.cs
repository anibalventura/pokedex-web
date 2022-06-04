using System.Collections.Generic;

namespace Database.Models
{
	public class Region
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Pokemon> Pokemons { get; set; }
	}
}
