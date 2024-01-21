using System;
using EduHome.Core.DTOS;

namespace EduHome.Service.Services.Interfaces
{
	public interface IDegreeService
	{

        public Task<IEnumerable<DegreeGetDto>> GetAllAsync();

        public Task CreateAsync(DegreePostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, DegreePostDto dto);
        public Task<DegreeGetDto> GetAsync(int id);
    }
}

