using SQLite;
using RegistroProductos.Models;

namespace RegistroProductos.Data;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        string dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "productos.db3");

        _database = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitializeAsync()
    {
        await _database.CreateTableAsync<Producto>();
    }

    public async Task<int> GuardarProductoAsync(Producto producto)
    {
        return await _database.InsertAsync(producto);
    }
}