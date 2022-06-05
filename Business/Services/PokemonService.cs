using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repositories;
using Business.ViewModels;
using Database;
using Database.Models;

namespace Business.Services
{
	public class PokemonService
	{
		private readonly PokemonRepository _repository;

		public PokemonService(ApplicationContext dbContext)
		{
			_repository = new(dbContext);
		}

        public async Task Add(SavePokemonViewModel vm)
        {
            Pokemon pokemon = new();
            pokemon.Name = vm.Name;
            pokemon.ImageUrl = vm.ImageUrl;
            pokemon.PrimaryTypeId = vm.PrimaryTypeId;
            pokemon.SecondaryTypeId = vm.SecondaryTypeId;
            pokemon.RegionId = vm.RegionId;

            await _repository.AddPokemon(pokemon);
        }

        public async Task Update(SavePokemonViewModel vm)
        {
            Pokemon pokemon = new();
            pokemon.Id = vm.Id;
            pokemon.Name = vm.Name;
            pokemon.ImageUrl = vm.ImageUrl;
            pokemon.PrimaryTypeId = vm.PrimaryTypeId;
            pokemon.SecondaryTypeId = vm.SecondaryTypeId;
            pokemon.RegionId = vm.RegionId;

            await _repository.UpdatePokemon(pokemon);
        }

        public async Task Delete(int id)
        {
            Pokemon pokemon = await _repository.GetPokemonById(id);

            await _repository.DeletePokemon(pokemon);
        }

        public async Task<List<PokemonViewModel>> GetAll()
        {
            List<Pokemon> list = await _repository.GetAllPokemons();

            return list.Select(s => new PokemonViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ImageUrl = s.ImageUrl,
            }).ToList();
        }

        public async Task<SavePokemonViewModel> GetById(int id)
        {
            Pokemon pokemon = await _repository.GetPokemonById(id);

            SavePokemonViewModel vm = new();
            vm.Id = pokemon.Id;
            vm.Name = pokemon.Name;
            vm.ImageUrl = pokemon.ImageUrl;
            vm.PrimaryTypeId = pokemon.PrimaryTypeId;
            vm.SecondaryTypeId = pokemon.SecondaryTypeId;
            vm.RegionId = pokemon.RegionId;

            return vm;
        }
    }
}
