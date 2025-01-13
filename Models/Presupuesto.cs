namespace Models;

public class Presupuestos{
    private int idPresupuesto;
    private string nombreDestinatario;
    private string? fechaCreacion;
    List<PresupuestosDetalle>? detalle;

    public Presupuestos(int idPresupuesto, string nombreDestinatario, string fechaCreacion, List<PresupuestosDetalle> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        this.Detalle = detalle;
    }
    public Presupuestos(int idPresupuesto, string nombreDestinatario, string fechaCreacion)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        Detalle = new List<PresupuestosDetalle>();
    }

    public Presupuestos(string nombreDestinatario)
    {
        this.nombreDestinatario = nombreDestinatario;
        Detalle = new List<PresupuestosDetalle>();

    }

    public Presupuestos(){}


    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
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