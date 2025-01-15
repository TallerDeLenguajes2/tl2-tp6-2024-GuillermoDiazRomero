namespace Models;

public class PresupuestosDetalle{
    private Producto producto;
    private int cantidad;

    public PresupuestosDetalle(Producto producto, int cantidad)
    {
        this.producto = producto;
        this.cantidad = cantidad;
    }
    public PresupuestosDetalle(){}

    public Producto Producto { get => producto; set => producto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}