using System;
using EduHome.Core.DTOS;
using EduHome.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
	public interface IAuthorService
	{
        public Task<IEnumerable<AuthorGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(AuthorPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, AuthorPostDto dto);
        public Task<AuthorGetDto> GetAsync(int id);
    }
}

