using System;
using EduHome.Core.DTOS;
using EduHome.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
	public interface IBlogService
	{

        public Task<IEnumerable<BlogGetDto>> GetAllAsync();

        public Task<CommonResponse> CreateAsync(BlogPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, BlogPostDto dto);
        public Task<BlogGetDto> GetAsync(int id);
    }
}

