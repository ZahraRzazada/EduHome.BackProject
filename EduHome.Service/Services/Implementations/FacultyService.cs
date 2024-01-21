using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class FacultyService : IFacultyService
    {

        readonly IFacultyRepository _facultyRepository;

        public FacultyService(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task CreateAsync(FacultyPostDto dto)
        {
            Faculty faculty = new Faculty();
            faculty.Name = dto.Name;

            await _facultyRepository.AddAsync(faculty);
            await _facultyRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<FacultyGetDto>> GetAllAsync()
        {
            IEnumerable<FacultyGetDto> faculty = await _facultyRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution().Select(x => new FacultyGetDto { Name = x.Name })
               .ToListAsync();
            return faculty;
        }
        public async Task<FacultyGetDto> GetAsync(int id)
        {
            Faculty? faculty = await _facultyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (faculty == null)
            {
                throw new ItemNotFoundExcpetion("faculty Not Found");
            }

            FacultyGetDto facultyGetDto = new FacultyGetDto
            {
                Name = faculty.Name,

            };
            return facultyGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Faculty? faculty = await _facultyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (faculty == null)
            {
                throw new ItemNotFoundExcpetion("Faculty Not Found");
            }
            faculty.IsDeleted = true;
            await _facultyRepository.UpdateAsync(faculty);
            await _facultyRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, FacultyPostDto dto)
        {
            Faculty? faculty = await _facultyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (faculty == null)
            {
                throw new ItemNotFoundExcpetion("Faculty Not Found");
            }

            faculty.Name = dto.Name;



            await _facultyRepository.UpdateAsync(faculty);
            await _facultyRepository.SaveChangesAsync();
        }
    }
}

