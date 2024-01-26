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
    public class NoticeService : INoticeService
    {
        readonly INoticeRepository _noticeRepository;
   
        readonly IWebHostEnvironment _env;

        public NoticeService(INoticeRepository noticeRepository, IWebHostEnvironment env)
        {
            _noticeRepository = noticeRepository;
 
            _env = env;
        }

        public async Task<CommonResponse> CreateAsync(NoticePostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };

            Notice notice = new Notice();
            notice.Description = dto.Description;
        
            await _noticeRepository.AddAsync(notice);
            await _noticeRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<IEnumerable<NoticeGetDto>> GetAllAsync()
        {
           
           IEnumerable< NoticeGetDto> notice = await _noticeRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().
               Select(x =>
               new NoticeGetDto
               {
                 Description=x.Description,
                 Id=x.Id
                  
               })
               .ToListAsync();
            return notice;
        }

        public async Task<NoticeGetDto> GetAsync(int id)
        {
            Notice? notice = await _noticeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (notice == null)
            {
                throw new ItemNotFoundExcpetion("Notice Not Found");
            }

            NoticeGetDto noticeGetDto = new NoticeGetDto
            {
                Id = notice.Id,
                Description = notice.Description,
            };
            return noticeGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Notice? notice = await _noticeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (notice == null)
            {
                throw new ItemNotFoundExcpetion("Notice Not Found");
            }
            notice.IsDeleted = true;
            await _noticeRepository.UpdateAsync(notice);
            await _noticeRepository.SaveChangesAsync();
        }


        public async Task<CommonResponse> UpdateAsync(int id, NoticePostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };

            Notice? notice = await _noticeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (notice == null)
            {
                throw new ItemNotFoundExcpetion("Notice Not Found");
            }

            notice.Description = dto.Description;

            await _noticeRepository.UpdateAsync(notice);
            await _noticeRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}

