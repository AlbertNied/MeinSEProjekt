﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MeinSEProjekt
{
    public class Kunde : Benutzer, ITransaktion

    {
        public decimal Kontostand { get; private set; } 
        public decimal Festgeld { get; private set; } 
        public decimal Kredit { get; private set; }

        // Für JSON: verwendet geerbten JsonConstructor aus Benutzer
        [JsonConstructor]
        public Kunde(string benutzername, string passwort, decimal kontostand, decimal festgeld, decimal kredit)
            : base(benutzername, passwort)
        {
            Kontostand = kontostand;
            Festgeld = festgeld;
            Kredit = kredit;
        }

        // Für Registrierung
        public Kunde(string benutzername, string passwort)
            : base(benutzername, passwort, true) { }

        public void Einzahlen(decimal betrag) => Durchfueren(betrag);

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

        public void Durchfueren(decimal betrag)
        {
            Kontostand += betrag;
        }
    }
}
