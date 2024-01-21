using System;
namespace EduHome.Core.DTOS;

public record SliderGetDto
{
    public string Id { get; set; }
	public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Image { get; set; } = null!;

}

