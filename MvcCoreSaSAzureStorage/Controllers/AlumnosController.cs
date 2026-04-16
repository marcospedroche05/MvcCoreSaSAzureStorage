using Microsoft.AspNetCore.Mvc;
using MvcCoreSaSAzureStorage.Models;
using MvcCoreSaSAzureStorage.Services;

namespace MvcCoreSaSAzureStorage.Controllers
{
    public class AlumnosController : Controller
    {
        private ServiceAzureAlumnos _service;
        public AlumnosController(ServiceAzureAlumnos service)
        {
            this._service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string curso)
        {
            List<Alumno> alumnos = await this._service.GetAlumnosAsync(curso);
            return View(alumnos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            await this._service.CreateAlumnoAsync(alumno.IdAlumno,
                alumno.Nombre, alumno.Apellidos, alumno.Nota);
            return RedirectToAction("Index");
        }
    }
}
