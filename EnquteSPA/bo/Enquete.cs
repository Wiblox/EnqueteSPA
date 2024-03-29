using EnquteSPA.bo;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EnquteSPA
{
    [Table("Enquete")]
    public class Enquete
    {
        public Enquete() { }

        public Enquete(string noEnquete, string objet, string race, string departement, DateTime dateDepot, int? idInfracteur, int? idPlaignant, string motif, int? idEnqueteur, int statut)
        {
            NoEnquete = noEnquete;
            Objet = objet;
            Race = race;
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

        public string Objet { get; set; }

        public string Race { get; set; }

        public string Departement { get; set; }

        public DateTime DateDepot { get; set; }
        
        public int? IdInfracteur { get; set; }
        
        public int? IdPlaignant { get; set; }

        public string Motif { get; set; }
        
        public int? IdEnqueteur { get; set; }

        public int Statut {get; set; }

        public IQueryable<Document> GetAllDocuments()
        {
            using var db = new Context();
            return db.Document.Where(v => v.NoEnquete == this.IdEnquete);
        }
    }
}