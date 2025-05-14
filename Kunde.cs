using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinSEProjekt
{
    class Kunde : Benutzer

    {
        public decimal Kontostand { get; set; } = 0;
        public decimal Festgeld { get; set; } = 0;
        public decimal Kredit { get; set; } = 0;

        public Kunde(string benutzername, string passwort) : base(benutzername, passwort) { }

        public string Benutzername { get; internal set; }

        public void Erledigen(decimal betrag)
        {
            Kontostand += betrag;
        }

        public void Einzahlen(decimal betrag) => Erledigen(betrag);

        public bool Abheben(decimal betrag)
        {
            if (betrag > Kontostand) return false;
            Kontostand -= betrag;
            return true;
        }

        public void FestgeldAnlegen(decimal betrag, double zinsen, int monate)
        {
            Festgeld += betrag + betrag * (decimal)zinsen * monate / 12 / 100;
        }

        public void KreditAufnehmen(decimal betrag, double zinsen, int monate)
        {
            Kredit += betrag + betrag * (decimal)zinsen * monate / 12 / 100;
            Kontostand += betrag;
        }

        public void MonatsAbrechnung()
        {
            Festgeld *= 1.01m;
        }
    }
}
