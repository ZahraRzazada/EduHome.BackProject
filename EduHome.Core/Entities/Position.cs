using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Position:BaseEntity
	{
        public string Name { get; set; } = null!;
        public List<Teacher> Teachers { get; set; }
    }
}

