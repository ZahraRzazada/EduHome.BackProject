using System.Reflection.Metadata;
using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Extensions;
using EduHome.Service.Responses;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class SliderService : ISliderService
    {
        readonly ISliderRepository _sliderRepository;
        readonly IWebHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository, IWebHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
            _env = env;
        }
        public async Task<CommonResponse> CreateAsync(SliderPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Slider slider = new Slider();
            slider.Description = dto.Description;
            if (dto.ImageFile == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field image is required";
                return commonResponse;
            }

            if (!dto.ImageFile.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image is not valid";
                return commonResponse;
            }

            if (dto.ImageFile.IsSizeOk(1))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image  size is not valid";
                return commonResponse;
            }

            slider.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/slider");
            slider.Title = dto.Title;
            await _sliderRepository.AddAsync(slider);
            await _sliderRepository.SaveChangesAsync();

            return commonResponse;
        }
        public async Task<IEnumerable<SliderGetDto>> GetAllAsync()
        {
            IEnumerable<SliderGetDto> sliders = await _sliderRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new SliderGetDto { Title = x.Title, Description = x.Description, Image = x.Image,Id=x.Id })
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
            Id=slider.Id,
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

        public async Task<CommonResponse> UpdateAsync(int id, SliderPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Slider? slider = await _sliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (slider == null)
            {
                throw new ItemNotFoundExcpetion("Slider Not Found");
            }

            slider.Title = dto.Title;
            slider.Description = dto.Description;
            if (dto.ImageFile != null)
            {
                if (!dto.ImageFile.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image is not valid";
                    return commonResponse;
                }

                if (dto.ImageFile.IsSizeOk(1))
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image  size is not valid";
                    return commonResponse;
                }

                slider.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/slider");
            }

            await _sliderRepository.UpdateAsync(slider);
            await _sliderRepository.SaveChangesAsync();
            return commonResponse;
        }

    }
}

