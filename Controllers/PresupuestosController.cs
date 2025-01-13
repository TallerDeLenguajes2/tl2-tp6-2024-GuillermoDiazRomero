using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
using persistence;
[ApiController]
[Route("[Controller]")]

public class PresupuestosController : Controller
{
    private readonly ILogger<PresupuestosController> _logger;
    private readonly PresupuestoRepository repoPresu;
    public PresupuestosController(ILogger<PresupuestosController> logger){
        _logger = logger;
        repoPresu = new PresupuestoRepository();
    }

    [HttpGet("ListarPresupuestos")]
    public IActionResult ListarPresupuestos(){
        return View(repoPresu.ListarPresupuestos());
    }

    [HttpGet("Detalle")]
    public IActionResult Detalle(int id){
        return View (repoPresu.ObtenerDetalles(id));
    }

    [HttpGet("Crear")]
    public IActionResult Crear(){
        return View ();
    }

    [HttpPost("NuevoPresupuestoString")]
    public IActionResult NuevoPresupuesto([FromForm] string nomDestinatario)
    {
        Presupuestos nuevoPresupuesto = new Presupuestos();
        nuevoPresupuesto.NombreDestinatario = nomDestinatario;
        nuevoPresupuesto.FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd");
        nuevoPresupuesto.Detalle = new List<PresupuestosDetalle>();
        repoPresu.CrearPresupuesto(nuevoPresupuesto);
        return RedirectToAction("ListarPresupuestos");
    }
}