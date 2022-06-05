using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repositories;
using Business.ViewModels;
using Database;
using Database.Models;

namespace Business.Services
{
	public class TypeService
	{
		private readonly TypeRepository _repository;

		public TypeService(ApplicationContext dbContext)
		{
			_repository = new(dbContext);
		}

        public async Task Add(SaveTypeViewModel vm)
        {
            Type type = new();
            type.Name = vm.Name;

            await _repository.AddType(type);
        }

        public async Task Update(SaveTypeViewModel vm)
        {
            Type type = new();
            type.Id = vm.Id;
            type.Name = vm.Name;

            await _repository.UpdateType(type);
        }

        public async Task Delete(int id)
        {
            Type type = await _repository.GetTypeById(id);

            await _repository.DeleteType(type);
        }

        public async Task<List<TypeViewModel>> GetAll()
        {
            List<Type> list = await _repository.GetAllTypes();

            return list.Select(s => new TypeViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
        }

        public async Task<SaveTypeViewModel> GetById(int id)
        {
            Type type = await _repository.GetTypeById(id);

            SaveTypeViewModel vm = new();
            vm.Id = type.Id;
            vm.Name = type.Name;

            return vm;
        }
    }
}

