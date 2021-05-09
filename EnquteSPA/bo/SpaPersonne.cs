using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquteSPA
{
    [Table("SpaPersonne")]
    public class SpaPersonne
    {
        public SpaPersonne() { }

        public SpaPersonne(string nom, string prenom, string mail, string ville, string rue, string numero, bool isSalarie, bool isEnqueteur, bool etat)
        {
            Nom = nom;
            Prenom = prenom;
            Mail = mail;
            Ville = ville;
            Rue = rue;
            Numero = numero;
            IsSalarie = isSalarie;
            IsEnqueteur = isEnqueteur;
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
        
        public bool IsSalarie { get; set; }

        public bool IsEnqueteur { get; set; }
        
        public bool Etat { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SpaPersonne personne &&
                   IdSpaPersonne == personne.IdSpaPersonne &&
                   Nom == personne.Nom &&
                   Prenom == personne.Prenom &&
                   Mail == personne.Mail &&
                   Ville == personne.Ville &&
                   Rue == personne.Rue &&
                   Numero == personne.Numero &&
                   IsSalarie == personne.IsSalarie &&
                   IsEnqueteur == personne.IsEnqueteur &&
                   Etat == personne.Etat;
        }

        public string GetLocalisation()
        {
            return $"{Numero} {Rue} {Ville} France";
        }
    }
}