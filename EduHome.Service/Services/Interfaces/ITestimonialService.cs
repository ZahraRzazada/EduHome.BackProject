using System;
using EduHome.Core.DTOS;

namespace EduHome.Service.Services.Interfaces
{
	public interface ITestimonialService
	{

        public Task<IEnumerable<TestimonialGetDto>> GetAllAsync();

        public Task CreateAsync(TestimonialGetDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, TestimonialGetDto dto);
        public Task<TestimonialGetDto> GetAsync(int id);
    }
}

