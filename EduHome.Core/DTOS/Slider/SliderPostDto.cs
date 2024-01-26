using System;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS;

public record SliderPostDto
{
   
	public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile? ImageFile { get; set; }
    public DateTime CreatedAt { get; set; }

}

