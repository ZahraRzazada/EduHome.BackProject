using System;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS
{
	public record TeacherPostDto
	{
        public string FullName { get; set; } = null!;
        public string Info { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public List<string> SkillKeys { get; set; } = null!;
        public List<string> SkillValues { get; set; } = null!;
        public List<string> Icons { get; set; } = null!;
        public List<string> Urls { get; set; } = null!;
        public int PositionId { get; set; }
        public int DegreeId { get; set; }
        public int HobbyId { get; set; } 
        public List<int> FacultyIds { get; set; } = null!;
    }
}

