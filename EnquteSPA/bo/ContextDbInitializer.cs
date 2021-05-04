using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EnquteSPA.bo
{
    class ContextDbInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context) {

            // ETAPE 1 : Génération des SpaPersonne
            IList<SpaPersonne> spaPersonnes = new List<SpaPersonne>
            {
                new SpaPersonne() { Nom = "Leroy", Prenom = "Daniel", Mail = "dleroy@gmail.com", Ville = "Boulogne-Billancourt", Rue = "Rue Gallieni", Numero = "15", IdFonction = 1, DelegueEnqueteur = true, Etat = true },
                new SpaPersonne() { Nom = "Muller", Prenom = "Odile", Mail = "omuller@hotmail.com", Ville = "Muret", Rue = "Avenue des Pyrénéees", Numero = "5", IdFonction = 2, DelegueEnqueteur = true, Etat = true },
                new SpaPersonne() { Nom = "Jacquet", Prenom = "Jacques", Mail = "jjacquet@free.fr", Ville = "Soissons", Rue = "Avenue de Reims", Numero = "25", IdFonction = 3, DelegueEnqueteur = true, Etat = true },
                new SpaPersonne() { Nom = "Renault", Prenom = "Josiane", Mail = "jrenault@gmail.com", Ville = "Nantes", Rue = "Boulevard René Coty", Numero = "10", IdFonction = 3, DelegueEnqueteur = true, Etat = true }
            };

            context.SpaPersonne.AddRange(spaPersonnes);


            // ETAPE 1 : Génération des Personnes
            IList<Personne> personnes = new List<Personne>
            {
                new Personne() { Nom = "Dupont", Prenom = "Christine", Mail = "pdupont@gmail.com", Ville = "Paris", Rue = "Avenue des Champs-Elysées", Numero = "15" },
                new Personne() { Nom = "Bernard", Prenom = "Thomas", Mail = "tbernard@hotmail.com", Ville = "Saint-Denis", Rue = "Boulevard Carnot", Numero = "2" },
                new Personne() { Nom = "Thomas", Prenom = "Lucie", Mail = "lthomas@icloud.com", Ville = "Toulouse", Rue = "Rue Lafayette", Numero = "4" },
                new Personne() { Nom = "Petit", Prenom = "Michel", Mail = "mpetit@gmail.com", Ville = "Colomiers", Rue = "Boulevard Emile Calvet", Numero = "5" },
                new Personne() { Nom = "Robert", Prenom = "Danielle", Mail = "drobert@gmail.com", Ville = "Reims", Rue = "Rue de Mars", Numero = "20" },
                new Personne() { Nom = "Richard", Prenom = "Jean", Mail = "jrichard@hotmail.com", Ville = "Muizon", Rue = "Rue de la Gare", Numero = "6" },
                new Personne() { Nom = "Durand", Prenom = "Jacqueline", Mail = "pdurand@orange.fr", Ville = "Brest", Rue = "Boulevard Jean Moulin", Numero = "12" },
                new Personne() { Nom = "Dubois", Prenom = "Patrice", Mail = "pdubois@free.fr", Ville = "Gouesnou", Rue = "Rue du Bois", Numero = "10" }
            };

            context.Personne.AddRange(personnes);


            // ETAPE 2 : Génération des Enquêtes
            IList<Enquete> enquetes = new List<Enquete>
            {
                new Enquete() { NoEnquete = "75-2020-01-0001", Departement = "75", DateDepot = new DateTime(2020, 1, 10, 12, 0, 0), IdInfracteur = 1, IdPlaignant = 2, Motif = "Maltraitance", IdEnqueteur = null, Statut = (int)modele.StatutEnquete.NON_ASSIGNEE },
                new Enquete() { NoEnquete = "31-2020-02-0001", Departement = "31", DateDepot = new DateTime(2020, 2, 2, 12, 0, 0), IdInfracteur = 3, IdPlaignant = 4, Motif = "Abandon", IdEnqueteur = 2, Statut = (int)modele.StatutEnquete.EN_COURS },
                new Enquete() { NoEnquete = "51-2020-11-0001", Departement = "51", DateDepot = new DateTime(2020, 11, 20, 12, 0, 0), IdInfracteur = 5, IdPlaignant = 6, Motif = "Animal mort", IdEnqueteur = 3, Statut = (int)modele.StatutEnquete.EN_COURS },
                new Enquete() { NoEnquete = "54-2021-05-0001", Departement = "54", DateDepot = new DateTime(2021, 5, 2, 12, 0, 0), IdInfracteur = 7, IdPlaignant = 8, Motif = "Accumulation d'animaux dans une petite surface", IdEnqueteur = 4, Statut = (int)modele.StatutEnquete.RENDUE }
            };

            context.Enquete.AddRange(enquetes);

            base.Seed(context);
        }

    }
}
