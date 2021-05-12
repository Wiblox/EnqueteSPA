using EnquteSPA.bo;
using EnquteSPA.modele;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : MetroWindow
    {
        public Compte compte;
        private Erreur erreur;

        public login()
        {
            InitializeComponent();
            Static.utilisateur = false;
            erreur = new Erreur();
            XUser.Focus();
        }

        private bool VerifChamps()
        {
            erreur.ResetMsgRetour();
            erreur.TestMail(XUser.Text, "Email");
            erreur.TestNonVide(XPassword.Password, "Mot de passe");

            if (!erreur.IsSafe())
                this.ShowMessageAsync("Champs connexions incomplets ou incorrects", erreur.GetMsgRetour());
            return erreur.IsSafe();
        }

        private void Connexion()
        {
            if (VerifChamps())
            {
                using var db = new Context();
                Compte compte = Compte.CheckCompte(XUser.Text, XPassword.Password);
                if (compte != null)
                {
                    Static.utilisateur = true;
                    this.compte = compte;
                    DialogResult = true;
                    Close();
                }
                else
                {
                    this.ShowMessageAsync("Impossible de se connecter", "Email ou mot de passe incorrect.");
                }
            }
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            Connexion();
        }

        private void XUser_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                Connexion();
        }

        private void XPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                Connexion();
        }
    }
}
