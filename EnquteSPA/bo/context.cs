using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EnquteSPA.bo
{
    class context : DbContext
    {
        public DbSet<Compte> Comptes { get; set; }
    }
}
