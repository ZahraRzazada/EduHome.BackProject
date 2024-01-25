using System;
using EduHome.Core.DTOS;

namespace EduHome.App.ViewModels
{
	public class AboutViewModel
	{
        public IEnumerable<TeacherGetDto> Teachers { get; set; }
        public IEnumerable<TestimonialGetDto> Testimoials { get; set; }
        //public  AboutGetDto About { get; set; }
    }
}

