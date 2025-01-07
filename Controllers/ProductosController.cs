using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
using persistence;
[ApiController]
[Route("[Controller]")]

public class ProductosController : Controller
{

    private readonly ILogger<ProductosController> _logger;
    private readonly ProductoRepository repoProd;

    public ProductosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        repoProd = new ProductoRepository();
    }


    [HttpGet("ListarProductos")]
    public IActionResult ListarProductos()
    {
        return View(repoProd.ListarProductos());
    }

    [HttpGet("Modificar")]
    public IActionResult Modificar(int id)
    {
        return View(repoProd.DetallesProducto(id));
    }

    [HttpPost("ModificarProducto")]
    public IActionResult ModificarProducto([FromForm]Producto prod)
    {
        repoProd.ModificarProducto(prod.IdProducto, prod);
        return RedirectToAction("ListarProductos");
    }

    [HttpGet("Agregar")]
    public IActionResult Agregar(){
        return View();
    }
    [HttpPost("CrearProducto")]
    public IActionResult CrearProducto([FromForm]Producto prod)
    {
        repoProd.CrearProducto(prod);
        return RedirectToAction("ListarProductos");
    }

    [HttpGet("Eliminar")]
    public IActionResult Eliminar(int id){
        return View(repoProd.DetallesProducto(id));
    }

    [HttpGet("EliminarProducto/{id:int}")]
    public IActionResult EliminarProducto(int id){
        repoProd.EliminarProducto(id);
        return RedirectToAction("ListarProductos");
    }
}

