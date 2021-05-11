using EnquteSPA.bo;
using EnquteSPA.modele;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Linq;
using System.Windows;

namespace EnquteSPA
{
    /// <summary>
    /// Logique d'interaction pour AddVisite.xaml
    /// </summary>
    public partial class AddVisite : MetroWindow
    {
        private Erreur erreur;
        private Visite visite;
        private Enquete enquete;

        public AddVisite(Enquete enquete, Visite visite = null)
        {
            InitializeComponent();
            XDate.SelectedDate = DateTime.Today;
            erreur = new Erreur();
            this.visite = visite;
            this.enquete = enquete;

            using var db = new Context();
            XAccompagnant.ItemsSource = db.SpaPersonne.Where(v => v.Etat == true && v.IdSpaPersonne != enquete.IdEnqueteur && v.IsEnqueteur==true).ToList();

            if (this.visite != null)
            {
                Title = "Modification d'une visite";
                XDate.SelectedDate = this.visite.DateVisite;
                XDate.IsEnabled = false;
                XAccompagnant.SelectedItem = db.SpaPersonne.Find(visite.IdAccompagnant);
                XAvisPassage.IsChecked = this.visite.AvisPassage;
                XRapport.Text = this.visite.Rapport;
            }
        }

        private void XAvisPassage_Checked(object sender, RoutedEventArgs e) 
        {
            XRapport_Container.Visibility = Visibility.Collapsed;
            XRapport.Text = "";
        }

        private void XAvisPassage_Unchecked(object sender, RoutedEventArgs e)
        {
            XRapport_Container.Visibility = Visibility.Visible;
        }

        private bool VerifChamps()
        {
            erreur.ResetMsgRetour();
            if (!(bool)XAvisPassage.IsChecked && XRapport.Text.Length == 0)
                erreur.AddToMsgRetour("Veuillez sélectionner avis de passage ou saisir un rapport.");

            if (!erreur.IsSafe())
                this.ShowMessageAsync("Erreur ajout visite", erreur.GetMsgRetour());
            return erreur.IsSafe();
        }

        private void ClickBtnValider(object sender, RoutedEventArgs e)
        {
            if (VerifChamps())
            {
                using var db = new Context();

                if(visite == null)
                {
                    db.Visite.Add(new Visite(enquete.IdEnquete, XRapport.Text, (DateTime)XDate.SelectedDate, (bool)XAvisPassage.IsChecked, (int?)XAccompagnant.SelectedValue));
                }
                else
                {   
                    if (XAccompagnant.SelectedValue != null)
                        visite.IdAccompagnant = (int)XAccompagnant.SelectedValue;
                }
                db.SaveChanges();
                Close();
            }
        }
    }
}
