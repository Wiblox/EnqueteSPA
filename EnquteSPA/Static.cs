﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EnquteSPA
{
    public static class Static
    {

        public static bool utilisateur;
        public static Compte utilisateurCourant;

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str[1..].ToLower();

            return str.ToUpper();
        }
    }
}
