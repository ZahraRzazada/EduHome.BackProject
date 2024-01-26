using System;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS
{
	public class NoticePostDto
	{
        
        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}

