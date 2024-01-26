using System;
namespace EduHome.Core.DTOS
{
	public class NoticeGetDto
	{
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}

