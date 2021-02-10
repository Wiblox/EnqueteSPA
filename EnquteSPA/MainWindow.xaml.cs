using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ObservableCollection<Enquete> person;

        public MainWindow()
        {
            login frm = new login();
            frm.ShowDialog();
            InitializeComponent();
        }
    


    protected override async void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            await this.ShowMessageAsync("Déconexion", "Vous allez bientôt quitter l'application.");
            e.Cancel = false;
            System.Windows.Application.Current.Shutdown();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
          
                await this.ShowMessageAsync("This is the title", "Some message");
            
        }

        private void data_Initialized(object sender, EventArgs e)
        {
            Enquete e1 = new Enquete("1", 57, DateTime.Now, 0, 0, "LE MOTIF", 0, 0);
            Enquete e2 = new Enquete("2", 57, DateTime.Now, 0, 0, "LE MOTIF", 0, 0);
            Enquete e3 = new Enquete("3", 57, DateTime.Now, 0, 0, "LE MOTIF", 0, 0);
            person = new ObservableCollection<Enquete>()
         {
                 new Enquete("1", 57, DateTime.Now, 0, 0, "LE MOTIF", 0, 0),
                 new Enquete("2", 57, DateTime.Now, 0, 0, "LE MOTIF", 0, 0),
                 new Enquete("3", 57, DateTime.Now, 0, 0, "LE MOTIF", 0, 0)
        };
            data.ItemsSource = person;
      

        }
    }
}
