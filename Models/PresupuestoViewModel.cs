using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models;

public class CrearPresupuestoViewModel
{
    private int _idCliente;
    private string _fechaCreacion;
    private IEnumerable<SelectListItem> _clientes;

    public CrearPresupuestoViewModel(IEnumerable<SelectListItem> clientes)
    {
        _fechaCreacion = string.Empty;
        _idCliente = 0;
        _clientes = clientes;
    }

    public CrearPresupuestoViewModel()
    {
        _fechaCreacion = string.Empty;
        _idCliente = 0;
        _clientes = new List<SelectListItem>();
    }

    public string FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
    public IEnumerable<SelectListItem> Clientes { get => _clientes; set => _clientes = value; }
    public int IdCliente { get => _idCliente; set => _idCliente = value; }
}