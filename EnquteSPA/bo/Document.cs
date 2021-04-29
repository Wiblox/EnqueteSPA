﻿using EnquteSPA.modele;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public TypeDocument typeDocument { get; set; }
        
        public string Libelle { get; set; } 
    }
}
