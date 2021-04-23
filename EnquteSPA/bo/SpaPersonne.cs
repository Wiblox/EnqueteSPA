using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnquteSPA
{
    [Table("SpaPersonne")]
    class SpaPersonne
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSpaPersonne { get; set; }

        [ForeignKey("Personne")]
        public int IdPersonne { get; set; }

        public virtual Personne Personne {get; set; }

        [ForeignKey("Fonction")]
        public int IdFonction { get; set; }

        public virtual Fonction Fonction {get; set; }
        
        public bool DelegueEnqueteur { get; set; } 
        
        public bool Etat { get; set; } 

    
    }
}