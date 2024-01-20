using System;
using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class TestimonialService : ITestimonialService
    {
        readonly ITestimoialRepository _testimonialRepository;

        public TestimonialService(ITestimoialRepository testimonialrRepository)
        {
            _testimonialRepository = testimonialrRepository;
        }
        public async Task CreateAsync(TestimonialGetDto dto)
        {
            Testimonial testimonial = new Testimonial();
            testimonial.FullName = dto.FullName;
            testimonial.Image = dto.Image;
            testimonial.Text = dto.Text;
            await _testimonialRepository.AddAsync(testimonial);
            await _testimonialRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TestimonialGetDto>> GetAllAsync()
        {
            IEnumerable<TestimonialGetDto> testimonials = await _testimonialRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new TestimonialGetDto {  FullName=x.FullName, Position = x.Position, Image = x.Image,Text=x.Text })
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

        public async Task UpdateAsync(int id, TestimonialGetDto dto)
        {
            Testimonial? testimonial = await _testimonialRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (testimonial == null)
            {
                throw new ItemNotFoundExcpetion("Testimonial Not Found");
            }

            testimonial.FullName = dto.FullName;
            testimonial.Text = dto.Text;
            testimonial.Image = dto.Image;
            testimonial.Position = dto.Position;

            await _testimonialRepository.UpdateAsync(testimonial);
            await _testimonialRepository.SaveChangesAsync();
        }

    }
}

