using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class TestimonialController : Controller
    {
        readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _testimonialService.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestimonialPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _testimonialService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
          
            return View(await _testimonialService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, TestimonialPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _testimonialService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _testimonialService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

