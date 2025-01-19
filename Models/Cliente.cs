using System.ComponentModel.DataAnnotations;

namespace Models;

public class Cliente{

    private int idCliente;
    private string nombre;
    private string email;
    private string telefono;

    public Cliente(){}

    public Cliente(string nombre, string email, string telefono)
    {
        this.nombre = nombre;
        this.email = email;
        this.telefono = telefono;
    }

    public Cliente(int idCliente, string nombre, string email, string telefono)
    {
        this.idCliente = idCliente;
        this.nombre = nombre;
        this.email = email;
        this.telefono = telefono;
    }

    public int IdCliente { get => idCliente; set => idCliente = value; }
    [Required(ErrorMessage ="Error a cargar el nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [Required(ErrorMessage ="Error a cargar el email")]
    [EmailAddress(ErrorMessage ="Error al validar el email")]
    public string Email { get => email; set => email = value; }
    [Required(ErrorMessage ="Error a cargar el telefono")]
    [Phone(ErrorMessage ="Error al validar el telefono")]
    public string Telefono { get => telefono; set => telefono = value; }
}