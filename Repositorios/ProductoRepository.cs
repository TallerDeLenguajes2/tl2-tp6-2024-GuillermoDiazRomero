using Microsoft.Data.Sqlite;
using Models;


namespace persistence;

public class ProductoRepository
{
    private string conexionString = "Data Source=db/Tienda.db;Cache=Shared";

    public void CrearProducto(Producto prod)
    {
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = "INSERT INTO Productos (idProducto,Descripcion, Precio) VALUES ((SELECT MAX(idProducto) FROM Productos)+1,@dscr, @precio)";
            conexion.Open();
            var command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@dscr", prod.Descripcion));
            command.Parameters.Add(new SqliteParameter("@precio", prod.Precio));
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void ModificarProducto(int id, Producto prod)
    {
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = @"UPDATE Productos
                    SET Descripcion = @dscr, Precio = @precio
                    WHERE idProducto = @id";
            conexion.Open();
            var command = new SqliteCommand(query, conexion);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@dscr", prod.Descripcion);  // Utilizamos AddWithValue
            command.Parameters.AddWithValue("@precio", prod.Precio);
            command.ExecuteNonQuery();
            conexion.Close(); //Es innecesario ya que el mismo using se encarga de cerrar la conexion
        }
    }

    public List<Producto> ListarProductos()
    {
        List<Producto> listado = new List<Producto>();

        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = "SELECT * FROM Productos";
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Producto prod = new Producto(Convert.ToInt32(reader["idProducto"]), Convert.ToString(reader["Descripcion"]) ?? "No tiene descripcion", Convert.ToInt32(reader["Precio"]));
                    listado.Add(prod);
                }
                conexion.Close();
            }
        }
        return listado;
    }

    public Producto DetallesProducto(int id)
    {
        Producto prod;
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            var query = "SELECT * FROM Productos WHERE idProducto = @id";
            conexion.Open();
            var command = new SqliteCommand(query, conexion);
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                prod = new Producto(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]) ?? "No tiene descripcion", Convert.ToInt32(reader[2]));
                conexion.Close();
            }
        }
        return prod;

    }

    public void EliminarProducto(int id)
    {
        using (SqliteConnection conexion = new SqliteConnection(conexionString))
        {
            conexion.Open();
            var query = "DELETE FROM Productos WHERE idProducto = (@id)";
            var command = new SqliteCommand(query, conexion);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }
}