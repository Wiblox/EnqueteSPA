using EnquteSPA.bo;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EnquteSPA
{
    [Table("Compte")]
    public class Compte
    {
        public Compte() { }

        public Compte(string mail, string motDePasse, bool admin)
        {
            Mail = mail;
            MotDePasse = motDePasse;
            Admin = admin;

            using var db = new Context();
            db.Compte.Add(this);
            db.SaveChanges();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompte { get; set; }
        public string Mail { get; set; }
        public string MotDePasse { get; set; }
        public bool Admin { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Compte compte &&
                   Mail == compte.Mail &&
                   MotDePasse == compte.MotDePasse &&
                   Admin == compte.Admin;
        }

        public static Compte CheckCompte(string mail, string mdp)
        {
            using var db = new Context();
            IQueryable<Compte> cTab = db.Compte.Where(v => v.Mail == mail && v.MotDePasse == mdp);
            //IQueryable<Compte> cTab = from v in db.Compte where v.Mail == mail && v.MotDePasse == mdp select v;
            return cTab.Count() == 0 ? null : cTab.First();
        }
    }
}
