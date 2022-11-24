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
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Login()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
        }

        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario, string contrasenia)
        {
            return db.Query<Estudiante>("select * from estudiante where usuario=? and contrasenia=?", usuario, contrasenia);
        }

        private void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var con = new SQLiteConnection(databasePath);
                con.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(con, txtUsuario.Text, txtContrasenia.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new ConsultaRegistro());
                }
                else
                {
                    DisplayAlert("ERROR", "Usuario o contraseña incorrectos o usuario no existe", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", "Usuario o contraseña incorrectos o usuario no existe" + ex.Message, "Aceptar");
            }

        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}