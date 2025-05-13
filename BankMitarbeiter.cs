using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinSEProjekt
{
    class BankMitarbeiter : Benutzer
    {

        public BankMitarbeiter(string benutzername, string passwort) : base(benutzername, passwort) { }

        public string Benutzername { get; internal set; }

        public void Log(string nachricht)
        {
            File.AppendAllText("log.txt", $"[{DateTime.Now}] {nachricht}\n");
        }
    }
}