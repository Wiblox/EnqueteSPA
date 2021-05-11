using EnquteSPA.bo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for Accueil.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        public Users()
        {
            InitializeComponent();
        }

        private void data_Initialized()
        {
            using var db = new Context();
            data.ItemsSource = db.Compte.ToList();
        }
        void ShowHideDetails(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(" sdfd de f");
            Button fdv = (Button)sender;
            using var db = new Context();
            Debug.WriteLine(fdv.CommandParameter);
            
            Compte compte = db.Compte.Find(fdv.CommandParameter);
            AddUsers addEnquete = new AddUsers(compte);
            addEnquete.ShowDialog();
            db.SaveChanges();
            data.ItemsSource = db.Compte.ToList();
        }
        private void data_Initialized(object sender, EventArgs e)
        {
            data_Initialized();
        }

        private void data_Initialized(object sender, RoutedEventArgs e)
        {
            data_Initialized();
        }

        private void Button_Initialized(object sender, EventArgs e)
        {
            if(Static.utilisateurCourant?.Admin == false)
                 AddUser.IsEnabled = false;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUsers frm = new AddUsers();
            frm.ShowDialog();
            data.ItemsSource = db.Compte.ToList();

        }

        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(" sdfd de f");
            Button fdv = (Button)sender;
            using var db = new Context();
            Debug.WriteLine(fdv.CommandParameter);

            Compte compte = db.Compte.Find(fdv.CommandParameter);
            AddUsers addEnquete = new AddUsers(compte);
            addEnquete.ShowDialog();
            db.SaveChanges();
            data.ItemsSource = db.Compte.ToList();

        }
    }
}
