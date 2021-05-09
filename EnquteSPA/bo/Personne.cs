using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("Personne")]
    public class Personne
    {
        public Personne() { }
        public Personne(string denomination, string mail, string ville, string rue, string numero)
        {
            Denomination = denomination;
            Mail = mail;
            Ville = ville;
            Rue = rue;
            Numero = numero;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersonne { get; set; }
        
        public string Denomination { get; set; } 
        
        public string Mail { get; set; } 
        
        public string Ville { get; set; } 
        
        public string Rue { get; set; } 
        
        public string Numero { get; set; }
    }
}