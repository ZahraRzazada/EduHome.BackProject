using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Author:BaseEntity
	{
        public string FullName { get; set; } = null!;
        public List<Blog> Blogs { get; set; }
        
    }
}

