using EnquteSPA.bo;
using EnquteSPA.modele;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for AddSpaPersonne.xaml
    /// </summary>
    public partial class AddSpaPersonne : MetroWindow
    {
        private SpaPersonne spaPersonne;
        private Erreur erreur;

        public AddSpaPersonne(SpaPersonne spaPersonne = null)
        {
            InitializeComponent();
            erreur = new Erreur();
            this.spaPersonne = spaPersonne;

            if (spaPersonne != null)
            {
                Title = "Modification d'un enquêteur";

                XNom.Text = spaPersonne.Nom;
                XPrenom.Text = spaPersonne.Prenom;
                XMail.Text = spaPersonne.Mail;
                XVille.Text = spaPersonne.Ville;
                XRue.Text = spaPersonne.Rue;
                XNumero.Text = spaPersonne.Numero;
                XFonction.SelectedIndex = spaPersonne.IsSalarie ? 0 : 1;
                XEnqueteur.IsChecked = spaPersonne.IsEnqueteur;
                XEtat.IsChecked = spaPersonne.Etat;

                XEtat.Visibility = Visibility.Visible;
            }
        }

        private bool VerifChamps()
        {
            erreur.ResetMsgRetour();
            if (!erreur.TestNonVide(XNom.Text + XPrenom.Text))
                erreur.AddToMsgRetour("Veuillez saisir un nom et/ou un prénom.");
            erreur.TestMail(XMail.Text, "Mail");
            erreur.TestNonVide(XVille.Text, "Ville");
            erreur.TestNonVide(XNumero.Text, "Numéro");
            erreur.TestNonVide(XRue.Text, "Rue");
            if (XFonction.SelectedItem == null)
                erreur.AddToMsgRetour("Pas de fonction sélectionnée.");

            if (!erreur.IsSafe())
                this.ShowMessageAsync("Erreur ajout enquêteur", erreur.GetMsgRetour());
            return erreur.IsSafe();
        }

        private void Button_Valider(object sender, RoutedEventArgs e)
        {
            if (VerifChamps())
            {
                using var db = new Context();
                if (spaPersonne == null)
                {
                    SpaPersonne SpaPersonne = new SpaPersonne(XNom.Text, XPrenom.Text, XMail.Text, XVille.Text, XRue.Text, XNumero.Text, XFonction.SelectedIndex == 0 ? true : false, (bool)XEnqueteur.IsChecked, true);
                    db.SpaPersonne.Add(SpaPersonne);
                }
                else
                {
                    spaPersonne.Nom = XNom.Text;
                    spaPersonne.Prenom = XPrenom.Text;
                    spaPersonne.Mail = XMail.Text;
                    spaPersonne.Ville = XVille.Text;
                    spaPersonne.Rue = XRue.Text;
                    spaPersonne.Numero = XNumero.Text;
                    spaPersonne.IsSalarie = bool.Parse(((ComboBoxItem)XFonction.SelectedItem).Tag.ToString());
                    spaPersonne.IsEnqueteur = (bool)XEnqueteur.IsChecked;
                    spaPersonne.Etat = (bool)XEtat.IsChecked;
                }
                db.SaveChanges();
                Close();
            }
        }

        private void XNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            XNom.Text = XNom.Text.ToUpper();
            XNom.SelectionStart = XNom.Text.Length;
        }

        private void XPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            XPrenom.Text = Static.FirstLetterToUpper(XPrenom.Text);
            XPrenom.SelectionStart = XPrenom.Text.Length;
        }

        private async void XEtat_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void XEnqueteur_Unchecked(object sender, RoutedEventArgs e)
        {
            if (spaPersonne != null)
            {
                using var db = new Context();
                int nb = db.Enquete.Where(v => v.IdEnqueteur == spaPersonne.IdSpaPersonne && v.Statut < 3).Count();
                if (nb > 0)
                {
                    this.ShowMessageAsync("Attention !", $"Cet enquêteur est associé à {nb} enquête{(nb < 2 ? "" : "s")} !\nVous devez {(nb < 2 ? "la" : "les")} réattribuer avant de\ndésélectionner \"Enquêteur\".");
                    XEnqueteur.IsChecked = true;
                }
            }
        }

        private void XEnqueteur_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void XEtat_Click(object sender, RoutedEventArgs e)
        {
            if (spaPersonne != null && XEtat.IsChecked == false && XEnqueteur.IsChecked == true)
            {
                using var db = new Context();
                int nb = db.Enquete.Where(v => v.IdEnqueteur == spaPersonne.IdSpaPersonne && v.Statut < 3).Count();
                if (nb > 0)
                {
                    var res = await this.ShowMessageAsync("Attention !", $"Cet enquêteur est associé à {nb} enquête{(nb < 2 ? "" : "s")} !", MessageDialogStyle.AffirmativeAndNegative);
                    if (res == MessageDialogResult.Negative)
                        XEtat.IsChecked = true;
                }
            }
        }
    }
}
