using RegistroProductos.Data;
using RegistroProductos.Models;

namespace RegistroProductos
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public MainPage()
        {
            InitializeComponent();

            _databaseService = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _databaseService.InitializeAsync();
        }

        private async void OnGuardarProductoClicked(object sender, EventArgs e)
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Error", "Ingrese el nombre del producto.", "Aceptar");
                return;
            }

            // Validar descripción
            if (string.IsNullOrWhiteSpace(DescripcionEntry.Text))
            {
                await DisplayAlert("Error", "Ingrese la descripción del producto.", "Aceptar");
                return;
            }

            // Validar precio
            if (!double.TryParse(PrecioEntry.Text, out double precio) || precio <= 0)
            {
                await DisplayAlert("Error", "Ingrese un precio válido mayor que 0.", "Aceptar");
                return;
            }

            // Validar cantidad
            if (!int.TryParse(CantidadEntry.Text, out int cantidad) || cantidad <= 0)
            {
                await DisplayAlert("Error", "Ingrese una cantidad válida mayor que 0.", "Aceptar");
                return;
            }

            // Crear el producto
            Producto producto = new Producto
            {
                Nombre = NombreEntry.Text.Trim(),
                Descripcion = DescripcionEntry.Text.Trim(),
                Precio = precio,
                Cantidad = cantidad,
                FechaRegistro = FechaRegistroPicker.Date.Value
            };

            // Guardar en SQLite
            await _databaseService.GuardarProductoAsync(producto);

            // Mostrar mensaje de éxito
            await DisplayAlert(
                "Registro exitoso",
                $"El producto se guardó correctamente.\nID generado: {producto.ID}",
                "Aceptar");

            // Limpiar los campos
            NombreEntry.Text = string.Empty;
            DescripcionEntry.Text = string.Empty;
            PrecioEntry.Text = string.Empty;
            CantidadEntry.Text = string.Empty;
            FechaRegistroPicker.Date = DateTime.Today;
        }
    }
}