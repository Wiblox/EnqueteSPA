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
        SpaPersonne modifier;


        public AddSpaPersonne()
        {
            modifier = null;
            InitializeComponent();
        }

        public AddSpaPersonne(SpaPersonne modif)
        {
            modifier = modif;
            InitializeComponent();

            Nom.Text = modif.Nom;
            Prenom.Text = modif.Prenom;
            Mail.Text = modif.Mail;
            Ville.Text = modif.Ville;
            Rue.Text = modif.Rue;
            Numero.Text = modif.Numero;
            Delegue.IsChecked = modif.DelegueEnqueteur;
            NomB.Text = "Modifer Enquêteur";

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var db = new Context())
            {
                if (modifier == null)
                {
                    SpaPersonne enqueteur = new SpaPersonne(Nom.Text, Prenom.Text, Mail.Text, Ville.Text, Rue.Text, Numero.Text, 1, Delegue.IsChecked, true);
                    db.SpaPersonne.Add(enqueteur);
                    db.SaveChanges();

                }
                else
                {
                    modifier.Nom = Nom.Text;
                    modifier.Prenom = Prenom.Text;
                    modifier.Mail = Mail.Text;
                    modifier.Ville = Ville.Text;
                    modifier.Rue = Rue.Text;
                    modifier.Numero = Numero.Text;
                    modifier.DelegueEnqueteur = Delegue.IsChecked;
                    db.SaveChanges();

                }

                this.Close();
            }
        }
    }
}
