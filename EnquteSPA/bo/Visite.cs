using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("Visite")]
    public class Visite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVisite { get; set; }
        
        [ForeignKey("Enquete")]
        public int IdEnquete { get; set; }

        public string Rapport { get; set; }

        public DateTime DateVisite { get; set; }

        public bool AvisPassage { get; set; }

        public int IdAccompagnant { get; set; }
    }
}