using EnquteSPA.bo;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
            XMotif.Text = en.Motif;
            XObjet.Text = en.Objet;
            XRace.Text = en.Race;
            XDateDepot.SelectedDate = en.DateDepot;
            if (en.Statut == 3) { toggle.IsOn = true;


                disable(false);


            }
            else { toggle.IsOn = false; }
            if (en.Statut == 1) { toggle.IsEnabled = false; }
            Title = "Enquête : " + en.NoEnquete;
        }
        public void disable(bool statut)
        {
            if (Static.utilisateurCourant?.Admin == false)
            {

                toggle.IsEnabled = statut;
            }
            XObjet.IsEnabled = statut;
            XRace.IsEnabled = statut;
            Button_AddVisite.IsEnabled = statut;
            buttonds.IsEnabled = statut;
            fsdfsdf.IsEnabled = statut;
            XDateDepot.IsEnabled = statut;

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
            var el = (sender as FrameworkElement);
            add.Owner = Window.GetWindow(el);
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
            var el = (sender as FrameworkElement);
            av.Owner = Window.GetWindow(el);
            av.ShowDialog();
            using var db = new Context();
            XGridVisite.ItemsSource = db.Visite.Where(c => c.IdEnquete== idenquete).ToList();
        }

        private void Button_Visite_Rapport_Click(object sender, RoutedEventArgs e)
        {
            Button fdv = (Button)sender;
            using var db = new Context();
            Visite visite = db.Visite.Find(fdv.CommandParameter);
            this.ShowMessageAsync($"Enquête n°{en.NoEnquete} - Visite du {visite.DateVisite:dd/MM/yyyy}", visite.Rapport);
        }

        private void SendMail(object sender, RoutedEventArgs e)
        {
            String destinataire = "test@gmail.com";
            String sujet = "Affectation de l'enquête 57/21/05/001";
            String corps = "Bonjour Michel, %0A%0AL'enquête 57/21/05/001 saisie le 09/05/2020 vous a été affectée. %0ACelle-ci a pour objet \"Refuge\" et concerne les espèces suivantes : Cochons, Chiens.%0ASon motif : \"Maltraitance de cochons\".%0A%0APlaignant%0AL214%0AMail : l214@gmail.com%0AAdresse : 4 rue du soleil 67204 Achenheim%0A%0AInfracteur%0ALes Mousquetaires%0AMail : lesmousquetaires@mail.com%0AAdresse : 18 rue Taison 57000 Metz";
            System.Diagnostics.Process.Start(new ProcessStartInfo("mailto:" + destinataire + "?subject=" + sujet + "&body=" + corps + "") { UseShellExecute = true });

        }

        private void StatutEnquete(object sender, RoutedEventArgs e)
        {
            using var db = new Context();
           var te= db.Enquete.Find(en.IdEnquete);
            ToggleSwitch sdfsender = (ToggleSwitch)sender;
            if (sdfsender.IsOn)
            {
                te.Statut = 3;
                disable(false);
            }
            else
            {
                disable(true);
                if (te.IdEnqueteur == null)
                {
                    te.Statut = 1;

                }
                else
                {
                    te.Statut = 2;
                }
            }
            Debug.WriteLine("Staut = " + te.Statut);
            db.SaveChanges();
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
            switch(((string)value).ToLower())
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
            if (value == null) return "--";
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
            return (value == null || ((string)value).Length == 0) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
