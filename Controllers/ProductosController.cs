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

    [HttpGet("Opciones")]
    public IActionResult Opciones()
    {
        return View();
    }

    [HttpGet("ListarProductos")]
    public IActionResult ListarProductos()
    {
        return View(repoProd.ListarProductos());
    }


}