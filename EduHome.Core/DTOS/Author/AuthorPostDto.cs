using System;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.DTOS
{
	public class AuthorPostDto
	{
        public string FullName { get; set; } = null!;
        //public IFormFile? ImageFile { get; set; }
    }
}

