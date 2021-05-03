using EnquteSPA.bo;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddSpaPersonne.xaml
    /// </summary>
    public partial class AddSpaPersonne : MetroWindow
    {
        
        public AddSpaPersonne()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var db = new context())
            {
                SpaPersonne enqueteur = new SpaPersonne(Nom.Text, Prenom.Text, Mail.Text, Ville.Text, Rue.Text, Numero.Text, 1, Delegue.IsChecked, true);
                db.SpaPersonne.Add(enqueteur);
                db.SaveChanges();
            }
        }
    }
}
