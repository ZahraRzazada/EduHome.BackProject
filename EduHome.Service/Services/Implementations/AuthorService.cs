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
    public class AuthorService : IAuthorService
    {
        readonly IAuthorRepository _authorRepository;
   
        readonly IWebHostEnvironment _env;

        public AuthorService(IAuthorRepository AuthorRepository, IWebHostEnvironment env)
        {
            _authorRepository = AuthorRepository;
 
            _env = env;
        }

        public async Task<CommonResponse> CreateAsync(AuthorPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };

            Author Author = new Author();
            Author.FullName = dto.FullName;
        
            await _authorRepository.AddAsync(Author);
            await _authorRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<IEnumerable<AuthorGetDto>> GetAllAsync(int page = 1)
        {
           
           IEnumerable< AuthorGetDto> author = await _authorRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().
               Select(x =>
               new AuthorGetDto
               {
                   FullName = x.FullName,
                   Id = x.Id,
               })
               .ToListAsync();
            return author;
        }

        public async Task<AuthorGetDto> GetAsync(int id)
        {
            Author? Author = await _authorRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Author == null)
            {
                throw new ItemNotFoundExcpetion("Author Not Found");
            }

            AuthorGetDto AuthorGetDto = new AuthorGetDto
            {
                Id = Author.Id,
                FullName = Author.FullName,
            };
            return AuthorGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Author? Author = await _authorRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Author == null)
            {
                throw new ItemNotFoundExcpetion("Author Not Found");
            }
            Author.IsDeleted = true;
            await _authorRepository.UpdateAsync(Author);
            await _authorRepository.SaveChangesAsync();
        }


        public async Task<CommonResponse> UpdateAsync(int id, AuthorPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };
           
            Author? Author = await _authorRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Author == null)
            {
                throw new ItemNotFoundExcpetion("Author Not Found");
            }

            Author.FullName = dto.FullName;
   
            await _authorRepository.UpdateAsync(Author);
            await _authorRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}

