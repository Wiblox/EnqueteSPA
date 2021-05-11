using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Windows;
using EnquteSPA.bo;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        bool close;
        public MainWindow()
        {
            // AddEnquete ae = new AddEnquete();
            // ae.ShowDialog();
            //AddDocument doc = new AddDocument("ds");
            //doc.ShowDialog();

            login frm = new login();
            frm.ShowDialog();
            if ((bool)frm.DialogResult)
            {
                Static.utilisateurCourant = frm.compte;
                InitializeComponent();
                LoginName.Text = Static.utilisateurCourant.Mail;
                if (Static.utilisateurCourant?.Admin == false)
                {
                    users.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                close = true;
                Close();
            }
        }

        protected override async void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!close)
                Deconnexion(null, null);
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("This is the title", "Some message");
        }

        private async void Deconnexion(object sender, RoutedEventArgs e)
        {
            var res = await this.ShowMessageAsync("Déconnexion", "Vous allez bientôt quitter l'application.", MessageDialogStyle.AffirmativeAndNegative);
            if (res == MessageDialogResult.Affirmative)
                Application.Current.Shutdown();
        }
    }
}
