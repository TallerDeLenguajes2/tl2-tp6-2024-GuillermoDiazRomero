using Microsoft.Data.Sqlite;
using Models;

namespace persistence;

public class PresupuestoRepository
{
    private string conexionString = "Data Source=db/Tienda.db;Cache=Shared";
    public void CrearPresupuesto(Presupuestos nuevoPresupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(conexionString))
        {
            var query = "INSERT INTO Presupuestos (idPresupuesto,NombreDestinatario, FechaCreacion) VALUES ((SELECT MAX(idPresupuesto) FROM Presupuestos)+1,@nomDes,@feCre)";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nomDes", nuevoPresupuesto.NombreDestinatario);
            command.Parameters.AddWithValue("@feCre", nuevoPresupuesto.FechaCreacion);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public List<Presupuestos> ListarPresupuestos()
    {
        List<Presupuestos> Lista = new List<Presupuestos>();
        using (SqliteConnection connection = new SqliteConnection(conexionString))
        {
            var query = "SELECT * FROM Presupuestos";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Presupuestos presupuestos = new Presupuestos(Convert.ToInt32(reader["idPresupuesto"]), Convert.ToString(reader["NombreDestinatario"]) ?? "NULL", Convert.ToString(reader["FechaCreacion"]) ?? "NULL");
                    Lista.Add(presupuestos);
                }
            }
            connection.Close();
        }
        return Lista;
    }

    public Presupuestos ObtenerPresupuesto(int id)
    {
        Presupuestos p;
        using (SqliteConnection connection = new SqliteConnection(conexionString))
        {
            var query = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                p = new Presupuestos(Convert.ToInt32(reader["idPresupuesto"]), Convert.ToString(reader["NombreDestinatario"]) ?? "NULL", Convert.ToString(reader["FechaCreacion"]));
                connection.Close();
            }
        }
        return p;
    }
    public List<PresupuestosDetalle> ObtenerDetalles(int idPresupuesto)
    {
        List<PresupuestosDetalle> lista = new List<PresupuestosDetalle>();
        using (SqliteConnection connection = new SqliteConnection(conexionString))
        {
            var query = "SELECT * FROM PresupuestosDetalle A INNER JOIN Productos B ON A.idProducto = B.idProducto WHERE A.idPresupuesto = @id";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", idPresupuesto);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Producto prod = new Producto(Convert.ToInt32(reader["idProducto"]), Convert.ToString(reader["Descripcion"]) ?? "No tiene descripcion", Convert.ToInt32(reader["Precio"]));
                    PresupuestosDetalle presProd = new PresupuestosDetalle(prod, Convert.ToInt32(reader["Cantidad"]));
                    lista.Add(presProd);
                }
                connection.Close();
            }
        }
        return lista;
    }


    public void AgregarPresupuesto(int idPresupuesto, int idProducto, int cant)
    {
        using (SqliteConnection connection = new SqliteConnection(conexionString))
        {
            var query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@pres,@prod,@cant)";
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@pres", idPresupuesto);
            command.Parameters.AddWithValue("@prod", idProducto);
            command.Parameters.AddWithValue("@cant", cant);
            command.ExecuteNonQuery();
            connection.Close();
        }

    }


    public void EliminarProducto(int idPresupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(conexionString))
        {
            connection.Open();

            var query1 = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
            SqliteCommand command1 = new SqliteCommand(query1, connection);
            command1.Parameters.AddWithValue("@id", idPresupuesto);
            command1.ExecuteNonQuery();

            var query2 = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
            SqliteCommand command2 = new SqliteCommand(query2, connection);
            command2.Parameters.AddWithValue("@id", idPresupuesto);
            command2.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void ModificarPresupuesto(int id, Presupuestos p){
        using (SqliteConnection connection = new SqliteConnection(conexionString)){
            connection.Open();
            var query = "UPDATE Presupuestos SET NombreDestinatario = @nom, FechaCreacion = @fec WHERE idPresupuesto = @id";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id",id);
            command.Parameters.AddWithValue("@nom",p.NombreDestinatario);
            command.Parameters.AddWithValue("@fec",p.FechaCreacion);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

}