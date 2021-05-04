using System.Diagnostics;
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

        private void EditEnquete(object sender, RoutedEventArgs e)
        {
            Button fdv = (Button)sender;
            Debug.WriteLine(fdv.CommandParameter);
            using var db = new Context();
            Enquete editEnquete = db.Enquete.Where(v => v.NoEnquete == (string)fdv.CommandParameter).First();
            AddEnquete addEnquete = new AddEnquete(editEnquete);
            addEnquete.ShowDialog();
            db.SaveChanges();
            data.ItemsSource = db.Enquete.ToList();
        }
    }
}
