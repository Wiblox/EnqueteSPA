using EnquteSPA.bo;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for OuvertureEnquete.xaml
    /// </summary>
    public partial class OuvertureEnquete : MetroWindow
    {
        int idenquete;
        Enquete en;
        public OuvertureEnquete(int id)
        {
            idenquete = id;
            using var db = new Context();
            en = db.Enquete.Find(id);
            InitializeComponent();
            this.Title = "Enquete : " + en.NoEnquete;
        }

        private void DataGrid_Initialized(object sender, EventArgs e)
        {
            using var db = new Context();
            GridVisite.ItemsSource = db.Visite.Where(v => v.IdEnquete == idenquete).ToList();
        }

        private void DataGrid_Initialized_1(object sender, EventArgs e)
        {
            using var db = new Context();
            GridDocument.ItemsSource = db.Document.Where(v => v.NoEnquete == idenquete).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDocument add = new AddDocument(idenquete);
            add.ShowDialog();
            using var db = new Context();
            GridDocument.ItemsSource = db.Document.Where(v => v.NoEnquete == idenquete).ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //open document
            Button fdv = (Button)sender;
           
            string opeen = Directory.GetCurrentDirectory() + "\\" + ((string)fdv.CommandParameter).Replace('/', '\\');
            Debug.WriteLine(Directory.GetCurrentDirectory() + "\\" + ((string)fdv.CommandParameter).Replace('/','\\' ));

            Process photoViewer = new Process();
            photoViewer.StartInfo.FileName = @"explorer.exe";
            photoViewer.StartInfo.Arguments = @opeen;
            photoViewer.Start();

        }
    }
}
