using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task CreateAsync(CategoryPostDto dto)
        {
            Category category = new Category();
            category.Name = dto.Name;

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
        {
            IEnumerable<CategoryGetDto> categories = await _categoryRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new CategoryGetDto { Name = x.Name,Id=x.Id })
               .ToListAsync();
            return categories;
        }
        public async Task<CategoryGetDto> GetAsync(int id)
        {
            Category? category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (category == null)
            {
                throw new ItemNotFoundExcpetion("Category Not Found");
            }

            CategoryGetDto categoryGetDto = new CategoryGetDto
            {
                Name = category.Name,

            };
            return categoryGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Category? category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (category == null)
            {
                throw new ItemNotFoundExcpetion("Category Not Found");
            }
            category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryPostDto dto)
        {
            Category? category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (category == null)
            {
                throw new ItemNotFoundExcpetion("Category Not Found");
            }

            category.Name = dto.Name;



            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}

