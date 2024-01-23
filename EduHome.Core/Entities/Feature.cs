using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Feature:BaseEntity
	{
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}

