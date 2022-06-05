using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class PokemonRepository
    {
        private readonly ApplicationContext _dbContext;

        public PokemonRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPokemon(Pokemon pokemon)
        {
            await _dbContext.Set<Pokemon>().AddAsync(pokemon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePokemon(Pokemon pokemon)
        {
            _dbContext.Entry(pokemon).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePokemon(Pokemon pokemon)
        {
            _dbContext.Set<Pokemon>().Remove(pokemon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Pokemon>> GetAllPokemons()
        {
            return await _dbContext
                 .Set<Pokemon>()
                 .Include(pokemon => pokemon.PrimaryType)
                 .Include(pokemon => pokemon.SecondaryType)
                 .Include(pokemon => pokemon.Region)
                 .ToListAsync();
        }
        public async Task<Pokemon> GetPokemonById(int id)
        {
            return await _dbContext
                 .Set<Pokemon>()
                 .FindAsync(id);
        }
    }
}
