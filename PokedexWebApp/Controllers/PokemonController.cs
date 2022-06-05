using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Services;
using Business.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

		public async Task<List<SelectListItem>> GetTypeDropDownList()
		{
			List<TypeViewModel> types = await _typeService.GetAll();

			List<SelectListItem> selectListItems = new()
			{
				new SelectListItem { Value = "", Text = "Select" },
			};

			foreach (TypeViewModel type in types)
			{
				selectListItems.Add(new SelectListItem
				{
					Value = $"{type.Id}",
					Text = type.Name,
				});
			}

			return selectListItems;
		}

		public async Task<List<SelectListItem>> GetRegionDropDownList()
		{
			List<RegionViewModel> regions = await _regionService.GetAll();

			List<SelectListItem> selectListItems = new()
			{
				new SelectListItem { Value = "", Text = "Select" },
			};

			foreach (RegionViewModel region in regions)
			{
				selectListItems.Add(new SelectListItem
				{
					Value = $"{region.Id}",
					Text = region.Name,
				});
			}

			return selectListItems;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<PokemonViewModel> list = await _pokemonService.GetAll();

			return View(list);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.Regions = await GetRegionDropDownList();
			ViewBag.PrimaryTypes = await GetTypeDropDownList();
			ViewBag.SecondaryTypes = await GetTypeDropDownList();

			return View("Create", new SavePokemonViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(SavePokemonViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Regions = await GetRegionDropDownList();
				ViewBag.PrimaryTypes = await GetTypeDropDownList();
				ViewBag.SecondaryTypes = await GetTypeDropDownList();

				return View("Create", vm);
			}

			await _pokemonService.Add(vm);

			return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			ViewBag.Regions = await GetRegionDropDownList();
			ViewBag.PrimaryTypes = await GetTypeDropDownList();
			ViewBag.SecondaryTypes = await GetTypeDropDownList();

			return View("Create", await _pokemonService.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(SavePokemonViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Regions = await GetRegionDropDownList();
				ViewBag.PrimaryTypes = await GetTypeDropDownList();
				ViewBag.SecondaryTypes = await GetTypeDropDownList();

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
