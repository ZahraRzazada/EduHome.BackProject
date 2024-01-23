
using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Category:BaseEntity
	{
        public string Name { get; set; } = null!;
        public List<Blog> Blogs { get; set; }
        public List<Course> Courses { get; set; }
    }
}

