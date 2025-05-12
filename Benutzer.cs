using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinSEProjekt
{
    abstract class Benutzer
    {

        public string BenutzerName { get; set; }
        public string Passwort { get; set; }

        

        public Benutzer(string benutzername, string passwort) 
        {
            BenutzerName = benutzername;
            Passwort = passwort;
        }

    }
}
