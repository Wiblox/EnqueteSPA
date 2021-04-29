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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEnquete { get; set; }

        public string NoEnquete { get; set; }

        public int Departement { get; set; }

        public DateTime DateDepot { get; set; }
        
        [ForeignKey("Infracteur")]
        public int? IdInfracteur { get; set; }

        public virtual Personne Infracteur {get; set; }
        
        [ForeignKey("Plaignant")]
        public int? IdPlaignant { get; set; }

        public virtual Personne Plaignant {get; set; }

        public string Motif { get; set; }
        
        [ForeignKey("Enqueteur")]
        public int? IdEnqueteur { get; set; }

        public virtual SpaPersonne Enqueteur { get; set; }

        public int Statut {get; set; }

        public void AddDocument(TypeDocument td, string path)
        {
            using var db = new context();
            db.Document.Add(new Document(this, td, path));
            db.SaveChanges();
        }

        public IQueryable<Document> getAllDocuments()
        {
            using var db = new context();
            return db.Document.Where(v => v.IdEnquete == this.IdEnquete);
        }
    }
}