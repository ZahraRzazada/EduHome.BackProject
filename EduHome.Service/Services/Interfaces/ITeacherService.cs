using System;
using EduHome.Core.DTOS;
using EduHome.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
	public interface ITeacherService
	{
        public Task<IEnumerable<TeacherGetDto>> GetAllAsync();

        public Task<CommonResponse> CreateAsync(TeacherPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, TeacherPostDto dto);
        public Task<TeacherGetDto> GetAsync(int id);
    }
}

