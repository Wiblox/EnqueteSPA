﻿using System.Windows;
using System.Windows.Controls;
using EnquteSPA.bo;
using System.Linq;

namespace EnquteSPA
{
    /// <summary>
    /// Logique d'interaction pour Index.xaml
    /// </summary>
    public partial class Index : UserControl
    {
        public Index()
        {
            InitializeComponent();
            InitData();
        }

        public void InitData()
        {
            dso();
        }
        public void dso()
        {
            using var db = new Context();
            int nbEnquetesNonAssignees = db.Enquete.Where(o => o.Statut == (int)modele.StatutEnquete.NON_ASSIGNEE).Count();
            int nbEnquetesEnCours = db.Enquete.Where(o => o.Statut == (int)modele.StatutEnquete.EN_COURS).Count();
            int nbEnqueteurs = db.SpaPersonne.Where(o => o.IsEnqueteur == true).Count();

            NbEnquetesNonAssignees.Text = nbEnquetesNonAssignees.ToString();
            NbEnquetesEnCours.Text = nbEnquetesEnCours.ToString();
            NbEnqueteurs.Text = nbEnqueteurs.ToString();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dso();

        }
    }
}
