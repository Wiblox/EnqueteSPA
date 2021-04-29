using EnquteSPA.modele;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("SpaPersonne")]
    class SpaPersonne
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSpaPersonne { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public int IdFonction { get; set; }

        public Fonction Fonction {get; set; }
        
        public bool? DelegueEnqueteur { get; set; }
        
        public bool Etat { get; set; }
    }
}