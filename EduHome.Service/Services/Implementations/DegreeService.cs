using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class DegreeService : IDegreeService
    {
        readonly IDegreeRepository _degreeRepository;

        public DegreeService(IDegreeRepository degreeRepository)
        {
            _degreeRepository = degreeRepository;
        }
        public async Task CreateAsync(DegreePostDto dto)
        {
            Degree degree = new Degree();
            degree.Name = dto.Name;

            await _degreeRepository.AddAsync(degree);
            await _degreeRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<DegreeGetDto>> GetAllAsync()
        {
            IEnumerable<DegreeGetDto> degree = await _degreeRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new DegreeGetDto { Name = x.Name })
               .ToListAsync();
            return degree;
        }
        public async Task<DegreeGetDto> GetAsync(int id)
        {
            Degree? degree = await _degreeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (degree == null)
            {
                throw new ItemNotFoundExcpetion("Degree Not Found");
            }

            DegreeGetDto degreeGetDto = new DegreeGetDto
            {
                Name = degree.Name,

            };
            return degreeGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Degree? degree = await _degreeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (degree == null)
            {
                throw new ItemNotFoundExcpetion("Degree Not Found");
            }
            degree.IsDeleted = true;
            await _degreeRepository.UpdateAsync(degree);
            await _degreeRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, DegreePostDto dto)
        {
            Degree? degree = await _degreeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (degree == null)
            {
                throw new ItemNotFoundExcpetion("Degree Not Found");
            }

            degree.Name = dto.Name;



            await _degreeRepository.UpdateAsync(degree);
            await _degreeRepository.SaveChangesAsync();
        }

    }
}

