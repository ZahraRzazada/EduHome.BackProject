using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Controllers
{
    public class TeacherController : Controller
    {
        readonly IDegreeService _degreeService;
        readonly IFacultyService _facultyService;
        readonly IPositionService _positionService;
        readonly ITeacherService _teacherService;

        public TeacherController(IDegreeService degreeService, IFacultyService facultyService, IPositionService positionService, ITeacherService teacherService)
        {
            _degreeService = degreeService;
            _facultyService = facultyService;
            _positionService = positionService;
            _teacherService = teacherService;
        }
    


        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<TeacherGetDto> Teachers = await _teacherService.GetAllAsync();
            return View(Teachers);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var taecherGetDto = await _teacherService.GetAsync(id);
            return View(taecherGetDto);
        }
    }
}

