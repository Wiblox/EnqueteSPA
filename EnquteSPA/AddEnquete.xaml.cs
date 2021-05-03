using System.Windows;
using MahApps.Metro.Controls;
using System;
using EnquteSPA.bo;

namespace EnquteSPA
{
    /// <summary>
    /// Logique d'interaction pour AddEnquete.xaml
    /// </summary>
    public partial class AddEnquete : MetroWindow
    {
        public AddEnquete()
        {
            InitializeComponent();
            XDateDepot.SelectedDate = DateTime.Today;
            GenTxtNumEnquete();
        }

        private void GenTxtNumEnquete()
        {
            string d = XDepartement.Text;
            if (d.Length == 0) d = "<DEP>";
            XNumEnquete.Text = $"{d}-{DateTime.Today.Year}-{DateTime.Today.Month}-<ID>";
        }
        private void ListeInspecteurs(object sender, System.EventArgs e)
        {
            // TODO : La listes des inspecteurs DISPONIBLES
        }

        private void TxtDepartementChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            GenTxtNumEnquete();
            // Regex des départements : https://www.regions-et-departements.fr/departements-francais
            // Regex regex = new Regex("^([0-9]|[0-8][0-9])|9[0-5]|2[A|B]|97[1-4|6]$");
            // if (!regex.IsMatch(XDepartement.Text) && XDepartement.Text.Length > 0)
            // {
            //     //XDepartement.Text = XDepartement.Text.Remove(XDepartement.Text.Length - 1, 1);
            // }
            // System.Diagnostics.Debug.WriteLine($"e: {XDepartement.Text} {XDepartement.Text.Length} {regex.Match(XDepartement.Text)}");
        }

        private void ClickBtnValider(object sender, RoutedEventArgs e)
        {
            using var db = new context();
            Personne plaignant = new Personne(XPlaignantNom.Text, XPlaignantPrenom.Text, XPlaignantMail.Text, XPlaignantVille.Text, XPlaignantRue.Text, XPlaignantNumero.Text);
            db.Personne.Add(plaignant);
            Personne infracteur = new Personne(XInfracteurNom.Text, XInfracteurPrenom.Text, XInfracteurMail.Text, XInfracteurVille.Text, XInfracteurRue.Text, XInfracteurNumero.Text);
            db.Personne.Add(infracteur);
            db.SaveChanges();
            Enquete enquete = new Enquete(XNumEnquete.Text, Int32.Parse(XDepartement.Text), (DateTime)XDateDepot.SelectedDate, infracteur.IdPersonne, plaignant.IdPersonne, XMotif.Text, 1, 1);
            db.Enquete.Add(enquete);
            db.SaveChanges();
            Close();
        }
    }
}
