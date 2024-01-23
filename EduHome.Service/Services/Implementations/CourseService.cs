using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Entities.BaseEntities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Extensions;
using EduHome.Service.Responses;
using EduHome.Service.Services.Interfaces;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class CourseService : ICourseService
    {
        readonly IWebHostEnvironment _env;
        readonly ICourseRepository _courseRepository;

        public CourseService(IWebHostEnvironment env, ICourseRepository courseRepository)
        {
            _env = env;
            _courseRepository = courseRepository;
        }

        public async Task<CommonResponse> CreateAsync(CoursePostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Course course = new Course
            {
                Name = dto.Name,
                Certification=dto.Certification,
                Apply=dto.Apply,
                About=dto.About,
                Features=new List<Feature>(),
                Description = dto.Description,
                Title = dto.Title,
                TagBlogs = new List<TagBlog>(),
                CategoryId = dto.CategoryId
            };

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

            course.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "img/course");

            foreach (var item in dto.TagsIds)
            {
               course.TagBlogs.Add(new TagBlog
            {
                Course = course,
                TagId = item,
             });
            }
            for (int i = 0; i < dto.FeatureKeys.Count(); i++)
            {
                course.Features.Add(new Feature
                {
                    Course = course,
                    Key = dto.FeatureKeys[i],
                    Value = dto.FeatureValues[i],
                });
            }
            await _courseRepository.AddAsync(course);
             await _courseRepository.SaveChangesAsync();
             return commonResponse;
        }

        public async Task<IEnumerable<CourseGetDto>> GetAllAsync()
        {
            IEnumerable<CourseGetDto> Courses = await _courseRepository.GetQuery(x => !x.IsDeleted)
              .AsNoTrackingWithIdentityResolution()
              .Include(x => x.TagBlogs)
              .ThenInclude(x => x.Tag)
              .Include(x => x.Category)
              .Select(x =>
              new CourseGetDto
              {
                  Name=x.Name,
                  Title = x.Title,
                  Id = x.Id,
                  About=x.About,
                  Apply=x.Apply,
                  Certification=x.Certification,
                  Description = x.Description,
                  tags = x.TagBlogs.Select(y => new TagGetDto { Name = y.Tag.Name }),
                  Image = x.Image,
                  Date = x.CreatedAt,
                  CategoryGetDto = new CategoryGetDto { Name = x.Category.Name }
              })
              .ToListAsync();
            return Courses;
        }

        public async Task<CourseGetDto> GetAsync(int id)
        {
            Course? course = await _courseRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag", "Category", "Skills");

            if (course == null)
            {
                throw new ItemNotFoundExcpetion("Course Not Found");
            }

            CourseGetDto courseGetDto = new CourseGetDto
            {
                Name=course.Name,
                Id = course.Id,
                Date = course.CreatedAt,
                Description = course.Description,
                About=course.About,
                Apply=course.Apply,
                Certification=course.Certification,
                Image = course.Image,
                tags = course.TagBlogs.Select(x => new TagGetDto { Name = x.Tag.Name, Id = x.Tag.Id }),
                Title = course.Title,
                CategoryGetDto = new CategoryGetDto
                {
                    Name = course.Category.Name,
                    Id = course.Category.Id,
                },
                Features=course.Features

            };
            return courseGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Course? course = await _courseRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag");

            if (course == null)
            {
                throw new ItemNotFoundExcpetion("Course Not Found");
            }
            course.IsDeleted = true;
            await _courseRepository.UpdateAsync(course);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, CoursePostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;
            Course? course = await _courseRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag");

            if (course == null)
            {
                throw new ItemNotFoundExcpetion("Blog Not Found");
            }
            course.Name = dto.Name;
            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Apply = dto.Apply;
            course.Certification = dto.Certification;
            course.About = dto.About;
            course.CategoryId = dto.CategoryId;
            course.Features.Clear();
            for (int i = 0; i < dto.FeatureKeys.Count(); i++)
            {
                course.Features.Add(new Feature
                {
                    Course = course,
                    Key = dto.FeatureKeys[i],
                    Value = dto.FeatureValues[i],
                });
            }

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

                course.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "img/course");
            }
            course.TagBlogs.Clear();

            foreach (var item in dto.TagsIds)
            {
                course.TagBlogs.Add(new TagBlog
                {
                    Course = course,
                    TagId = item,
                });
            }
            if (dto.FeatureKeys == null || dto.FeatureValues == null || dto.FeatureKeys.Count() != dto.FeatureValues.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The feature network is not valid";
                return commonResponse;
            }

            if (dto.FeatureKeys.Any(x => string.IsNullOrWhiteSpace(x)) || dto.FeatureValues.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The feature network is not valid";
                return commonResponse;
            }

            await _courseRepository.UpdateAsync(course);
            await _courseRepository.SaveChangesAsync();
            return commonResponse;
        }

    }
}

