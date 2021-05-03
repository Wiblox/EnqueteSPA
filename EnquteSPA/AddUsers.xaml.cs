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
        public AddUsers()
        {
            InitializeComponent();
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
            if (password.Text.Length == 0)
            {
                erreur += "Mot de passe incorect ";
                verification = false;
            }
            if (verification)
            {
                new Compte(email.Text, password.Text, false);
                Close();
            }
            else
            {
                this.ShowMessageAsync("Erreur création de compte", erreur);
            }
        }
    }
}
