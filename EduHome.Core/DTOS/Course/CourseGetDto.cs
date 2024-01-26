using System;
using EduHome.Core.Entities;

namespace EduHome.Core.DTOS
{
	public class CourseGetDto
	{
        public string Name { get; set; } = null!;
        public CategoryGetDto CategoryGetDto { get; set; }
        public DateTime Date { get; set; }
        public List<string> FeatureKeys { get; set; } = null!;
        public List<string> FeatureValues { get; set; } = null!;
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

