using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnquteSPA
{
    [Table("Compte")]
    class Compte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompte { get; set; }
        
        [ForeignKey("SpaPersonne")]
        public int IdSpaPersonne { get; set; }

        public virtual SpaPersonne SpaPersonne {get; set; }


    }
}