using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
using persistence;
[ApiController]
[Route("[Controller]")]

public class ClienteController : Controller
{

    private readonly ILogger<ClienteController> _logger;
    private readonly ClienteRepository repoClien;

    public ClienteController(ILogger<ClienteController> logger)
    {
        _logger = logger;
        repoClien = new ClienteRepository();
    }


    [HttpGet("ListarClientes")]
    public IActionResult ListarClientes()
    {
        return View(repoClien.ListarClientes());
    }

    [HttpGet("Modificar")]
    public IActionResult Modificar(int id)
    {
        return View(repoClien.ObtenerCliente(id));
    }

    [HttpPost("ModificarCliente")]
    public IActionResult ModificarCliente([FromForm]Cliente clien)
    {
        repoClien.ModificarCliente(clien.IdCliente, clien);
        return RedirectToAction("ListarClientes");
    }

    [HttpGet("Crear")]
    public IActionResult Crear(){
        return View();
    }
    [HttpPost("CrearCliente")]
    public IActionResult CrearCliente([FromForm]Cliente clien)
    {
        repoClien.CrearCliente(clien);
        return RedirectToAction("ListarClientes");
    }

    [HttpPost("Eliminar")]
    public IActionResult Eliminar(int id){
        repoClien.EliminarCliente(id);
        return RedirectToAction("ListarClientes");
    }
}

