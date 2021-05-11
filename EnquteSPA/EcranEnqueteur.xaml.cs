using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EnquteSPA.bo;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for EcranEnqueteur.xaml
    /// </summary>
    public partial class EcranEnqueteur : UserControl
    {
        public EcranEnqueteur()
        {
            InitializeComponent();
        }

        private void ListeEnqueteurs(object sender, EventArgs e)
        {
            using var db = new Context();
            data.ItemsSource = db.SpaPersonne.ToList();
        }

        private void Button_AddEnqueteur(object sender, RoutedEventArgs e)
        {
            AddSpaPersonne fenetre = new AddSpaPersonne();
            var el = (sender as FrameworkElement);
            fenetre.Owner = Window.GetWindow(el);
            fenetre.ShowDialog();
            using var db = new Context();
            data.ItemsSource = db.SpaPersonne.ToList();
        }

        private void Button_Localisation(object sender, RoutedEventArgs e)
        {
            LocateEnqueteur locate = new LocateEnqueteur();
            var el = (sender as FrameworkElement);
            locate.Owner = Window.GetWindow(el);
            locate.ShowDialog();
        }

        private void Button_EditEnqueteur(object sender, RoutedEventArgs e)
        {
            Button fdv = (Button)sender;
            using var db = new Context();
            var modifSPA = db.SpaPersonne.Find(fdv.CommandParameter);
            AddSpaPersonne newx = new AddSpaPersonne(modifSPA);
            var el = (sender as FrameworkElement);
            newx.Owner = Window.GetWindow(el);
            newx.ShowDialog();
            db.SaveChanges();
            data.ItemsSource = db.SpaPersonne.ToList();
        }

        private void data_Loaded(object sender, RoutedEventArgs e)
        {
            using var db = new Context();
            data.ItemsSource = db.SpaPersonne.ToList();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            using var db = new Context();
            data.ItemsSource = db.SpaPersonne.ToList();
        }
    }

    public class FonctionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? "Salarié" : "Bénévole";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
