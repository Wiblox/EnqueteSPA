using EnquteSPA.bo;
using System;
using System.Collections.Generic;
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
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility =
                    row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
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
            var el = (sender as FrameworkElement);
            frm.Owner = Window.GetWindow(el);
            frm.ShowDialog();
        }
    }
}
