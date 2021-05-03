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
            IList<SpaPersonne> spaPersonnes = new List<SpaPersonne>();

            spaPersonnes.Add(new SpaPersonne() { Nom = "Leroy", Prenom = "Daniel", Mail = "dleroy@gmail.com", Ville = "Boulogne-Billancourt", Rue = "Rue Gallieni", Numero = "15", IdFonction = 1, DelegueEnqueteur = true, Etat = true });
            spaPersonnes.Add(new SpaPersonne() { Nom = "Muller", Prenom = "Odile", Mail = "omuller@hotmail.com", Ville = "Muret", Rue = "Avenue des Pyrénéees", Numero = "5", IdFonction = 2, DelegueEnqueteur = true, Etat = true });
            spaPersonnes.Add(new SpaPersonne() { Nom = "Jacquet", Prenom = "Jacques", Mail = "jjacquet@free.fr", Ville = "Soissons", Rue = "Avenue de Reims", Numero = "25", IdFonction = 3, DelegueEnqueteur = true, Etat = true });
            spaPersonnes.Add(new SpaPersonne() { Nom = "Renault", Prenom = "Josiane", Mail = "jrenault@gmail.com", Ville = "Nantes", Rue = "Boulevard René Coty", Numero = "10", IdFonction = 3, DelegueEnqueteur = true, Etat = true });


            context.SpaPersonne.AddRange(spaPersonnes);


            // ETAPE 1 : Génération des Personnes
            IList<Personne> personnes = new List<Personne>();

            personnes.Add(new Personne() { Nom = "Dupont", Prenom = "Christine", Mail = "pdupont@gmail.com", Ville = "Paris", Rue = "Avenue des Champs-Elysées", Numero = "15" });
            personnes.Add(new Personne() { Nom = "Bernard", Prenom = "Thomas", Mail = "tbernard@hotmail.com", Ville = "Saint-Denis", Rue = "Boulevard Carnot", Numero = "2" });
            personnes.Add(new Personne() { Nom = "Thomas", Prenom = "Lucie", Mail = "lthomas@icloud.com", Ville = "Toulouse", Rue = "Rue Lafayette", Numero = "4" });
            personnes.Add(new Personne() { Nom = "Petit", Prenom = "Michel", Mail = "mpetit@gmail.com", Ville = "Colomiers", Rue = "Boulevard Emile Calvet", Numero = "5" });
            personnes.Add(new Personne() { Nom = "Robert", Prenom = "Danielle", Mail = "drobert@gmail.com", Ville = "Reims", Rue = "Rue de Mars", Numero = "20" });
            personnes.Add(new Personne() { Nom = "Richard", Prenom = "Jean", Mail = "jrichard@hotmail.com", Ville = "Muizon", Rue = "Rue de la Gare", Numero = "6" });
            personnes.Add(new Personne() { Nom = "Durand", Prenom = "Jacqueline", Mail = "pdurand@orange.fr", Ville = "Brest", Rue = "Boulevard Jean Moulin", Numero = "12" });
            personnes.Add(new Personne() { Nom = "Dubois", Prenom = "Patrice", Mail = "pdubois@free.fr", Ville = "Gouesnou", Rue = "Rue du Bois", Numero = "10" });


            context.Personne.AddRange(personnes);


            // ETAPE 2 : Génération des Enquêtes
            IList<Enquete> enquetes = new List<Enquete>();

            enquetes.Add(new Enquete() { NoEnquete = "75/2010/01/1000", Departement = 75,  DateDepot = new DateTime(2010, 1, 10, 10, 15, 00), IdInfracteur = 1, IdPlaignant = 2, Motif = "Maltraitance", IdEnqueteur = 1, Statut = (int) modele.StatutEnquete.NON_ASSIGNEE });
            enquetes.Add(new Enquete() { NoEnquete = "31/2012/10/0685", Departement = 31, DateDepot = new DateTime(2012, 10, 2, 20, 12, 10), IdInfracteur = 3, IdPlaignant = 4, Motif = "Abdandon", IdEnqueteur = 2, Statut = (int)modele.StatutEnquete.EN_COURS });
            enquetes.Add(new Enquete() { NoEnquete = "51/2018/06/0301", Departement = 51, DateDepot = new DateTime(2018, 6, 20, 15, 25, 50), IdInfracteur = 5, IdPlaignant = 6, Motif = "Animal mort", IdEnqueteur = 3, Statut = (int)modele.StatutEnquete.EN_COURS });
            enquetes.Add(new Enquete() { NoEnquete = "29/2020/08/0256", Departement = 29, DateDepot = new DateTime(2020, 8, 15, 5, 40, 30), IdInfracteur = 7, IdPlaignant = 8, Motif = "Accumulation d'animaux dans une petite surface", IdEnqueteur = 4, Statut = (int)modele.StatutEnquete.RENDUE });

            context.Enquete.AddRange(enquetes);


            base.Seed(context);
        }

    }
}
