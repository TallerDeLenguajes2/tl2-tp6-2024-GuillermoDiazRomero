using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using persistence;
[ApiController]
[Route("[Controller]")]

public class PresupuestosController : Controller
{
    private readonly ILogger<PresupuestosController> _logger;
    private readonly PresupuestoRepository repoPresu;
    private readonly ProductoRepository repoProd;
    private readonly ClienteRepository repoClien;
    public PresupuestosController(ILogger<PresupuestosController> logger){
        _logger = logger;
        repoPresu = new PresupuestoRepository();
        repoProd = new ProductoRepository();
    }

    [HttpGet("ListarPresupuestos")]
    public IActionResult ListarPresupuestos(){
        return View(repoPresu.ListarPresupuestos());
    }

    [HttpGet("Detalle")]
    public IActionResult Detalle(int id){
        ViewBag.Id = id;
        return View(repoPresu.ObtenerDetalles(id));
    }

    [HttpGet("Crear")]
    public IActionResult Crear(){
        var clientes = repoClien.ListarClientes()
                                      .Select(c => new SelectListItem()
                                            {
                                                Value = c.IdCliente.ToString(),
                                                Text = c.Nombre
                                            });
    return View(new CrearPresupuestoViewModel(clientes));
    }

    [HttpPost("NuevoPresupuestoString")]
    public IActionResult NuevoPresupuesto([FromForm] int idCliente)
    {
        if (!ModelState.IsValid)
        {
            return View("Crear");
        }
        Presupuestos nuevoPresupuesto = new Presupuestos();
        Cliente cliente = repoClien.ObtenerCliente(idCliente);
        nuevoPresupuesto.Cliente = cliente;
        nuevoPresupuesto.FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd");
        nuevoPresupuesto.Detalle = new List<PresupuestosDetalle>();
        repoPresu.CrearPresupuesto(nuevoPresupuesto);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("Eliminar")]
    public IActionResult Eliminar(int id){
        // ViewBag.presu = repoPresu.ObtenerPresupuesto(id);
        ViewBag.Objeto1 = repoPresu.ObtenerDetalles(id);
        return View(repoPresu.ObtenerPresupuesto(id));
    }

    [HttpGet("EliminarPresupuesto")]
    public IActionResult EliminarPresupuesto(int id){
        repoPresu.EliminarProducto(id);
        return RedirectToAction("ListarPresupuestos");
    }

    
    [HttpGet("Modificar")]
    public IActionResult Modificar(int id){
        return View(repoPresu.ObtenerPresupuesto(id));
    }
    [HttpPost("ModificarPresupuesto")]
    public IActionResult ModificarPresupuesto([FromForm]Presupuestos nuevoP){
        repoPresu.ModificarPresupuesto(nuevoP.IdPresupuesto,nuevoP);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("Agregar")]
    public IActionResult Agregar(int id){
        var productos = repoProd.ListarProductos()
                                      .Select(c => new SelectListItem()
                                            {
                                                Value = c.IdProducto.ToString(),
                                                Text = c.Descripcion.ToString(),
                                            });
        return View(new AgregarProductoPresupuesto(productos));
    }

    [HttpPost("AgregarProducto/{id}")]
    public IActionResult AgregarProducto([FromRoute]int id, [FromForm] int Producto, [FromForm] int Cantidad){
        repoPresu.AgregarPresupuesto(id,Producto,Cantidad);
        return RedirectToAction("ListarPresupuestos");
    }

}