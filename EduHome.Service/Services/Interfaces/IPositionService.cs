using System;
using EduHome.Core.DTOS;

namespace EduHome.Service.Services.Interfaces
{
	public interface ISliderService
	{

        public Task<IEnumerable<SliderGetDto>> GetAllAsync();

        public Task CreateAsync(SliderGetDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, SliderGetDto dto);
        public Task<SliderGetDto> GetAsync(int id);
    }
}

