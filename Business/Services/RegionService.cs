using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repositories;
using Business.ViewModels;
using Database;
using Database.Models;

namespace Business.Services
{
    public class RegionService
    {
        private readonly RegionRepository _repository;

        public RegionService(ApplicationContext dbContext)
        {
            _repository = new(dbContext);
        }

        public async Task Add(SaveRegionViewModel vm)
        {
            Region region = new();
            region.Name = vm.Name;

            await _repository.AddRegion(region);
        }

        public async Task Update(SaveRegionViewModel vm)
        {
            Region region = new();
            region.Id = vm.Id;
            region.Name = vm.Name;

            await _repository.UpdateRegion(region);
        }

        public async Task Delete(int id)
        {
            Region region = await _repository.GetRegionById(id);

            await _repository.DeleteRegion(region);
        }

        public async Task<List<RegionViewModel>> GetAll()
        {
            List<Region> list = await _repository.GetAllRegions();

            return list.Select(s => new RegionViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
        }

        public async Task<SaveRegionViewModel> GetById(int id)
        {
            Region region = await _repository.GetRegionById(id);

            SaveRegionViewModel vm = new();
            vm.Id = region.Id;
            vm.Name = region.Name;

            return vm;
        }
    }
}
