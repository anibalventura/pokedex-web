using System.Threading.Tasks;
using Business.Services;
using Business.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PokedexWebApp.Controllers
{
	public class PokemonController : Controller
	{
		private readonly PokemonService _pokemonService;
		private readonly RegionService _regionService;
		private readonly TypeService _typeService;

		public PokemonController(ApplicationContext dbContext)
		{
			_pokemonService = new(dbContext);
			_regionService = new(dbContext);
			_typeService = new(dbContext);
		}

		public async Task LoadViewBags()
        {
			ViewBag.Regions = await _regionService.GetAll();
			ViewBag.Types = await _typeService.GetAll();
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _pokemonService.GetAll());
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			await LoadViewBags();

			return View("Create", new SavePokemonViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(SavePokemonViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				await LoadViewBags();

				return View("Create", vm);
			}

			await _pokemonService.Add(vm);

			return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			await LoadViewBags();

			return View("Create", await _pokemonService.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(SavePokemonViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				await LoadViewBags();

				return View("Create", vm);
			}

			await _pokemonService.Update(vm);

			return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			return View(await _pokemonService.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			await _pokemonService.Delete(id);

			return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
		}
	}
}
