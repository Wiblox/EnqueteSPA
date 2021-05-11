using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EnquteSPA.bo
{
    class ContextDbInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {

            // ETAPE 1 : Génération du personnel
            IList<SpaPersonne> spaPersonnes = new List<SpaPersonne>
            {
                new SpaPersonne("", "Atos", "atos@mail.com", "Boulogne-Billancourt", "Rue Gallieni", "15", true, true, true),
                new SpaPersonne("", "Portos", "portos@mail.com", "Muret", "Avenue des Pyrénéees", "5", true, true, true),
                new SpaPersonne("", "Aramis", "aramis@mail.com", "Soissons", "Avenue de Reims", "25", true, false, true),
                new SpaPersonne("", "Luigi", "luigi@gmail.com", "Nantes", "Boulevard René Coty", "10", false, true, true),
                new SpaPersonne("", "Mario", "mario@gmail.com", "Nantes", "Boulevard René Coty", "10", false, false, true)
            };

            context.SpaPersonne.AddRange(spaPersonnes);


            // ETAPE 2 : Génération des Personnes
            IList<Personne> personnes = new List<Personne>
            {
                //new Personne() { Denomination = "Dupont Christine", Mail = "pdupont@gmail.com", Ville = "Paris", Rue = "Avenue des Champs-Elysées", Numero = "15" },
                //new Personne() { Denomination = "Bernard Thomas", Mail = "tbernard@hotmail.com", Ville = "Saint-Denis", Rue = "Boulevard Carnot", Numero = "2" },
                //new Personne() { Denomination = "Thomas Lucie", Mail = "lthomas@icloud.com", Ville = "Toulouse", Rue = "Rue Lafayette", Numero = "4" },
                //new Personne() { Denomination = "Petit Michel", Mail = "mpetit@gmail.com", Ville = "Colomiers", Rue = "Boulevard Emile Calvet", Numero = "5" },
                //new Personne() { Denomination = "Robert Danielle", Mail = "drobert@gmail.com", Ville = "Reims", Rue = "Rue de Mars", Numero = "20" },
                //new Personne() { Denomination = "Richard Jean", Mail = "jrichard@hotmail.com", Ville = "Muizon", Rue = "Rue de la Gare", Numero = "6" },
                //new Personne() { Denomination = "Durand Jacqueline", Mail = "pdurand@orange.fr", Ville = "Brest", Rue = "Boulevard Jean Moulin", Numero = "12" },
                //new Personne() { Denomination = "Dubois Patrice", Mail = "pdubois@free.fr", Ville = "Gouesnou", Rue = "Rue du Bois", Numero = "10" }
                new Personne("L214", "l214@mail.com", "67204 Achenheim", "rue du soleil", "4"),
                new Personne("Les Mousquetaires", "lesmousquetaires@mail.com", "57000 Metz", "Rue Taison", "18"),
                new Personne("PETA France", "petafrance@mail.com", "75008 Paris", "Place de la Madeleine", "6"),
                new Personne("Nintendo", "nintendo@mail.com", "95031 Cergy", "Boulevard de l'Oise", "6")
            };

            context.Personne.AddRange(personnes);


            // ETAPE 3 : Génération des Enquêtes
            IList<Enquete> enquetes = new List<Enquete>
            {
                //new Enquete() { NoEnquete = "75/20/01/001", Departement = "75", DateDepot = new DateTime(2020, 1, 10, 12, 0, 0), IdInfracteur = 1, IdPlaignant = 2, Motif = "Maltraitance", IdEnqueteur = null, Statut = (int)modele.StatutEnquete.NON_ASSIGNEE },
                //new Enquete() { NoEnquete = "31/20/02/001", Departement = "31", DateDepot = new DateTime(2020, 2, 2, 12, 0, 0), IdInfracteur = 3, IdPlaignant = 4, Motif = "Abandon", IdEnqueteur = 2, Statut = (int)modele.StatutEnquete.EN_COURS },
                //new Enquete() { NoEnquete = "51/20/11/001", Departement = "51", DateDepot = new DateTime(2020, 11, 20, 12, 0, 0), IdInfracteur = 5, IdPlaignant = 6, Motif = "Animal mort", IdEnqueteur = 3, Statut = (int)modele.StatutEnquete.EN_COURS },
                //new Enquete() { NoEnquete = "54/21/05/001", Departement = "54", DateDepot = new DateTime(2021, 5, 2, 12, 0, 0), IdInfracteur = 7, IdPlaignant = 8, Motif = "Accumulation d'animaux dans une petite surface", IdEnqueteur = 4, Statut = (int)modele.StatutEnquete.RENDUE }
                new Enquete("57/21/05/001", "refuge", "Cochons", "57", DateTime.Now, 2, 1, "Maltraitance cochons.", 4, (int)modele.StatutEnquete.EN_COURS),
                new Enquete("75/21/05/001", "Paris", "NAC", "75", DateTime.Now, 4, 3, "Maltraitance NAC.", 1, (int)modele.StatutEnquete.EN_COURS),
            };

            context.Enquete.AddRange(enquetes);

            base.Seed(context);
        }

    }
}
