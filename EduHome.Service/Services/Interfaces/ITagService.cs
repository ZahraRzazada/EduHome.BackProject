using System;
using EduHome.Core.DTOS;

namespace EduHome.Service.Services.Interfaces
{
	public interface ITagService
	{

        public Task<IEnumerable<TagGetDto>> GetAllAsync();

        public Task CreateAsync(TagPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, TagPostDto dto);
        public Task<TagGetDto> GetAsync(int id);
    }
}

