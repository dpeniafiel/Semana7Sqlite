using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Semana7Sqlite.modelos;

namespace Semana7Sqlite.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Registro()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try {
                var DatosRegistro = new Estudiante { Nombre = txtNombre.Text, Usuario= txtUsuario.Text, Contrasenia= txtContrasenia.Text };
                con.InsertAsync(DatosRegistro);
                DisplayAlert("CORRECTO", "Estudiante registrado con éxito", "Aceptar");
                LimpiarCampos();
            } catch (Exception ex)
            {
                DisplayAlert("ERROR", $"No se pudo registrar el estudiante {ex.Message}", "Aceptar");
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtContrasenia.Text = "";
            txtUsuario.Text = "";
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}