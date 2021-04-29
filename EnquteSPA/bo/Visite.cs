using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("Visite")]
    class Visite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVisite { get; set; }
        
        [ForeignKey("Enquete")]
        public int IdEnquete { get; set; }

        public virtual Enquete Enquete {get; set; }

        public string Rapport { get; set; }

        public DateTime DateVisite { get; set; }
    }
}