using CleanArchitect.Application.DTOs.BikeShop;
using CleanArchitect.Domain.Entities.BikeShop;

namespace CleanArchitect.Application.Interfaces
{
    public interface IBikeService
    {
        Task<BikeDTO> GetById(Guid idBike);

        Task<BikeDTO[]> GetAll();

        List<Bike> GetByName(string name);

        Task<Bike> Create(BikeDTO bikeDTO);

        Task<bool> Update(BikeDTO bikeDTO);

        Task<bool> Delete(Guid idBike);

    }
}
