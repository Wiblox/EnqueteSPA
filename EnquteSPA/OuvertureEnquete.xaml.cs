using EnquteSPA.bo;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for OuvertureEnquete.xaml
    /// </summary>
    public partial class OuvertureEnquete : MetroWindow
    {
        int idenquete;
        Enquete en;
        public OuvertureEnquete(int id)
        {
            idenquete = id;
            using var db = new Context();
            en = db.Enquete.Find(id);
            InitializeComponent();
            Title = "Enquête : " + en.NoEnquete;
        }

        private void ListeVisites(object sender, EventArgs e)
        {
            using var db = new Context();
            XGridVisite.ItemsSource = db.Visite.Where(v => v.IdEnquete == idenquete).ToList();
        }

        private void ListeDocuments(object sender, EventArgs e)
        {
            using var db = new Context();
            XGridDocument.ItemsSource = db.Document.Where(v => v.NoEnquete == idenquete).ToList();
        }

        private void AjouterDocument(object sender, RoutedEventArgs e)
        {
            AddDocument add = new AddDocument(idenquete);
            add.ShowDialog();
            using var db = new Context();
            XGridDocument.ItemsSource = db.Document.Where(v => v.NoEnquete == idenquete).ToList();
        }

        private void OuvrirDocument(object sender, RoutedEventArgs e)
        {
            //open document
            Button fdv = (Button)sender;
           
            string opeen = Directory.GetCurrentDirectory() + "\\" + ((string)fdv.CommandParameter).Replace('/', '\\');
            Debug.WriteLine(Directory.GetCurrentDirectory() + "\\" + ((string)fdv.CommandParameter).Replace('/','\\' ));

            Process photoViewer = new Process();
            photoViewer.StartInfo.FileName = @"explorer.exe";
            photoViewer.StartInfo.Arguments = @opeen;
            photoViewer.Start();
        }

        private void SupprimerDocument(object sender, RoutedEventArgs e)
        {
            Button fdv = (Button)sender;
            using var db = new Context();
            Document doc = db.Document.Find(fdv.CommandParameter);
            db.Document.Remove(doc);
            db.SaveChanges();
            XGridDocument.ItemsSource = db.Document.Where(v => v.NoEnquete == idenquete).ToList();
        }

        private void Button_AddVisite_Click(object sender, RoutedEventArgs e)
        {
            AddVisite av = new AddVisite(en);
            av.ShowDialog();
            using var db = new Context();
            XGridVisite.ItemsSource = db.Visite.ToList();
        }
    }

    public class DocumentPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            string file = ((string)value).Split('/').Last();
            string ext = '.'+file.Split('.').Last();
            if (file.Length - ext.Length < 45)
                return file;
            return $"{file[0..40]}[...]{ext}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class DocumentTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            switch((string)value)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".tiff":
                    return "Image";
                case ".pdf":
                    return "PDF";
                case ".doc":
                case ".docx":
                    return "Word";
                case ".mp3":
                case ".m4a":
                case ".flac":
                case ".ogg":
                    return "Audio";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    
    public class VisiteAccompagnantConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            using var db = new Context();
            SpaPersonne sp = db.SpaPersonne.Find(value);
            return $"{sp.Nom} {sp.Prenom}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    
    public class VisiteRapportConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || ((string)value).Length == 0) return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
