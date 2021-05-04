using EnquteSPA.bo;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
        public OuvertureEnquete(int id)
        {
            idenquete = id;
            InitializeComponent();
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
    }
}
