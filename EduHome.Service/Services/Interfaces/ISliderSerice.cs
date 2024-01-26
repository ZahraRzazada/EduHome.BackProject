using System;
using EduHome.Core.DTOS;
using EduHome.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
	public interface ISliderService
	{

        public Task<IEnumerable<SliderGetDto>> GetAllAsync();

        public Task<CommonResponse> CreateAsync(SliderPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, SliderPostDto dto);
        public Task<SliderGetDto> GetAsync(int id);
    }
}

