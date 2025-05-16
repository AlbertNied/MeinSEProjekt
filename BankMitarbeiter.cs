using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MeinSEProjekt
{
    public class BankMitarbeiter : Benutzer
    {

        [JsonConstructor]
        public BankMitarbeiter(string benutzername, string passwort)
            : base(benutzername, passwort) { }

        public BankMitarbeiter(string benutzername, string passwort, bool istRegistrierung)
            : base(benutzername, passwort, istRegistrierung) { }

        public void Log(string nachricht)
        {
            File.AppendAllText("log.txt", $"[{DateTime.Now}] {nachricht}\n");
        }
    }
}