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
    public partial class Accueil : UserControl
    {
        public Accueil()
        {
            InitializeComponent();
        }

        private void data_Initialized(object sender, EventArgs e)
        {
            using (var db = new context())
            {
                var personne = new Personne { Nom = "AAA", Prenom = "Quentin", Mail = "Pas gentil" };
                db.Personne.Add(personne);
                db.SaveChanges();

                data.ItemsSource = db.Personne.ToList();
            }


        }
    }
}
