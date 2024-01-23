using System;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS
{
	public class CoursePostDto
	{
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public string About { get; set; } = null!;
        public string Apply { get; set; } = null!;
        public string Certification { get; set; } = null!;
        public int CategoryId { get; set; }
        public List<int> TagsIds { get; set; } = null!;
        public List<string> FeatureKeys { get; set; } = null!;
        public List<string> FeatureValues { get; set; } = null!;
    }
}

