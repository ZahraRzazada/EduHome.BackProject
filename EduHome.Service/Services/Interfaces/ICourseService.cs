using System;
using EduHome.Core.DTOS;
using EduHome.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
	public interface ICourseService
	{

        public Task<IEnumerable<CourseGetDto>> GetAllAsync();

        public Task<CommonResponse> CreateAsync(CoursePostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, CoursePostDto dto);
        public Task<CourseGetDto> GetAsync(int id);
    }
}

