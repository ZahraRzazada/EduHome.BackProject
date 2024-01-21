using System;
namespace EduHome.Core.DTOS
{
	public record HobbyGetDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

