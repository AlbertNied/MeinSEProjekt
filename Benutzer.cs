using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MeinSEProjekt
{
    abstract class Benutzer
    {

        public string BenutzerName { get; set; } = string.Empty;
        public string Passwort { get; set; } = string.Empty;

        
        public Benutzer() { }

        public Benutzer(string benutzername, string passwort) 
        {
            BenutzerName = benutzername;
            Passwort = HashPasswort(passwort);
        }


        public bool Pruefen(string passwort)
        {
            return Passwort == HashPasswort(passwort);
        }

        private string HashPasswort(string passwort)
        {
            using var sha = SHA256.Create();
            var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(passwort));
            return Convert.ToBase64String(hashBytes);
        }

    }
}
