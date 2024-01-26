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
    public class BlogService : IBlogService
    {
        readonly IWebHostEnvironment _env;
        readonly IBlogRepository _blogRepository;

        public BlogService(IWebHostEnvironment env, IBlogRepository blogRepository)
        {
            _env = env;
            _blogRepository = blogRepository;
        }

        public async Task<CommonResponse> CreateAsync(BlogPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Blog blog = new Blog
            {
                AuthorId = dto.AuthorId,
               
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

            blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/blog");

            foreach (var item in dto.TagsIds)
            {
                blog.TagBlogs.Add(new TagBlog
                {
                    Blog = blog,
                    TagId = item,
                });
            }

            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<IEnumerable<BlogGetDto>> GetAllAsync()
        {
            IEnumerable<BlogGetDto> Authors = await _blogRepository.GetQuery(x => !x.IsDeleted)
              .AsNoTrackingWithIdentityResolution()
              .Include(x => x.TagBlogs)
              .ThenInclude(x => x.Tag)
              .Include(x => x.Author)
              .Include(x => x.Category)
              .Select(x =>
              new BlogGetDto
              {
                  Title = x.Title,
                  Id = x.Id,
                  Description = x.Description,
                  tags = x.TagBlogs.Select(y => new TagGetDto { Name = y.Tag.Name }),
                  Image = x.Image,
                  Date = x.CreatedAt,
                  AuthorGetDto = new AuthorGetDto { FullName = x.Author.FullName },
                  CategoryGetDto = new CategoryGetDto { Name = x.Category.Name }
              })
              .ToListAsync();
            return Authors;
        }

        public async Task<BlogGetDto> GetAsync(int id)
        {
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag","Author","Category");

            if (blog == null)
            {
                throw new ItemNotFoundExcpetion("Blog Not Found");
            }

            BlogGetDto BlogGetDto = new BlogGetDto
            {
                Id = blog.Id,
                Date = blog.CreatedAt,
                Description = blog.Description,
                Image = blog.Image,
                tags = blog.TagBlogs.Select(x => new TagGetDto { Name = x.Tag.Name, Id = x.Tag.Id }),
                Title = blog.Title,
                AuthorGetDto = new AuthorGetDto
                {
                    FullName = blog.Author.FullName,
                    Id = blog.Author.Id,
                },
                CategoryGetDto = new CategoryGetDto
                {
                    Name = blog.Category.Name,
                    Id = blog.Category.Id,
                },

            };
            return BlogGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag");

            if (blog == null)
            {
                throw new ItemNotFoundExcpetion("Blog Not Found");
            }
            blog.IsDeleted = true;
            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, BlogPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag", "Author");

            if (blog == null)
            {
                throw new ItemNotFoundExcpetion("Blog Not Found");
            }
            blog.Title = dto.Title;
            blog.Description = dto.Description;
            blog.AuthorId = dto.AuthorId;
            blog.CategoryId = dto.CategoryId;

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

                blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/blog");
            }
            blog.TagBlogs.Clear();

            foreach (var item in dto.TagsIds)
            {
                blog.TagBlogs.Add(new TagBlog
                {
                    Blog = blog,
                    TagId = item,
                });
            }

            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return commonResponse;
        }

    }
}

