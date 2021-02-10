using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnquteSPA
{
    [Table("Enqueteur")]
    class Enqueteur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEnqueteur { get; set; }

        public bool Etat { get; set; }

        [ForeignKey("SpaPersonne")]
        public int IdSpaPersonne { get; set; }

        public virtual SpaPersonne SpaPersonne { get; set; }
    }
}