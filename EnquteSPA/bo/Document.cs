using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnquteSPA
{
    [Table("Document")]
    class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDocument { get; set; }
        
        [ForeignKey("Enquete")]
        public int IdEnquete { get; set; }

        public virtual Enquete Enquete {get; set; }

        [ForeignKey("TypeDocument")]
        public int IdTypeDocument { get; set; }

        public virtual TypeDocument TypeDocument {get; set; }
        
        public string Libelle { get; set; } 
    }
}
