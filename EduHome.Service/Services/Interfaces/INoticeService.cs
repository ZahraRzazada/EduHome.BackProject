using System;
using EduHome.Core.DTOS;

using EduHome.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
	public interface INoticeService
	{
        public Task<IEnumerable<NoticeGetDto>> GetAllAsync();

        public Task<CommonResponse> CreateAsync(NoticePostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, NoticePostDto dto);
        public Task<NoticeGetDto> GetAsync(int id);
    }
}

