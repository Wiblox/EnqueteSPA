using System.Windows;
using MahApps.Metro.Controls;
using System;
using EnquteSPA.bo;
using EnquteSPA.modele;
using MahApps.Metro.Controls.Dialogs;

namespace EnquteSPA
{
    /// <summary>
    /// Logique d'interaction pour AddEnquete.xaml
    /// </summary>
    public partial class AddEnquete : MetroWindow
    {
        private readonly Erreur erreur;

        public AddEnquete()
        {
            InitializeComponent();
            XDateDepot.SelectedDate = DateTime.Today;
            GenTxtNumEnquete();
            erreur = new Erreur();
        }

        private void GenTxtNumEnquete()
        {
            string d = XDepartement.Text;
            if (d.Length == 0) d = "<DEP>";
            // TODO : Get la liste des enquêtes du jour pour connaître <ID>
            XNumEnquete.Text = $"{d}-{DateTime.Today.Year}-{DateTime.Today.Month}-<ID>";
        }
        private void ListeInspecteurs(object sender, EventArgs e)
        {
            // TODO : La listes des inspecteurs DISPONIBLES
        }

        private void TxtDepartementChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (erreur.TestNonVide(XDepartement.Text) && !erreur.TestDepartement(XDepartement.Text))
            {
                XDepartement.Text = XDepartement.Text.Remove(XDepartement.Text.Length - 1, 1);
                XDepartement.SelectionStart = XDepartement.Text.Length;
            }
            GenTxtNumEnquete();
        }

        private bool VerifChamps()
        {
            erreur.ResetMsgRetour();
            erreur.TestDepartement(XDepartement.Text, "Département");
            /*if(XInspecteur.SelectedItem == null)
                erreur.AddToMsgRetour("Pas d'inspecteur sélectionné.");*/
            erreur.TestNonVide(XMotif.Text, "Motif");
            // Plaignant
            erreur.TestNonVide(XPlaignantNom.Text, "Nom du plaignant");
            erreur.TestNonVide(XPlaignantPrenom.Text, "Prénom du plaignant");
            erreur.TestMail(XPlaignantMail.Text, "Mail du plaignant");
            erreur.TestNonVide(XPlaignantVille.Text, "Ville du plaignant");
            erreur.TestNonVide(XPlaignantNumero.Text, "Numéro du plaignant");
            erreur.TestNonVide(XPlaignantRue.Text, "Rue du plaignant");
            // Infracteur
            erreur.TestNonVide(XInfracteurNom.Text, "Nom de l'infracteur");
            erreur.TestNonVide(XInfracteurPrenom.Text, "Prénom de l'infracteur");
            erreur.TestMail(XInfracteurMail.Text, "Mail de l'infracteur");
            erreur.TestNonVide(XInfracteurVille.Text, "Ville de l'infracteur");
            erreur.TestNonVide(XInfracteurNumero.Text, "Numéro de l'infracteur");
            erreur.TestNonVide(XInfracteurRue.Text, "Rue de l'infracteur");

            if(!erreur.IsSafe())
                this.ShowMessageAsync("Erreur création de compte", erreur.GetMsgRetour());
            return erreur.IsSafe();
        }

        private void ClickBtnValider(object sender, RoutedEventArgs e)
        {
            if(VerifChamps())
            {
                using var db = new Context();
                Personne plaignant = new Personne(XPlaignantNom.Text, XPlaignantPrenom.Text, XPlaignantMail.Text, XPlaignantVille.Text, XPlaignantRue.Text, XPlaignantNumero.Text);
                db.Personne.Add(plaignant);
                Personne infracteur = new Personne(XInfracteurNom.Text, XInfracteurPrenom.Text, XInfracteurMail.Text, XInfracteurVille.Text, XInfracteurRue.Text, XInfracteurNumero.Text);
                db.Personne.Add(infracteur);
                db.SaveChanges();
                Enquete enquete = new Enquete(XNumEnquete.Text, Int32.Parse(XDepartement.Text), (DateTime)XDateDepot.SelectedDate, infracteur.IdPersonne, plaignant.IdPersonne, XMotif.Text, 1, 1);
                //Enquete enquete = new Enquete(XNumEnquete.Text, Int32.Parse(XDepartement.Text), (DateTime)XDateDepot.SelectedDate, infracteur.IdPersonne, plaignant.IdPersonne, XMotif.Text, Int32.Parse(XInspecteur.), 1);
                db.Enquete.Add(enquete);
                db.SaveChanges();
                Close();
            }
        }
    }
}
