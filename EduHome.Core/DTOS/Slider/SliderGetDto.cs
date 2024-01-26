using System;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS;

public record SliderGetDto
{
    public int Id { get; set; }
	public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Image { get; set; }
    public DateTime CreatedAt { get; set; }

}

