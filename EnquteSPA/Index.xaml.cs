using System.Windows;
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
            int nbEnquetesNonAssignees;
            int nbEnquetesEnCours;
            int nbEnqueteurs;

            using (var db = new context())
            {

                nbEnquetesNonAssignees = db.Enquete.Where(o => o.Statut == (int)modele.StatutEnquete.NON_ASSIGNEE).Count();
                nbEnquetesEnCours = db.Enquete.Where(o => o.Statut == (int)modele.StatutEnquete.EN_COURS).Count();
                nbEnqueteurs = db.SpaPersonne.Where(o => o.DelegueEnqueteur == true).Count();
            }

            NbEnquetesNonAssignees.Text = nbEnquetesNonAssignees.ToString();
            NbEnquetesEnCours.Text = nbEnquetesEnCours.ToString();
            NbEnqueteurs.Text = nbEnqueteurs.ToString();
        }
    }
}
