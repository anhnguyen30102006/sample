using CleanArchitect.Application.DTOs.BikeShop;
using CleanArchitect.Application.Interfaces;
using CleanArchitect.Common.Configs;
using CleanArchitect.Domain.Entities.BikeShop;
using CleanArchitect.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitect.Infrastructure.Services
{
    public class BikeService : IBikeService
    {
        private readonly EBikeShopDbContext _context;
        public BikeService(EBikeShopDbContext context)
        {
            _context = context;
        }
        public async Task<Bike> Create(BikeDTO bikeDTO)
        {
            var countBikes = await _context.Bikes.CountAsync();
            var bike = new Bike
            {
                Name = bikeDTO.Name.Trim(),
                BrandName = bikeDTO.BrandName.Trim(),
                CategoryId = bikeDTO.CategoryId,
                //Category = bikeDTO.Category.Trim(),
                Description = bikeDTO.Description?.Trim(),
                ImageName = bikeDTO.ImagePath,
                Year = bikeDTO.Year,
                Position = ++countBikes
            };
            _context.Bikes.Add(bike);
            return bike;

        }

        public async Task<bool> Delete(Guid idBike)
        {
            var isOK = false;
            try
            {
                var bike = await _context.Bikes.FindAsync(idBike);
                if (bike != null)
                {
                    _context.Bikes.Remove(bike);
                    await _context.SaveChangesAsync();
                    isOK = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return isOK;
        }

        public async Task<BikeDTO[]> GetAll()
        {
            var bikes = new BikeDTO[] { };
            try
            {
                bikes = await _context.Bikes
                    .Select(b => new BikeDTO(b))
                    .ToArrayAsync();
            }
            catch
            {

            }
            return bikes;
        }

        public async Task<BikeDTO> GetById(Guid idBike)
        {
            var dtoBike = new BikeDTO();
            try
            {
                dtoBike = await _context.Bikes
                    .Where(b => b.Id == idBike)
                    .Select(b => new BikeDTO(b))
                    .SingleOrDefaultAsync();
            }
            catch
            {

            }
            return dtoBike;
        }

        public List<Bike> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(BikeDTO bikeDTO)
        {
            var isOK = false;
            try
            {
                var bike = await _context.Bikes.FindAsync(bikeDTO.Id);
                if (bike != null)
                {
                    bike.Name = bikeDTO.Name.Trim();
                    bike.BrandName = bikeDTO.BrandName.Trim();
                    bike.Year = bikeDTO.Year;
                    bike.CategoryId = bikeDTO.CategoryId;
                    //bike.Category = bikeDTO.Category.Trim();
                    bike.Description = bikeDTO?.Description?.Trim();
                    bike.ImageName = bikeDTO?.ImagePath;

                    await _context.SaveChangesAsync();
                    isOK = true;
                }
            }
            catch
            {

            }
            return isOK;
        }
    }
}
