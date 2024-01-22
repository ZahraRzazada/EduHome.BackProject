using System;
namespace EduHome.Core.DTOS
{
	public class BlogGetDto
	{
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public IEnumerable<TagGetDto> tags { get; set; } = null!;
        public AuthorGetDto AuthorGetDto { get; set; } = null!;
        public CategoryGetDto CategoryGetDto { get; set; }
        public DateTime Date { get; set; }

    }
}

