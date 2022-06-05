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
            List<Pokemon> pokemons = await _repository.GetAllPokemons();

            return pokemons.Select(s => new PokemonViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ImageUrl = s.ImageUrl,
                PrimaryType = s.PrimaryType,
                SecondaryType = s.SecondaryType,
                Region = s.Region,
                RegionId = s.RegionId,
            }).ToList();
        }

        public async Task<List<PokemonViewModel>> GetAllFiltered(FilterPokemonViewModel filters)
        {
            List<Pokemon> pokemons = await _repository.GetAllPokemons();

            List<PokemonViewModel> listViewModel = pokemons.Select(s => new PokemonViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ImageUrl = s.ImageUrl,
                PrimaryType = s.PrimaryType,
                SecondaryType = s.SecondaryType,
                Region = s.Region,
                RegionId = s.RegionId,
            }).ToList();

            if (filters.Name != "")
            {
                listViewModel = listViewModel
                    .Where(pokemon => pokemon.Name == filters.Name)
                    .ToList();
            }

            if (filters.RegionId != null)
            {
                listViewModel = listViewModel
                    .Where(pokemon => pokemon.RegionId == filters.RegionId.Value)
                    .ToList();
            }

            return listViewModel;
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
