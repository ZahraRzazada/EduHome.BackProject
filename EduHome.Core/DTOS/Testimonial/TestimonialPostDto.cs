using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS
{
	public record TestimonialPostDto
	{
        public string Text { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public IFormFile? ImageFile { get; set; } = null!;
        public string Position { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
       
    }
}

