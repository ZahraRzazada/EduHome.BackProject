using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class SliderService : ISliderService
    {
        readonly ISliderRepository _sliderRepository;
        
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public async Task CreateAsync(SliderGetDto dto)
        {
            Slider slider = new Slider();
            slider.Description = dto.Description;
            slider.Image = dto.Image;
            slider.Title = dto.Title;
            await _sliderRepository.AddAsync(slider);
            await _sliderRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<SliderGetDto>> GetAllAsync()
        {
            IEnumerable<SliderGetDto> sliders = await _sliderRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new SliderGetDto { Title = x.Title, Description = x.Description, Image = x.Image })
               .ToListAsync();
            return sliders;
        }
        public async Task<SliderGetDto> GetAsync(int id)
        {
            Slider? slider = await _sliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (slider == null)
            {
                throw new ItemNotFoundExcpetion("Slider Not Found");
            }

            SliderGetDto slidergetdto = new SliderGetDto
            {
            Title=slider.Title,
            Description=slider.Description,
            Image=slider.Image
            };
            return slidergetdto;
        }

        public async Task RemoveAsync(int id)
        {
            Slider? slider = await _sliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (slider == null)
            {
                throw new ItemNotFoundExcpetion("Slider Not Found");
            }
            slider.IsDeleted = true;
            await _sliderRepository.UpdateAsync(slider);
            await _sliderRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, SliderGetDto dto)
        {
            Slider? slider = await _sliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (slider == null)
            {
                throw new ItemNotFoundExcpetion("Slider Not Found");
            }

            slider.Title = dto.Title;
            slider.Description = dto.Description;
            slider.Image = dto.Image;

            await _sliderRepository.UpdateAsync(slider);
            await _sliderRepository.SaveChangesAsync();
        }

    }
}

