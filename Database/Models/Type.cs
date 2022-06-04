using System.Collections.Generic;

namespace Database.Models
{
	public class Type
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Pokemon> PokemonsPrimary { get; set; }
		public ICollection<Pokemon> PokemonsSecondary { get; set; }
	}
}
