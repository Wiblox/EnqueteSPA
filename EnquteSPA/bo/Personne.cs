using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnquteSPA
{
    [Table("Personne")]
    class Personne
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersonne { get; set; }
        
        public string Nom { get; set; } 
        
        public string Prenom { get; set; } 
        
        public string Mail { get; set; } 
        
        public string Ville { get; set; } 
        
        public string Rue { get; set; } 
        
        public string Numero { get; set; }
    }
}