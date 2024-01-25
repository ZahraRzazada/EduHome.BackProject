using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacultyController : Controller
    {
        readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task<IActionResult> Index()
        {
        
            return View(await _facultyService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacultyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _facultyService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _facultyService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _facultyService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, FacultyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _facultyService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

    }
}

