using System;
using EduHome.Core.DTOS;

namespace EduHome.Service.Services.Interfaces
{
	public interface ICategoryService
	{

        public Task<IEnumerable<CategoryGetDto>> GetAllAsync();

        public Task CreateAsync(CategoryPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, CategoryPostDto dto);
        public Task<CategoryGetDto> GetAsync(int id);
    }
}

