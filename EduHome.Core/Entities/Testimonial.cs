using System;
using System.ComponentModel.DataAnnotations;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Testimonial:BaseEntity
	{
        [StringLength(300)]
		public string Text { get; set; } = null!;
        [StringLength(20)]
        public string FullName { get; set; } = null!;
        public string Image { get; set; } = null!;
        [StringLength(20)]
        public string Position { get; set; } = null!;
        
    }
}

