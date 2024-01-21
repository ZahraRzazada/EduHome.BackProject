using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{ 
	public class Faculty : BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<TeacherFaculty> TeacherFaculties { get; set; }
    }
}


