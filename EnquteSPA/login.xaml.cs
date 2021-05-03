using EnquteSPA.bo;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : MetroWindow
    {
         public Compte usser;

        public login( )
        {
            Static.utilisateur = false;

            MouseDown += Window_MouseDown;

            InitializeComponent();

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Context())
            {
                Compte loogin = Compte.CheckCompte(user.Text,password.Password);
                if (loogin != null)
                {
                    Static.utilisateur = true;
                    usser = loogin;
                    this.DialogResult = true;
                    Close();
                }
            }


        }


    }
}
