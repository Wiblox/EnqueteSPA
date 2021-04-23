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
        public login()
        {

            LoginUser test = new LoginUser("quentisn", "slave");


            
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
            LoginUser test = new LoginUser(this.user.Text, this.password.Text);
            if (test.CheckUser())
            {
                DialogResult = true;
                this.Close();
            }

        }


    }
}
