using System;
using EduHome.Core.DTOS;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Teacher:BaseEntity
	{
        public string FullName { get; set; } = null!;
        public string Info { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<SocialNetwork> SocialNetworks { get; set; }
        public int PositionId { get; set; }
        public int DegreeId { get; set; }
        public int Experience { get; set; }
        public Position Position { get; set; }
        public Degree Degree { get; set; }
        public List<TeacherHobbies> TeacherHobbies { get; set; }
        public List<Skill> Skills { get; set; } = null!;
        public List<TeacherFaculty> TeacherFaculties { get; set; }
        public string Storage { get; set; } = null!;
    }
}

