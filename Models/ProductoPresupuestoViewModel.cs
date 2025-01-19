
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models;
public class AgregarProductoPresupuesto{
    private int _idProducto;
    private int _cantidad;
    private IEnumerable<SelectListItem> _producto;
    public AgregarProductoPresupuesto(IEnumerable<SelectListItem> producto)
    {
        _idProducto = 0;
        _cantidad = 0;
        _producto = producto;
    }
    public AgregarProductoPresupuesto(){
        _idProducto = 0;
        _cantidad = 0;
        _producto = new List<SelectListItem>();
    }
    
    
    public int IdProducto { get => _idProducto; set => _idProducto = value; }
    public int Cantidad { get => _cantidad; set => _cantidad = value; }
    public IEnumerable<SelectListItem> Producto { get => _producto; set => _producto = value; }

}