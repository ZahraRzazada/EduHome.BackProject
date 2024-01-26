using System;
using EduHome.Core.DTOS;
using EduHome.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
	public interface ITestimonialService
	{

        public Task<IEnumerable<TestimonialGetDto>> GetAllAsync();

        public Task<CommonResponse> CreateAsync(TestimonialPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, TestimonialPostDto dto);
        public Task<TestimonialGetDto> GetAsync(int id);
    }
}

