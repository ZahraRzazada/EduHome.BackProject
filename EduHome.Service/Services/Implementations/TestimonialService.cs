using System;
using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Extensions;
using EduHome.Service.Responses;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class TestimonialService : ITestimonialService
    {
        readonly ITestimoialRepository _testimonialRepository;
        readonly IWebHostEnvironment _env;

        public TestimonialService(ITestimoialRepository testimonialrRepository, IWebHostEnvironment env)
        {
            _testimonialRepository = testimonialrRepository;
            _env = env;
        }
        public async Task<CommonResponse> CreateAsync(TestimonialPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;
            Testimonial testimonial = new Testimonial();
            testimonial.FullName = dto.FullName;
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

            testimonial.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/testimonial");
            testimonial.Text = dto.Text;
            testimonial.Position = dto.Position;
            await _testimonialRepository.AddAsync(testimonial);
            await _testimonialRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<IEnumerable<TestimonialGetDto>> GetAllAsync()
        {
            IEnumerable<TestimonialGetDto> testimonials = await _testimonialRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new TestimonialGetDto {  FullName=x.FullName, Position = x.Position, Image = x.Image,Text=x.Text,Id=x.Id })
               .ToListAsync();
            return testimonials;
        }

        public async Task<TestimonialGetDto> GetAsync(int id)
        {
            Testimonial? testimonial = await _testimonialRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (testimonial == null)
            {
                throw new ItemNotFoundExcpetion("Testimonial Not Found");
            }

            TestimonialGetDto testimonialGetDto = new TestimonialGetDto
            {
                Id=testimonial.Id,
                FullName = testimonial.FullName,
                Text = testimonial.Text,
                Position=testimonial.Position,
                Image = testimonial.Image
            };
            return testimonialGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Testimonial? testimonial = await _testimonialRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (testimonial == null)
            {
                throw new ItemNotFoundExcpetion("Testimonial Not Found");
            }
            testimonial.IsDeleted = true;
            await _testimonialRepository.UpdateAsync(testimonial);
            await _testimonialRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, TestimonialPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Testimonial? testimonial = await _testimonialRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (testimonial == null)
            {
                throw new ItemNotFoundExcpetion("Testimonial Not Found");
            }

            testimonial.FullName = dto.FullName;
            testimonial.Text = dto.Text;
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

                testimonial.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/testimonial");
            }
            testimonial.Position = dto.Position;

            await _testimonialRepository.UpdateAsync(testimonial);
            await _testimonialRepository.SaveChangesAsync();
            return commonResponse;
        }

    }
}

