using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinSEProjekt
{
    class Kunde : Benutzer
    {
        public Kunde(string benutzername, string passwort) : base(benutzername, passwort) { }
    }
}
