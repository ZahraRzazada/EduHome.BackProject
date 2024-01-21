using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Skill:BaseEntity
	{
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
    }
}

