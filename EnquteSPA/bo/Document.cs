using EnquteSPA.modele;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("Document")]
    public class Document
    {
        public Document() { }
        public Document(int enquete, string pathDoc)
        {
            NoEnquete = enquete;
            PathDoc = pathDoc;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDocument { get; set; }
      
        public int  NoEnquete {get; set; }
        
        public string PathDoc { get; set; } 
    }
}
