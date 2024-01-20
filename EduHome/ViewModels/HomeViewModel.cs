using System;
using EduHome.Core.DTOS;

namespace EduHome.ViewModels
{
	public class HomeViewModel
	{

        public IEnumerable<SliderGetDto> Sliders { get; set; }
        public IEnumerable<TestimonialGetDto> Testimonials { get; set; }
    }
}

