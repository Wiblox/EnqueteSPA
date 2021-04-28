using EnquteSPA.bo;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnquteSPA.Controller
{
    class User
    {
        // Création utilisateur
        public User(String nom, String prenom, String mail, Boolean admin, String mdp)
        {
            using (var db = new context())
            {
                var personne = new Personne { Nom = nom, Prenom = prenom, Mail = mail };
                db.Personne.Add(personne);
                var spaPersonne = new SpaPersonne { IdPersonne = personne.IdPersonne, Fonction = admin ? 1 : 2, DelegueEnqueteur = false, Etat = true };
                db.SpaPersonne.Add(spaPersonne);
                var compte = new Compte { IdSpaPersonne = spaPersonne.IdSpaPersonne, MotDePasse = mdp };
                db.Compte.Add(compte);
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