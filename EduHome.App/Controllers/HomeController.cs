﻿using System.Diagnostics;
using EduHome.App.ViewModels;
using EduHome.Core.DTOS;
using EduHome.Data.Contexts;
using EduHome.Service.ExternalServices.Interfaces;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using EduHome.Models;

namespace EduHome.App.Controllers;

public class HomeController : Controller
{
  
    readonly ISliderService _sliderService;
    readonly ITestimonialService _testimonialService;
    readonly ICourseService _courseService;
    readonly IBlogService _blogService;
    readonly IEmailService _emailService;
    readonly INoticeService _noticeService;

    public HomeController(ISliderService sliderService, ITestimonialService testimonialService, ICourseService courseService, IBlogService blogService, IEmailService emailService, INoticeService noticeService)
    {
        _sliderService = sliderService;
        _testimonialService = testimonialService;
        _courseService = courseService;
        _blogService = blogService;
        _emailService = emailService;
        _noticeService = noticeService;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel();
        homeViewModel.Sliders = await _sliderService.GetAllAsync();
        homeViewModel.Testimoials = await _testimonialService.GetAllAsync();
        homeViewModel.Courses = await _courseService.GetAllAsync();
        homeViewModel.Blogs = await _blogService.GetAllAsync();
        homeViewModel.Notices = await _noticeService.GetAllAsync();
        return View(homeViewModel);
    }

    public async Task<IActionResult> SendEmail()
    {
        await _emailService.SendEmail("rzazadazz@gmail.com", "Test", "<h1>ELnur will go to east</h1>");
        return Json("ok");
    }
}
