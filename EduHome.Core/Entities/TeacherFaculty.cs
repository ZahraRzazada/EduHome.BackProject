using System;
using System.Reflection.Metadata;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class TeacherFaculty:BaseEntity
	{
        public int FacultyId { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public Faculty Faculty { get; set; } = null!;
    }
}

