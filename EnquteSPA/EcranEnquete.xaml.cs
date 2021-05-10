using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EnquteSPA.bo;
using EnquteSPA.modele;

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
            using var db = new Context();
            db.SaveChanges();
            data.ItemsSource = db.Enquete.ToList();
        }

        private void EditEnquete(object sender, RoutedEventArgs e)
        {
            Button fdv = (Button)sender;
            using var db = new Context();
            Enquete editEnquete = db.Enquete.Where(v => v.NoEnquete == (string)fdv.CommandParameter).First();
            AddEnquete addEnquete = new AddEnquete(editEnquete);
            addEnquete.ShowDialog();
            db.SaveChanges();
            data.ItemsSource = db.Enquete.ToList();
        }

        private void OuvrirEnquete(object sender, RoutedEventArgs e)
        {
            Button fdv = (Button)sender;
            var el = (sender as FrameworkElement);
            OuvertureEnquete ds = new OuvertureEnquete((int)fdv.CommandParameter);
            ds.Owner = Window.GetWindow(el);
            ds.ShowDialog();
            using var db = new Context();
            data.ItemsSource = db.Enquete.ToList();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            using var db = new Context();
            data.ItemsSource = db.Enquete.ToList();

        }
    }

    public class StatutConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch((int)value)
            {
                case (int)StatutEnquete.EN_COURS:
                    return "En cours";
                case (int)StatutEnquete.NON_ASSIGNEE:
                    return "Non assignée";
                case (int)StatutEnquete.RENDUE:
                    return "Rendue";
            }
            return "ERREUR";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class PersonneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "--";
            using var db = new Context();
            Personne p = db.Personne.Find((int)value);
            return p.Denomination;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class SpaPersonneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "--";
            using var db = new Context();
            SpaPersonne p = db.SpaPersonne.Find((int)value);
            return $"{p.Nom} {p.Prenom}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
