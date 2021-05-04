using System.Windows;
using MahApps.Metro.Controls;
using System;
using EnquteSPA.bo;
using EnquteSPA.modele;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Diagnostics;

namespace EnquteSPA
{
    /// <summary>
    /// Logique d'interaction pour AddEnquete.xaml
    /// </summary>
    public partial class AddEnquete : MetroWindow
    {
        private readonly Erreur erreur;
        private Enquete enquete;
        private Personne plaignant, infracteur;
        private string ID;

        public AddEnquete(Enquete enquete = null)
        {
            InitializeComponent();
            XDateDepot.SelectedDate = DateTime.Today;
            GenTxtNumEnquete();
            erreur = new Erreur();
            this.enquete = enquete;

            if (this.enquete != null)
            {
                Title = "Modification d'une enquête";

                XNumEnquete.Text = this.enquete.NoEnquete;
                XDateDepot.SelectedDate = this.enquete.DateDepot;
                XDateDepot.Focusable = false;
                XDepartement.Text = this.enquete.Departement;
                XDepartement.IsReadOnly = true;
                XMotif.Text = this.enquete.Motif;

                using var db = new Context();

                XEnqueteur.SelectedItem = db.SpaPersonne.Find(enquete.IdEnqueteur);

                plaignant = db.Personne.Find(this.enquete.IdPlaignant);
                infracteur = db.Personne.Find(this.enquete.IdInfracteur);

                XPlaignantNom.Text = plaignant.Nom;
                XPlaignantNom.IsReadOnly = true;
                XPlaignantPrenom.Text = plaignant.Prenom;
                XPlaignantPrenom.IsReadOnly = true;
                XPlaignantMail.Text = plaignant.Mail;
                XPlaignantMail.IsReadOnly = true;
                XPlaignantVille.Text = plaignant.Ville;
                XPlaignantVille.IsReadOnly = true;
                XPlaignantNumero.Text = plaignant.Numero;
                XPlaignantNumero.IsReadOnly = true;
                XPlaignantRue.Text = plaignant.Rue;
                XPlaignantRue.IsReadOnly = true;

                XInfracteurNom.Text = infracteur.Nom;
                XInfracteurNom.IsReadOnly = true;
                XInfracteurPrenom.Text = infracteur.Prenom;
                XInfracteurPrenom.IsReadOnly = true;
                XInfracteurMail.Text = infracteur.Mail;
                XInfracteurMail.IsReadOnly = true;
                XInfracteurVille.Text = infracteur.Ville;
                XInfracteurVille.IsReadOnly = true;
                XInfracteurNumero.Text = infracteur.Numero;
                XInfracteurNumero.IsReadOnly = true;
                XInfracteurRue.Text = infracteur.Rue;
                XInfracteurRue.IsReadOnly = true;
            }
        }

        private void GenTxtNumEnquete()
        {
            if(enquete == null)
            {
                string id = "<ID>";
                string depTxt = XDepartement.Text;
                if (depTxt.Length == 0) depTxt = "<DEP>";
                string numEnqueteWithoutID = $"{depTxt}-{DateTime.Today.Year}-{$"{DateTime.Today.Month}".PadLeft(2, '0')}";
                if (XDepartement.Text.Length > 0)
                {
                    using var db = new Context();
                    id = $"{db.Enquete.Where(v => v.NoEnquete.StartsWith(numEnqueteWithoutID)).Count() + 1}".PadLeft(4, '0');
                }
                XNumEnquete.Text = $"{numEnqueteWithoutID}-{id}";
            }
        }

        private void ListeEnqueteurs(object sender, EventArgs e)
        {
            using var db = new Context();
            XEnqueteur.ItemsSource = db.SpaPersonne.Where(v => v.Etat == true).ToList();
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

            if (!erreur.IsSafe())
                this.ShowMessageAsync("Erreur création de compte", erreur.GetMsgRetour());
            return erreur.IsSafe();
        }

        private void ClickBtnValider(object sender, RoutedEventArgs e)
        {
            if (VerifChamps())
            {
                using var db = new Context();

                int statut = (XEnqueteur.SelectedItem == null) ? (int)StatutEnquete.NON_ASSIGNEE : (int)StatutEnquete.EN_COURS;
                if (enquete == null)
                {
                    Personne plaignant = new Personne(XPlaignantNom.Text, XPlaignantPrenom.Text, XPlaignantMail.Text, XPlaignantVille.Text, XPlaignantRue.Text, XPlaignantNumero.Text);
                    db.Personne.Add(plaignant);
                    Personne infracteur = new Personne(XInfracteurNom.Text, XInfracteurPrenom.Text, XInfracteurMail.Text, XInfracteurVille.Text, XInfracteurRue.Text, XInfracteurNumero.Text);
                    db.Personne.Add(infracteur);
                    db.SaveChanges();
                    Enquete enquete = new Enquete(XNumEnquete.Text, XDepartement.Text, (DateTime)XDateDepot.SelectedDate, infracteur.IdPersonne, plaignant.IdPersonne, XMotif.Text, (int?)XEnqueteur.SelectedValue, statut);
                    db.Enquete.Add(enquete);
                }
                else
                {
                    enquete.IdEnqueteur = (int)XEnqueteur.SelectedValue;
                    enquete.Statut = statut;
                    enquete.Motif = XMotif.Text;
                }
                db.SaveChanges();
                Close();
            }
        }
    }
}
