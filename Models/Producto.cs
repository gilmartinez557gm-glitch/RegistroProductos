using SQLite;

namespace RegistroProductos.Models;

public class Producto
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public double Precio { get; set; }

    public int Cantidad { get; set; }

    public DateTime FechaRegistro { get; set; }
}