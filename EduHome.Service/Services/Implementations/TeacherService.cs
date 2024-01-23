using System.Reflection.Metadata;
using EduHome.Core.DTOS;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Repositories;
using EduHome.Service.Exceptions;
using EduHome.Service.Extensions;
using EduHome.Service.Responses;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository, IWebHostEnvironment env, IPositionRepository positionrepository)
        {
            _teacherRepository = teacherRepository;
            _env = env;
            _positionrepository = positionrepository;
        }

        readonly IWebHostEnvironment _env;
        readonly IPositionRepository _positionrepository;

        public async Task<CommonResponse> CreateAsync(TeacherPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Teacher teacher = new Teacher
            {
              Info=dto.Info,
              FullName=dto.FullName,
              DegreeId=dto.DegreeId,
              Experience=dto.Experience,
              PositionId=dto.PositionId,
              Skills=new List<Skill>(),
              SocialNetworks=new List<SocialNetwork>(),

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
            teacher.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "img/teacher");

            for (int i = 0; i < dto.SkillKeys.Count(); i++)
            {
                teacher.Skills.Add(new Skill
                {
                    Teacher = teacher,
                    Key = dto.SkillKeys[i],
                    Value = dto.SkillValues[i],
                });
            }

     
            if (dto.Icons == null || dto.Urls == null || dto.Icons.Count() != dto.Urls.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }

            if (dto.Icons.Any(x => string.IsNullOrWhiteSpace(x)) || dto.Urls.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }

            if (!await CheckPosition(dto.PositionId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Position is not valid";
                return commonResponse;
            }

            teacher.Storage = "wwwroot";

           
            await _teacherRepository.AddAsync(teacher);
            await _teacherRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<IEnumerable<TeacherGetDto>> GetAllAsync()
        {
            var query = _teacherRepository.GetQuery(x => x.IsDeleted == false)
            .Include(x => x.SocialNetworks)
            .Include(x => x.Position);
        
            IEnumerable<TeacherGetDto> teacherGetDtos = query.Select(x =>
            new TeacherGetDto
            {
                Image=x.Image,
                Id=x.Id,
                FullName=x.FullName,
                SocialNetworks=x.SocialNetworks,
                Position = new PositionGetDto { Name = x.Position.Name },
            }
            );
            return teacherGetDtos;
        }

        public async Task<TeacherGetDto> GetAsync(int id)
        {
            var query = _teacherRepository.GetQuery(x => x.IsDeleted == false && x.Id == id)
                .Include(x => x.TeacherFaculties)
                .ThenInclude(x => x.Faculty)
                .Include(x => x.TeacherHobbies)
                .ThenInclude(x=>x.Hobby)
                .Include(x => x.Degree)
                .Include(x => x.Position)
                .Include(x => x.SocialNetworks)
                .Include(x => x.Skills);

            TeacherGetDto? teacher = await query.Select(x => new TeacherGetDto
            {
                Id = x.Id,
                Experience = x.Experience,
                Info =x.Info,
                Skills = x.Skills,
                Hobbies=x.TeacherHobbies.Select(y=> new HobbyGetDto { Name=y.Hobby.Name}),
                SocialNetworks = x.SocialNetworks,
                Image = x.Image,
                FullName = x.FullName,
                DegreeId = x.DegreeId,
                PositionId = x.PositionId,
                Degree = new DegreeGetDto { Name = x.Degree.Name },
                Position = new PositionGetDto { Name = x.Position.Name },
                Faculties = x.TeacherFaculties.Select(y => new FacultyGetDto { Name = y.Faculty.Name })
            }).FirstOrDefaultAsync();

            if (teacher == null)
            {
                throw new ItemNotFoundExcpetion("Teacher Not Found");
            }

            return teacher;
        }
            public async Task RemoveAsync(int id)
        {
            Teacher? teacher = await _teacherRepository.GetAsync(x => !x.IsDeleted && x.Id == id );

            if (teacher == null)
            {
                throw new ItemNotFoundExcpetion("Teacher Not Found");
            }
            teacher.IsDeleted = true;
            await _teacherRepository.UpdateAsync(teacher);
            await _teacherRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, TeacherPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };
            if (dto.Icons == null || dto.Urls == null || dto.Icons.Count() != dto.Urls.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }

            if (dto.Icons.Any(x => string.IsNullOrWhiteSpace(x)) || dto.Urls.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }


            if (dto.SkillKeys == null || dto.SkillValues == null || dto.SkillKeys.Count() != dto.SkillValues.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }

            if (dto.SkillKeys.Any(x => string.IsNullOrWhiteSpace(x)) || dto.SkillValues.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }
            if (!await CheckPosition(dto.PositionId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Position is not valid";
                return commonResponse;
            }


            Teacher? teacher = await _teacherRepository.GetQuery(x => x.IsDeleted == false && x.Id == id)
                .Include(x => x.TeacherFaculties)
                .ThenInclude(x => x.Faculty)
                .Include(x => x.TeacherHobbies)
                .ThenInclude(x => x.Hobby)
                .Include(x => x.Degree)
                .Include(x => x.Position)
                .Include(x => x.SocialNetworks)
                .Include(x => x.Skills).FirstOrDefaultAsync();


            if (teacher == null)
            {
                throw new ItemNotFoundExcpetion("Teacher Not Found");
            }

            teacher.FullName = dto.FullName;
            teacher.Info = dto.Info;
            teacher.PositionId = dto.PositionId;
            teacher.Experience = dto.Experience;
            teacher.DegreeId = dto.DegreeId;
            teacher.TeacherFaculties.Clear();
            teacher.TeacherHobbies.Clear();
            foreach (var item in dto.FacultyIds)
            {
                teacher.TeacherFaculties.Add(new TeacherFaculty
                {
                    Teacher = teacher,
                    FacultyId = item,
                });
            }
            foreach (var item in dto.HobbyIds)
            {
                teacher.TeacherHobbies.Add(new TeacherHobbies
                {
                    Teacher = teacher,
                    HobbyId = item,
                });
            }
            teacher.SocialNetworks.Clear();
            CreateSochial(dto, teacher);
            teacher.Skills.Clear();
            for (int i = 0; i < dto.SkillKeys.Count(); i++)
            {
                teacher.Skills.Add(new Skill
                {
                    Teacher = teacher,
                    Key = dto.SkillKeys[i],
                    Value = dto.SkillValues[i],
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

                teacher.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "img/teacher");
            }
            return commonResponse;

        }

        private void CreateSochial(TeacherPostDto dto, Teacher teacher)
        {
            for (int i = 0; i < dto.Icons.Count(); i++)
            {
                SocialNetwork socialNetwork = new SocialNetwork
                {
                    Teacher = teacher,
                    Icon = dto.Icons[i],
                    Url = dto.Urls[i]
                };
                teacher.SocialNetworks.Add(socialNetwork);
            }
        }

        private async Task<bool> CheckPosition(int id)
        {
            return await _positionrepository.GetQuery(x => !x.IsDeleted && x.Id == id).CountAsync() > 0;
        }
    }
}

