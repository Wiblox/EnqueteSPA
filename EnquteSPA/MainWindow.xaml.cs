﻿using MahApps.Metro.Controls;
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
using System.Data.Entity;
using EnquteSPA.bo;
using EnquteSPA.Controller;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

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
            using (var db = new context())
            {
                var personne = new Personne { Nom = "AAA", Prenom = "Quentin", Mail = "Pas gentil" };
                db.Personne.Add(personne);
                db.SaveChanges();

                new User("beaupuy", "quentin", "bquentin@mail.com", true, "mdp123");
                new User("avvvvv", "avvvvv", "avvvvv@mail.com", true, "avvvvv");
                new User("bvvvvv", "bvvvvv", "bvvvvv@mail.com", true, "bvvvvv");
                new User("cvvvvv", "cvvvvv", "cvvvvv@mail.com", true, "cvvvvv");
                new User("dvvvvv", "dvvvvv", "dvvvvv@mail.com", true, "dvvvvv");
                new User("evvvvv", "evvvvv", "evvvvv@mail.com", true, "evvvvv");


                data.ItemsSource = db.Personne.ToList();
            }
      

        }
    }
}
