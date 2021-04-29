using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
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
        Compte user;
        public MainWindow()
        {
            init();
            login frm = new login();
            frm.ShowDialog();
            if (frm.DialogResult != true) { close = true; Close(); }
            InitializeComponent();
            LoginName.Text = frm.usser.Mail;

        }

        private void init()
        {
            using (var db = new context())
            {
                //On initialise un admin
                if (db.Compte.ToList().Count == 0)
                {
                    Compte admin = new Compte("admin@gmail.com", "azerty123", true);
                }

            }
        }

    protected override async void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (close == false) { 
            e.Cancel = true;
            await this.ShowMessageAsync("Déconexion", "Vous allez bientôt quitter l'application.");
            e.Cancel = false;
            System.Windows.Application.Current.Shutdown();
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("This is the title", "Some message");
        }
    }
}
