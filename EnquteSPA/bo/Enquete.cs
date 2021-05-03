using EnquteSPA.bo;
using EnquteSPA.modele;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EnquteSPA
{
    [Table("Enquete")]
    class Enquete
    {
        public Enquete() { }

        public Enquete(string noEnquete, int departement, DateTime dateDepot, int? idInfracteur, int? idPlaignant, string motif, int? idEnqueteur, int statut)
        {
            NoEnquete = noEnquete;
            Departement = departement;
            DateDepot = dateDepot;
            IdInfracteur = idInfracteur;
            IdPlaignant = idPlaignant;
            Motif = motif;
            IdEnqueteur = idEnqueteur;
            Statut = statut;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEnquete { get; set; }

        public string NoEnquete { get; set; }

        public int Departement { get; set; }

        public DateTime DateDepot { get; set; }
        
        public int? IdInfracteur { get; set; }

        public virtual Personne Infracteur {get; set; }
        
        public int? IdPlaignant { get; set; }

        public virtual Personne Plaignant {get; set; }

        public string Motif { get; set; }
        
        public int? IdEnqueteur { get; set; }

        public virtual SpaPersonne Enqueteur { get; set; }

        public int Statut {get; set; }

    
        public IQueryable<Document> GetAllDocuments()
        {
            using var db = new Context();
            return db.Document.Where(v => v.NoEnquete == this.NoEnquete);
        }
    }
}