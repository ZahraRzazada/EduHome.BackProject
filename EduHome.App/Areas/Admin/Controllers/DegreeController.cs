using EduHome.Core.DTOS;

using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DegreeController : Controller
    {
        readonly IDegreeService _degreeService;

        public DegreeController(IDegreeService degreeService)
        {
            _degreeService = degreeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _degreeService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DegreePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _degreeService.CreateAsync(dto);
            return Ok(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _degreeService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _degreeService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, DegreePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _degreeService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

    }
}