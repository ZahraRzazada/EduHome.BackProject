using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class SocialNetwork:BaseEntity
	{
        public string Icon { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
    }
}

