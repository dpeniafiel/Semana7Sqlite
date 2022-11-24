using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Semana7Sqlite.Droid;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly:Xamarin.Forms.Dependency(typeof(SqlCliente))]
namespace Semana7Sqlite.Droid
{
    public class SqlCliente : Database
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentPath= System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = System.IO.Path.Combine(documentPath, "uisrael.db3");
            return new SQLiteAsyncConnection(path);
            
        }
    }
}