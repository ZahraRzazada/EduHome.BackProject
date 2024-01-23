using System;
using EduHome.Core.DTOS;

using EduHome.Core.Entities;

namespace EduHome.Core.DTOS
{
	public record TeacherGetDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int Experience { get; set; }
        public string Info { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<Skill> Skills { get; set; } = null!;
        public List<SocialNetwork> SocialNetworks { get; set; }
        public int PositionId { get; set; }
        public int DegreeId { get; set; }
        public PositionGetDto Position { get; set; }
        public DegreeGetDto Degree { get; set; }
        public IEnumerable<HobbyGetDto> Hobbies { get; set; } = null!;
        public IEnumerable<FacultyGetDto> Faculties { get; set; } = null!;
        public List<TeacherHobbies> TeacherHobbies { get; set; }
        public List<TeacherFaculty> TeacherFaculties { get; set; }


    }
}

