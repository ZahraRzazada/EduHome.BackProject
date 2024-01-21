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
        public string Info { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<Skill> Skills { get; set; } = null!;
        public List<SocialNetwork> SocialNetworks { get; set; }
        public int PositionId { get; set; }
        public int DegreeId { get; set; }
        public int HobbyId { get; set; }
        public IEnumerable<FacultyGetDto> Faculties { get; set; } = null!;
        public PositionGetDto Position { get; set; }
        public DegreeGetDto Degree { get; set; }
        public HobbyGetDto Hobby { get; set; }    
        public List<TeacherFaculty> TeacherFaculties { get; set; }
       

    }
}

