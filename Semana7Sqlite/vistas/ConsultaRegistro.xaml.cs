using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using Semana7Sqlite.modelos;

namespace Semana7Sqlite.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Estudiante> estudiantes;
        public ConsultaRegistro()
        {
            InitializeComponent();
            _connection= DependencyService.Get<Database>().GetConnection();
            NavigationPage.SetHasBackButton(this, false);
            CargarDatos();
        }

        private async void CargarDatos()
        {
            var resultado = await _connection.Table<Estudiante>().ToListAsync();
            estudiantes = new ObservableCollection<Estudiante>(resultado);
            listEstudiantes.ItemsSource = estudiantes;
        }

        private void listEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Estudiante objeto = (Estudiante)e.SelectedItem;
            var itemId= objeto.Id.ToString();
            var idSeleccionado = Int32.Parse(itemId);
            Navigation.PushAsync(new Elemento(idSeleccionado));

        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}