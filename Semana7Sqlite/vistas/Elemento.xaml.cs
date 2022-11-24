using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Semana7Sqlite.modelos;
using System.IO;

namespace Semana7Sqlite.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        int id;
        private SQLiteAsyncConnection _con;
        IEnumerable<Estudiante> borrar;
        IEnumerable<Estudiante> actualizar;

        public Elemento(int id)
        {
            InitializeComponent();
            this.id = id;
            _con = DependencyService.Get<Database>().GetConnection();
        }

        public static IEnumerable<Estudiante> Borrar(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("delete from Estudiante where id=?", id);
        }

        public static IEnumerable<Estudiante> Actualizar(SQLiteConnection db, int id, string nombre, string usuario, string contrasenia)
        {
            return db.Query<Estudiante>("update Estudiante set nombre=?, usuario=?, contrasenia=? where id=?", nombre, usuario, contrasenia, id);
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                actualizar = Actualizar(db, id, txtNombre.Text, txtUsuario.Text, txtContrasenia.Text);
                DisplayAlert("CORRECTO", "Estudiante actualizado", "Aceptar");
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", "Error actualizando " + ex.Message, "Aceptar");
            }

        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                borrar = Borrar(db, id);
                DisplayAlert("CORRECTO", "Estudiante eliminado", "Aceptar");
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", "Error eliminando " + ex.Message, "Aceptar");
            }
        }
    }
}