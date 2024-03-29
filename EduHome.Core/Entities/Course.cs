﻿using System;
using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
	public class Course:BaseEntity
	{
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string About { get; set; } = null!;
        public string Apply { get; set; } = null!;
        public string Certification { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<TagCourse> TagCourses { get; set; }
        public List<Feature> Features { get; set; } = null!;
    }
}

