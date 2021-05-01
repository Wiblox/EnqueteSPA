using EnquteSPA.modele;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("Document")]
    class Document
    {
        public Document() { }
        public Document(string enquete, TypeDocument typeDocument, string pathDoc)
        {

            NoEnquete = enquete;
            this.typeDocument = typeDocument;
            PathDoc = pathDoc;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDocument { get; set; }
      
        public string  NoEnquete {get; set; }

        public TypeDocument typeDocument { get; set; }
        
        public string PathDoc { get; set; } 
    }
}
