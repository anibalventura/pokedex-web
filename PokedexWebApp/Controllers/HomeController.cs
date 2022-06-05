using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Services;
using Business.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PokedexWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonService _service;

        public HomeController(ApplicationContext dbContext)
        {
            _service = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            List<PokemonViewModel> pokemons = await _service.GetAll();

            return View(pokemons);
        }
    }
}

