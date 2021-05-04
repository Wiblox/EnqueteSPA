using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EnquteSPA.modele
{
    public class Erreur
    {
        private List<string> MsgRetour;

        public Erreur()
        {
            MsgRetour = new List<string>();
        }

        public bool TestNonVide(string txt, string libelle = "")
        {
            if(txt.Length == 0)
            {
                if(libelle.Length > 0)
                    MsgRetour.Add($"{libelle} est vide.");
                return false;
            }
            return true;
        }

        public bool TestMail(string txt, string libelle = "")
        {
            bool retour;
            try
            {
                var addr = new System.Net.Mail.MailAddress(txt);
                retour = addr.Address == txt;
            }
            catch
            {
                retour = false;
            }
            if(!retour && libelle.Length > 0)
                MsgRetour.Add($"{libelle} n'est pas un mail.");
            return retour;
        }

        public bool TestNumber(string txt, string libelle = "")
        {
            Regex regex = new Regex("^[1-9][0-9]*$");
            bool retour = regex.IsMatch(txt);
            if (!retour && libelle.Length > 0)
                MsgRetour.Add($"{libelle} n'est pas un nombre.");
            return retour;
        }
        public bool TestDepartement(string txt, string libelle = "")
        {
            Regex regex = new Regex("^(([0-9]|[0-8][0-9])|(9[0-5])|(2[A|B])|(97[1-4|6]))$");
            bool retour = regex.IsMatch(txt);
            if (!retour && libelle.Length > 0)
                MsgRetour.Add($"{libelle} n'est pas un département.");
            return retour;
        }

        public bool TestMotDePasse(string txt, string libelle = "")
        {
            if (txt.Length < 5)
            {
                if(libelle.Length > 0)
                    MsgRetour.Add($"{libelle} doit faire au moins 5 caractères.");
                return false;
            }
            return true;
        }

        public void AddToMsgRetour(string txt)
        {
            if (txt.Length > 0)
                MsgRetour.Add(txt);
        }
        
        public string GetMsgRetour()
        {
            return String.Join("\n", MsgRetour.ToArray());
        }

        public void ResetMsgRetour()
        {
            MsgRetour = new List<string>();
        }

        public bool IsSafe()
        {
            return MsgRetour.Count == 0;
        }
    }
}
