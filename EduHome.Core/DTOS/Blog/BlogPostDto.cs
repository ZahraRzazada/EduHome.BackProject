using System;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS
{
	public class BlogPostDto
	{
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public List<int> TagsIds { get; set; } = null!;
 
    }
}

