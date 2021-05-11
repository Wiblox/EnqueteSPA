using EnquteSPA.bo;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for AddUsers.xaml
    /// </summary>
    public partial class AddUsers : MetroWindow
    {
        bool newcompte;
        Compte comptesd;
        public AddUsers()
        {
            newcompte = true;
            InitializeComponent();
        }
        public AddUsers(Compte compte)
        {
            comptesd = compte;
               newcompte = false;
            InitializeComponent();
            email.Text = compte.Mail;
            email.IsEnabled = false;
            button.Text = "Changer le mot de passe";
        }

        //On genere un mot de passa aleatoire
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        //On crée le compte
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool verification = true;
            string erreur = "";
            if (email.Text.Length == 0 || !IsValidEmail(email.Text))
            {
                verification = false;
                erreur += "Email incorect \n";
            }

            if (password.Password.Length == 0)
            {
                erreur += "Mot de passe incorect ";
                verification = false;
            }
            if (verification)
            {if(newcompte) { 
                new Compte(email.Text, password.Password, false);
                }
                else
                {
                    using var db = new Context();
                    var compte =db.Compte.Find(comptesd.IdCompte);
                    compte.MotDePasse = password.Password;
                    db.SaveChanges();

                }
                Close();
            }
            else
            {
                this.ShowMessageAsync("Erreur création de compte", erreur);
            }
        }
    }
}
