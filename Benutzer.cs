using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeinSEProjekt
{
    public abstract class Benutzer
    {

        public string BenutzerName { get; set; }
        public string Passwort { get; set; }

        // Dieser Konstruktor wird beim Deserialisieren von JSON verwendet
        [JsonConstructor]
        public Benutzer(string benutzerName, string passwort)
        {
            BenutzerName = benutzerName;
            Passwort = passwort; 
        }

        // Dieser wird bei manueller Registrierung verwendet
        protected Benutzer(string benutzername, string passwort, bool istRegistrierung)
        {
            BenutzerName = benutzername;
            Passwort = istRegistrierung ? HashPasswort(passwort) : passwort;
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
