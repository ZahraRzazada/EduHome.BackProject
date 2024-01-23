using System;
using EduHome.Core.DTOS;

namespace EduHome.Service.Services.Interfaces
{
	public interface IHobbyService
	{

        public Task<IEnumerable<HobbyGetDto>> GetAllAsync();

        public Task CreateAsync(HobbyPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, HobbyPostDto dto);
        public Task<HobbyGetDto> GetAsync(int id);
    }
}

