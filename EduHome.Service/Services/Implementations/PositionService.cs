using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class PositionService : IPositionService
    {

        readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task CreateAsync(PositionPostDto dto)
        {
            Position position = new Position();
            position.Name = dto.Name;

            await _positionRepository.AddAsync(position);
            await _positionRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<PositionGetDto>> GetAllAsync()
        {
            IEnumerable<PositionGetDto> positionGetDtos = await _positionRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new PositionGetDto { Name = x.Name,Id=x.Id })
               .ToListAsync();
            return positionGetDtos;
        }
        public async Task<PositionGetDto> GetAsync(int id)
        {
            Position? position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (position == null)
            {
                throw new ItemNotFoundExcpetion("Position Not Found");
            }

            PositionGetDto positionGetDto = new PositionGetDto
            {
                Name = position.Name,

            };
            return positionGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Position? position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (position == null)
            {
                throw new ItemNotFoundExcpetion("Position Not Found");
            }
            position.IsDeleted = true;
            await _positionRepository.UpdateAsync(position);
            await _positionRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, PositionPostDto dto)
        {
            Position? position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (position == null)
            {
                throw new ItemNotFoundExcpetion("Position Not Found");
            }

            position.Name = dto.Name;



            await _positionRepository.UpdateAsync(position);
            await _positionRepository.SaveChangesAsync();
        }
    }
}

