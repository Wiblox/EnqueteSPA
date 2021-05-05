using EnquteSPA.bo;
using EnquteSPA.modele;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
                XEtat.IsChecked = spaPersonne.Etat;

                XEtat.Visibility = Visibility.Visible;
            }
        }

        private bool VerifChamps()
        {
            erreur.ResetMsgRetour();
            erreur.TestNonVide(XNom.Text, "Nom");
            erreur.TestNonVide(XPrenom.Text, "Prénom");
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
                    SpaPersonne SpaPersonne = new SpaPersonne(XNom.Text, XPrenom.Text, XMail.Text, XVille.Text, XRue.Text, XNumero.Text, true, true);
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
    }
}
