using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class HobbyService : IHobbyService
    {

        readonly IHobbyRepository _hobbyRepository;

        public HobbyService(IHobbyRepository hobbyRepository)
        {
            _hobbyRepository = hobbyRepository;
        }

        public async Task CreateAsync(HobbyPostDto dto)
        {
            Hobby hobby = new Hobby();
            hobby.Name = dto.Name;

            await _hobbyRepository.AddAsync(hobby);
            await _hobbyRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<HobbyGetDto>> GetAllAsync()
        {
            IEnumerable<HobbyGetDto> hobbies = await _hobbyRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new HobbyGetDto { Name = x.Name, Id = x.Id })
               .ToListAsync();
            return hobbies;
        }
        public async Task<HobbyGetDto> GetAsync(int id)
        {
            Hobby? hobby = await _hobbyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (hobby == null)
            {
                throw new ItemNotFoundExcpetion("hobby Not Found");
            }

            HobbyGetDto hobbyGetDto = new HobbyGetDto
            {
                Name = hobby.Name,

            };
            return hobbyGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Hobby? hobby = await _hobbyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (hobby == null)
            {
                throw new ItemNotFoundExcpetion("hobby Not Found");
            }
            hobby.IsDeleted = true;
            await _hobbyRepository.UpdateAsync(hobby);
            await _hobbyRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, HobbyPostDto dto)
        {
            Hobby? hobby = await _hobbyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (hobby == null)
            {
                throw new ItemNotFoundExcpetion("hobby Not Found");
            }

            hobby.Name = dto.Name;



            await _hobbyRepository.UpdateAsync(hobby);
            await _hobbyRepository.SaveChangesAsync();
        }
    }
}

