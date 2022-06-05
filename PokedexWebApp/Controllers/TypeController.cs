using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Services;
using Business.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PokedexWebApp.Controllers
{
	public class TypeController : Controller
	{
		private readonly TypeService _service;

		public TypeController(ApplicationContext dbContext)
		{
			_service = new(dbContext);
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _service.GetAll());
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View("Create", new SaveTypeViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(SaveTypeViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", vm);
			}

			await _service.Add(vm);

			return RedirectToRoute(new { controller = "Type", action = "Index" });
		}

		[HttpGet]
        public async Task<IActionResult> Edit(int id)
		{
			return View("Create", await _service.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(SaveTypeViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", vm);
			}

			await _service.Update(vm);

			return RedirectToRoute(new { controller = "Type", action = "Index" });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			return View(await _service.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			await _service.Delete(id);

			return RedirectToRoute(new { controller = "Type", action = "Index" });
		}
	}
}
