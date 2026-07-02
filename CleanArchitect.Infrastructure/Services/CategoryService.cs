using CleanArchitect.Application.DTOs.BikeShop;
using CleanArchitect.Application.Interfaces;
using CleanArchitect.Domain.Entities.BikeShop;
using CleanArchitect.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitect.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly EBikeShopDbContext _context;
        public CategoryService(EBikeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> Create(CategoryDTO categoryDTO)
        {
            try
            {
                var countCategory = await _context.Categories.CountAsync();
                //category.Position = ++countCategory;
                //_context.Add(category);
                var newCategory = new Category
                {
                    //Id = Guid.NewGuid(),
                    Name = categoryDTO.Name.Trim(),
                    Description = categoryDTO.Description?.Trim(),
                    Position = ++countCategory
                };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
                return newCategory;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public async Task<bool> Delete(Guid idCategory)
        {
            var isOK = false;
            try
            {
                var category = await _context.Categories.FindAsync(idCategory);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                }

                await _context.SaveChangesAsync();
                isOK = true;
            }
            catch (Exception ex)
            {

            }
            return isOK;
        }

        public async Task<CategoryDTO[]> GetAll()
        {
            var cateDTOs = new CategoryDTO[]{ };
            try
            {
                cateDTOs = await _context.Categories
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Position = c.Position
                    })
                    .ToArrayAsync();
            }
            catch (Exception ex)
            { }
            return cateDTOs;
        }

        public async Task<CategoryDTO?> GetById(Guid idCategory)
        {
            var dtoCategory = new CategoryDTO();
            try
            {
                dtoCategory = await _context.Categories
                    .Where(c => c.Id == idCategory)
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Position = c.Position
                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

            }
            return dtoCategory;
        }

        public List<Category> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(CategoryDTO categoryDTO)
        {
            bool isOK = false;
            try
            {
                var oldCate = await _context.Categories.FindAsync(categoryDTO.Id);
                if (oldCate != null)
                {
                    oldCate.Name = categoryDTO.Name.Trim();
                    oldCate.Description = categoryDTO.Description?.Trim();
                    await _context.SaveChangesAsync();
                    isOK = true;
                }
            }
            catch (Exception ex)
            {

            }
            return isOK;
        }
    }
}
