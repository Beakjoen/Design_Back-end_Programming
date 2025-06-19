using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs;
using Shop.Application.Interfaces;
using Shop.Domain.Interfaces;
using Shop.Infrastructure;

namespace Shop.Application.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c=>new CategoryDto()
            {
                CategoryName = c.CategoryName,
                Description = c.Description,
            }).ToList();
        }
        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description,
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }
    }
}
