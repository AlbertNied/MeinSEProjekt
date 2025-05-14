using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System;

namespace MeinSEProjekt
{
    class Program
    {
        static List<Kunde> kunden = new();
        static List<BankMitarbeiter> admins = new();
        const string kundenDatei = "kunden.json";
        const string adminDatei = "admins.json";

        static void Main()
        {

            DatenLaden();

            static void DatenLaden()
            {
                if (File.Exists(kundenDatei))
                {
                    JsonSerializer.Deserialize<List<Kunde>>(File.ReadAllText(kundenDatei));
                }
                if (File.Exists(adminDatei))
                {
                    JsonSerializer.Deserialize<List<BankMitarbeiter>>(File.ReadAllText(adminDatei));
                }
            }

            static void DatenSpeichern()
            {
                File.WriteAllText(kundenDatei, JsonSerializer.Serialize(kunden));
                File.WriteAllText(adminDatei, JsonSerializer.Serialize(admins));
            }

            if (admins.Count == 0)
            {
                Console.WriteLine("Noch kein Bankmitarbeiter registriert. Bitte registrieren.");
                Registrieren(true);
            }

            Console.WriteLine("Willkommen zur Banksoftware");

            while (true)
            {
                Console.WriteLine("1. Login als Kunde");
                Console.WriteLine("2. Login als Admin");
                Console.WriteLine("3. Registrieren");
                Console.WriteLine("4. Beenden");

                string? auswahl = Console.ReadLine();
                switch (auswahl)
                {
                    case "1": Login(false); break;
                    case "2": Login(true); break;
                    case "3": Registrieren(); break;
                    case "4": DatenSpeichern(); return;
                }
            }

            static void Registrieren(bool istAdmin = false)
            {
                var (name, pw) = BenutzerAnmeldedaten();

                if (istAdmin == true)
                {
                    admins.Add(new BankMitarbeiter(name, pw));
                    Console.WriteLine("Bankmitarbeiter erfolgreich registriert.");
                }
                else
                {
                    kunden.Add(new Kunde(name, pw));
                    Console.WriteLine("Kunde erfolgreich registriert.");
                }
            }

            static (string name, string pw) BenutzerAnmeldedaten()
            {

                string? name, pw;
                do
                {
                    Console.Write("Benutzername: ");
                    name = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(name));

                do
                {
                    Console.Write("Passwort: ");
                    pw = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(pw));

                return (name, pw);
            }

            static void Login(bool istAdmin)
            {
                var (name, pw) = BenutzerAnmeldedaten();
                List<Benutzer> benutzerListe = istAdmin ? new List<Benutzer>(admins) : new List<Benutzer>(kunden);
                var benutzer = benutzerListe.Find(b => b.BenutzerName == name && b.Pruefen(pw));

                if (benutzer != null)
                {
                    Console.WriteLine($"Login erfolgreich: {benutzer.BenutzerName}");

                    if (benutzer is BankMitarbeiter admin)
                    {
                        admin.Log($"{admin.Benutzername} hat sich eingeloggt.");
                    }
                    else if (benutzer is Kunde kunde)
                    {
                        KundenMenu(kunde);
                    }
                }
                else
                {
                    Console.WriteLine("Login fehlgeschlagen.");
                }
            }

            static void KundenMenu(Kunde k)
            {

                while (true)
                {

                    Console.WriteLine("1. Kontostand anzeigen");
                    Console.WriteLine("2. Einzahlen");
                    Console.WriteLine("3. Abheben");
                    Console.WriteLine("4. Festgeld anlegen");
                    Console.WriteLine("5. Kredit aufnehmen");
                    Console.WriteLine("6. Monatsabrechnung");
                    Console.WriteLine("0. Logout");
                    string? kundeEingabe = Console.ReadLine();

                    switch (kundeEingabe)
                    {
                        case "1": Console.WriteLine($"Kontostand: {k.Kontostand} EUR, Festgeld: {k.Festgeld}, Kredit: {k.Kredit}"); break;
                        case "2": Console.Write("Betrag: "); k.Einzahlen(decimal.Parse(Console.ReadLine())); break;
                      
                    }
                }
            }

        }
    }
}