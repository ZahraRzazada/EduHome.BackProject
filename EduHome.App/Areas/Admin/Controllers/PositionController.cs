using EduHome.Core.DTOS;

using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        readonly IPositionService _positionService;

        public PositionController(IPositionService PositionService)
        {
            _positionService = PositionService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _positionService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _positionService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _positionService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _positionService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, PositionPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _positionService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

    }
}