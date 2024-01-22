using System;
namespace EduHome.Core.DTOS
{
	public class AuthorGetDto
	{
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}

