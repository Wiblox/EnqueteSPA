using EnquteSPA.modele;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("SpaPersonne")]
    class SpaPersonne
    {
        public SpaPersonne() { }

        public SpaPersonne(string nom, string prenom, string mail, string ville, string rue, string numero, int idFonction, bool? delegueEnqueteur, bool etat)
        {
            Nom = nom;
            Prenom = prenom;
            Mail = mail;
            Ville = ville;
            Rue = rue;
            Numero = numero;
            IdFonction = idFonction;
            DelegueEnqueteur = delegueEnqueteur;
            Etat = etat;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSpaPersonne { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Ville { get; set; }

        public string Rue { get; set; }

        public string Numero { get; set; }

        public int IdFonction { get; set; }
        
        public bool? DelegueEnqueteur { get; set; }
        
        public bool Etat { get; set; }

        public string GetLocalisation()
        {
            return $"{Numero} {Rue} {Ville} France";
        }
    }
}