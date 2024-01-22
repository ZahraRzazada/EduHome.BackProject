using System;
namespace EduHome.Core.Entities.BaseEntities
{
	public class CategoryBlog
	{
        public int CategoryId { get; set; }
        public int BlogId { get; set; }
        public Category Category { get; set; } = null!;
        public Blog Blog { get; set; } = null!;

    }
}

