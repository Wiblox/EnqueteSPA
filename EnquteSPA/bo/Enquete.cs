using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnquteSPA
{
    [Table("Enquete")]
    class Enquete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEnquete { get; set; }

        public string NoEnquete { get; set; }

        public int Departement { get; set; }

        public DateTime DateDepot { get; set; }
        
        [ForeignKey("Personne")]
        public int IdInfracteur { get; set; }

        public virtual Personne Infracteur {get; set; }
        
        [ForeignKey("Personne")]
        public int IdPlaignant { get; set; }

        public virtual Personne Plaignant {get; set; }

        public string Motif { get; set; }
        
        [ForeignKey("Enqueteur")]
        public int IdEnqueteur { get; set; }

        public virtual Enqueteur Enqueteur {get; set; }

        [ForeignKey("Statut")]
        public int IdStatut { get; set; }

        public virtual Statut Statut {get; set; }
    }
}