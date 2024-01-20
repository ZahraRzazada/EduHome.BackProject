using System;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.DTOS
{
	public class TestimonialGetDto
	{
        public string Text { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Position { get; set; } = null!;
       
    }
}

