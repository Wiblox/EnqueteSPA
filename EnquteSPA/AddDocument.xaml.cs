using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using EnquteSPA.bo;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddDocument : MetroWindow
    {

        string numeroEnqute;
        public AddDocument(string numero)
        {
            numeroEnqute = numero;
            InitializeComponent();
        }

        private void dragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Move;

        }

        private void dragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Info.Text = System.IO.Path.GetFileName(files[0]);
            this.confirmAsync(files[0]);


        }

        private void confirmAsync(string v)
        {
            string titre= "Ajout document Enquete n°"+ numeroEnqute;
            string message="Voulez vous ajouter le document "+ v + " à l'enquete n°"+ numeroEnqute;
            var truc = this.ShowModalMessageExternal(titre, message, MessageDialogStyle.AffirmativeAndNegative);

            if (truc == MessageDialogResult.Affirmative)
            {
                System.IO.Directory.CreateDirectory("numeroEnquete/"+ numeroEnqute);
                System.IO.File.Copy(v, "numeroEnquete/" + numeroEnqute+"/"+ System.IO.Path.GetFileName(v), true);
                Document doc = new Document(numeroEnqute, modele.TypeDocument.Pdf, "numeroEnquete/" + numeroEnqute + "/" + System.IO.Path.GetFileName(v));
                using (var db = new context())
                {
                    db.Document.Add(doc);
                    db.SaveChanges();
                }

                    Close();
            }
        }
    }
}



