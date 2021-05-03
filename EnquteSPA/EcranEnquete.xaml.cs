using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EnquteSPA.bo;

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
            using var db = new Context();
            data.ItemsSource = db.Enquete.ToList();
        }

        private void CreateEnquete(object sender, RoutedEventArgs e)
        {
            AddEnquete frm = new AddEnquete();
            frm.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button fdv = (Button)sender;
        }
    }
}
