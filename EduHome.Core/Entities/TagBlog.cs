using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class TagBlog:BaseEntity
	{
        public int TagId { get; set; }
        public int BlogId { get; set; }
        public Tag Tag { get; set; } = null!;
        public Blog Blog { get; set; } = null!;
    }
}

