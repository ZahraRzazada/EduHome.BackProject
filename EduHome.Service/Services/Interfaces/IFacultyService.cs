using System;
using EduHome.Core.DTOS;

namespace EduHome.Service.Services.Interfaces
{
	public interface IFacultyService
	{

        public Task<IEnumerable<FacultyGetDto>> GetAllAsync();

        public Task CreateAsync(FacultyPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, FacultyPostDto dto);
        public Task<FacultyGetDto> GetAsync(int id);
    }
}

