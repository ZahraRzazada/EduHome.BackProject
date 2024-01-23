using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Hobby:BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<TeacherHobbies> TeacherHobbies { get; set; }

    }
}

