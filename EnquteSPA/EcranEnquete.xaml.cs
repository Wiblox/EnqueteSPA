using System.Windows;
using System.Windows.Controls;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for Accueil.xaml
    /// </summary>
    public partial class EcranEnquete : UserControl
    {
        public EcranEnquete()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            
        }

        private void CreateEnquete(object sender, RoutedEventArgs e)
        {
            AddUsers frm = new AddUsers();
            frm.ShowDialog();
        }
    }
}
