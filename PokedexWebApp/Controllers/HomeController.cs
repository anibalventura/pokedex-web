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
        private readonly PokemonService _pokemonService;
        private readonly RegionService _regionService;

        public HomeController(ApplicationContext dbContext)
        {
            _pokemonService = new(dbContext);
            _regionService = new(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Regions = await _regionService.GetAll();

            return View(await _pokemonService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Filter(FilterPokemonViewModel filters)
        {
            ViewBag.Regions = await _regionService.GetAll();

            return View("Index", await _pokemonService.GetAllFiltered(filters));
        }
    }
}

