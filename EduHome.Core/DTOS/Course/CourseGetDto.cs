using System;
using EduHome.Core.Entities;

namespace EduHome.Core.DTOS
{
	public class CourseGetDto
	{
        public CategoryGetDto CategoryGetDto { get; set; }
        public DateTime Date { get; set; }
        public List<Feature> Features { get; set; } = null!;
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string About { get; set; } = null!;
        public string Apply { get; set; } = null!;
        public string Certification { get; set; } = null!;
        public string Image { get; set; } = null!;
        public Category Category { get; set; }
        public IEnumerable<TagGetDto> tags { get; set; } = null!;
       

    }
}

