using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Degree : BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<Teacher> Teachers { get; set; }
    }
}

