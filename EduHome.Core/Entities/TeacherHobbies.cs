using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
    public class TeacherHobbies : BaseEntity
	{
        public int HobbyId { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public Hobby Hobby { get; set; } = null!;
    }
}

