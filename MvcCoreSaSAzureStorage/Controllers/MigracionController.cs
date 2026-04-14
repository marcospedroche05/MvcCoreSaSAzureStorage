using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using MvcCoreSaSAzureStorage.Helpers;
using MvcCoreSaSAzureStorage.Models;

namespace MvcCoreSaSAzureStorage.Controllers
{
    public class MigracionController : Controller
    {
        private HelperXml _helper;
        private IConfiguration _conf;
        public MigracionController(HelperXml helper, IConfiguration conf)
        {
            this._helper = helper;
            this._conf = conf;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string accion)
        {
            // EN ESTE METODO LO QUE NECESITAMOS SON LAS KEYS DE AZURE
            // STORAGE
            // ESTA FUNCIONALIDAD DEBERIA ESTAR EN OTRO PROYECTO
            string azureKeys = this._conf.GetValue<string>("AzureKeys:StorageAccount");
            TableServiceClient tableService =
                new TableServiceClient(azureKeys);
            TableClient tableClient = tableService.GetTableClient("alumnos");
            await tableClient.CreateIfNotExistsAsync();
            List<Alumno> alumnos = this._helper.GetAlumnos();
            foreach (Alumno alumno in alumnos)
            {
                await tableClient.AddEntityAsync<Alumno>(alumno);
            }
            ViewData["MENSAJE"] = "Migracion de alumnos completada";
            return View();
        }
    }
}
