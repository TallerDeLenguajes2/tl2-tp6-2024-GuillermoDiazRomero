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


    // [HttpGet]
    // public ActionResult<List<Productos>> GetListarProducto(){
    //     List<Productos> lista = new List<Productos>();

    //     return lista;
    // }

    [HttpGet]
    public IActionResult ObtenerProductos()
    {   
        return View(repoProd.ListarProductos());
    }
}