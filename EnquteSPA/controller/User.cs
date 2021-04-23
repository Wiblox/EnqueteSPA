using EnquteSPA.bo;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnquteSPA.Controller
{
    class User
    {
        // Création utilisateur
        public User(String nom, String prenom, String mail, Boolean admin)
        {
            using (var db = new context())
            {
                var personne = new Personne { Nom = nom, Prenom = prenom, Mail = mail };
                db.Personne.Add(personne);
                var spaPersonne = new SpaPersonne { IdPersonne = personne.IdPersonne, Fonction = admin ? 1 : 2, DelegueEnqueteur = false, Etat = true };
                db.SpaPersonne.Add(spaPersonne);
                db.SaveChanges();
            }
        }


        // Check si admin
        public Boolean isAdmin(Personne p)
        {
            using (var db = new context())
            {
                return db.SpaPersonne.Find(p.IdPersonne).IdFonction == 1;
            }
        }
    }
}