using System;
using EduHome.Core.DTOS;


namespace EduHome.App.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<TestimonialGetDto> Testimoials { get; set; }
        public IEnumerable<SliderGetDto> Sliders { get; set; }
        public IEnumerable<CourseGetDto> Courses { get; set; }
        public IEnumerable<BlogGetDto> Blogs { get; set; }

    }
}

