using System;
using System.ComponentModel.DataAnnotations;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Slider:BaseEntity
	{  
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(300)]
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}

