using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Tag:BaseEntity
	{
        public string Name { get; set; } = null!;
        public List<TagBlog> TagBlogs { get; set; }
        public List<TagCourse> TagCourses { get; set; }
    }
}

