using CleanArchitect.Application.DTOs.BikeShop;
using CleanArchitect.Domain.Entities.BikeShop;

namespace CleanArchitect.Application.Interfaces
{
    public interface ICategoryService : ICategoryGetAll
    {
        Task<CategoryDTO?> GetById(Guid idCategory);
        //Task<CategoryDTO[]> GetAll();

        List<Category> GetByName(string name);

        Task<Category?> Create(CategoryDTO categoryDTO);
        Task<bool> Update(CategoryDTO categoryDTO);

        Task<bool> Delete(Guid idCategory);
    }
}
