using Microsoft.Data.Sqlite;
using Models;


namespace persistence;

public class ClienteRepository
{
    private string conexionString = "Data Source=db/Tienda.db;Cache=Shared";

    public void CrearCliente(Cliente nuevoCliente)
    {
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = "INSERT INTO Cliente (Nombre,Email,Telefono) VALUES (@nom, @email,@tel)";
            conexion.Open();
            var command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@nom", nuevoCliente.Nombre));
            command.Parameters.Add(new SqliteParameter("@email", nuevoCliente.Email));
            command.Parameters.Add(new SqliteParameter("@tel", nuevoCliente.Telefono));
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void ModificarCliente(int id, Cliente nuevoCliente)
    {
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = @"UPDATE Cliente
                    SET Nombre = @nom, Email = @email, Telefono = @tel
                    WHERE idCliente = @id";
            conexion.Open();
            var command = new SqliteCommand(query, conexion);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nom", nuevoCliente.Nombre);
            command.Parameters.AddWithValue("@email", nuevoCliente.Email);
            command.Parameters.AddWithValue("@tel", nuevoCliente.Telefono);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Cliente> ListarClientes()
    {
        List<Cliente> listado = new List<Cliente>();

        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = "SELECT * FROM Cliente";
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cliente nuevoCliente = new Cliente(Convert.ToInt32(reader["idCliente"]), Convert.ToString(reader["Nombre"]) ?? "No tiene nombre", Convert.ToString(reader["Email"]) ?? "No tiene email", Convert.ToString(reader["Telefono"]) ?? "No tiene telefono");
                    listado.Add(nuevoCliente);
                }
                conexion.Close();
            }
        }
        return listado;
    }

    public Cliente ObtenerCliente(int id)
    {
        Cliente nuevoCliente;
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = "SELECT * FROM Cliente WHERE idCliente = @id";
            conexion.Open();
            var command = new SqliteCommand(query, conexion);
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                nuevoCliente = new Cliente(Convert.ToInt32(reader["idCliente"]), Convert.ToString(reader["Nombre"]) ?? "No tiene nombre", Convert.ToString(reader["Email"]) ?? "No tiene email", Convert.ToString(reader["Telefono"]) ?? "No tiene telefono");
                conexion.Close();
            }
        }
        return nuevoCliente;

    }

    public void EliminarCliente(int id)
    {
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            conexion.Open();
            var query = @"DELETE FROM Cliente WHERE idCliente = @id";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            conexion.Close();
        }
    }
}