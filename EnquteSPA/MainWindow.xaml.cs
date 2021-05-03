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
        public MainWindow()
        {
            //AddEnquete ae = new AddEnquete();
            //ae.ShowDialog();
            //AddDocument doc = new AddDocument("ds");
            //doc.ShowDialog();
            LocateEnqueteur df = new LocateEnqueteur();
            df.ShowDialog();




            init();
            login frm = new login();
            frm.ShowDialog();
            if ((bool)frm.DialogResult)
            {
                Static.utilisateurCourant = frm.usser;
                InitializeComponent();
                LoginName.Text = Static.utilisateurCourant.Mail;
            }
            else
            {
                close = true;
                Close();
            }
        }

        private void init()
        {
            using (var db = new Context()) { 

                //On initialise un admin
                if (db.Compte.ToList().Count == 0)
            {
                new Compte("admin@gmail.com", "azerty123", true);
            }
        }
    }

        protected override async void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!close)
            {
                e.Cancel = true;
                await this.ShowMessageAsync("Déconnexion", "Vous allez bientôt quitter l'application.");
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
