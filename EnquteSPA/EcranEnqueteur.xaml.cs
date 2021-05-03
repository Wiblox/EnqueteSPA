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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnquteSPA.bo;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for EcranEnqueteur.xaml
    /// </summary>
    public partial class EcranEnqueteur : UserControl
    {
        public EcranEnqueteur()
        {
            InitializeComponent();
        }

        private void data_Initialized(object sender, EventArgs e)
        {
            using var db = new Context();
            data.ItemsSource = db.SpaPersonne.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSpaPersonne fenetre = new AddSpaPersonne();
            fenetre.ShowDialog();
            using var db = new Context();
            data.ItemsSource = db.SpaPersonne.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
