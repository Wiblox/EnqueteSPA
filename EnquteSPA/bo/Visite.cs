using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("Visite")]
    public class Visite
    {
        public Visite() { }
        public Visite(int idEnquete, string rapport, DateTime dateVisite, bool avisPassage, int? idAccompagnant)
        {
            IdEnquete = idEnquete;
            Rapport = rapport;
            DateVisite = dateVisite;
            AvisPassage = avisPassage;
            IdAccompagnant = idAccompagnant;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVisite { get; set; }

        public int IdEnquete { get; set; }

        public string Rapport { get; set; }

        public DateTime DateVisite { get; set; }

        public bool AvisPassage { get; set; }

        public int? IdAccompagnant { get; set; }
    }
}