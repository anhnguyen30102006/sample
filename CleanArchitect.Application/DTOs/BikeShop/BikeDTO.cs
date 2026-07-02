using CleanArchitect.Domain.Entities.BikeShop;

namespace CleanArchitect.Application.DTOs.BikeShop
{
    public class BikeDTO
    {
        public BikeDTO()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bike">bike lấy kèm Category</param>
        public BikeDTO(Bike bike)
        {
            Id = bike.Id;
            Name = bike.Name;
            Description = bike.Description;
            ImagePath = bike.ImageName;
            //ImagePath = bike.ImageName != null
            //    ? Path.Combine("~/", AppConstants.ImageFolderPath, bike.ImageName)
            //    : Path.Combine("~/", AppConstants.ImageDefault);
            Position = bike.Position;
            Year = bike.Year;
            BrandName = bike.BrandName;
            CategoryId = bike.CategoryId;
            Category = bike.Category.Name;
        }
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        //public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }

        public int Position { get; set; }

        public int Year { get; set; }

        public string BrandName { get; set; } = string.Empty;

        public Guid? CategoryId { get; set; }
        public string? Category { get; set; }

    }
}
