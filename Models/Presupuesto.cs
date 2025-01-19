namespace Models;

public class Presupuestos{
    private int idPresupuesto;
    private Cliente cliente;
    private string? fechaCreacion;
    List<PresupuestosDetalle>? detalle;

    public Presupuestos(int idPresupuesto, Cliente cliente, string fechaCreacion, List<PresupuestosDetalle> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.Cliente = cliente;
        this.fechaCreacion = fechaCreacion;
        this.Detalle = detalle;
    }
    public Presupuestos(int idPresupuesto, Cliente cliente, string fechaCreacion)
    {
        this.idPresupuesto = idPresupuesto;
        this.Cliente = cliente;
        this.fechaCreacion = fechaCreacion;
        Detalle = new List<PresupuestosDetalle>();
    }

    public Presupuestos(Cliente cliente) 
    {
        this.Cliente = cliente;
        Detalle = new List<PresupuestosDetalle>();

    }

    public Presupuestos(){}


    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public List<PresupuestosDetalle>? Detalle { get => detalle; set => detalle = value; }
    public string? FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }

    public int MontoPresupuesto(){
        return Detalle.Select(d => d.Producto.Precio * d.Cantidad).Sum();
    }
    public double MontoPresupuestoConIVA(){
        return MontoPresupuesto() * 1.21;
    }

    public int CantidadProductor(){
        return Detalle.Select(d => d.Cantidad).Sum();
    }
}