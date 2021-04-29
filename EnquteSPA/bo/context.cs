using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EnquteSPA.bo
{
    class context : DbContext
    {
        public DbSet<Compte> Compte { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Personne> Personne { get; set; }
        public DbSet<Enquete> Enquete { get; set; }
        public DbSet<SpaPersonne> SpaPersonne { get; set; }
        public DbSet<Visite> Visite { get; set; }

    }
}
