using System;
using EduHome.Core.DTOS;


namespace EduHome.App.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<TestimonialGetDto> Testimoials { get; set; }
        public IEnumerable<SliderGetDto> Sliders { get; set; }
    }
}

