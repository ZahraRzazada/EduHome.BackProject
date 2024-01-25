using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HobbyController : Controller
    {
        public readonly IHobbyService _hobbyService;

        public HobbyController(IHobbyService hobbyService)
        {
            _hobbyService = hobbyService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _hobbyService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HobbyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _hobbyService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _hobbyService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _hobbyService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, HobbyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _hobbyService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

    }
}

