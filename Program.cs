using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System;
using System.ComponentModel.Design;

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
                    kunden = JsonSerializer.Deserialize<List<Kunde>>(File.ReadAllText(kundenDatei)) ?? new List<Kunde>();
                if (File.Exists(adminDatei))
                    admins = JsonSerializer.Deserialize<List<BankMitarbeiter>>(File.ReadAllText(adminDatei)) ?? new List<BankMitarbeiter>();          
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
                    case "1": if (kunden.Count != 0)
                        { Login(false); break; }
                        else
                        {
                            Console.WriteLine("Noch kein Kunde registriert!");
                            break;
                        }
                    case "2": Login(true); break;
                            case "3": Registrieren(); break;
                            case "4": DatenSpeichern(); return;
                            }
            }

            static void Registrieren(bool istAdmin = false)
            {
                var (name, pw) = BenutzerAnmeldedaten();

                if (istAdmin)
                {
                    admins.Add(new BankMitarbeiter(name, pw, true));
                    Console.WriteLine("Bankmitarbeiter erfolgreich registriert.");

                }
                else
                {
                    kunden.Add(new Kunde(name, pw));
                    Console.WriteLine("Kunde erfolgreich registriert.");


                }
                DatenSpeichern();


            }

            static (string name, string pw) BenutzerAnmeldedaten()
            {
                string name, pw;
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
                Console.WriteLine();
                if (benutzer != null)
                {
                    Console.WriteLine($"Login erfolgreich: {benutzer.BenutzerName}");

                    if (benutzer is BankMitarbeiter admin)
                    {
                        admin.Log($"{admin.BenutzerName} hat sich eingeloggt.");
                        AdminMenu(admin);
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
                    Console.ForegroundColor = ConsoleColor.Green;
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
                        case "1":
                            Console.WriteLine($"Kontostand: {k.Kontostand} EUR, Festgeld: {k.Festgeld}, Kredit: {k.Kredit}");
                            break;
                        case "2":
                            Console.Write("Betrag: ");
                            k.Einzahlen(decimal.Parse(Console.ReadLine()));
                            break;
                        case "3":
                            Console.Write("Betrag: ");
                            if (!k.Abheben(decimal.Parse(Console.ReadLine())))
                                Console.WriteLine("Nicht genug Guthaben");
                            else
                                DatenSpeichern();
                            break;
                        case "4":
                            Console.Write("Betrag: "); decimal b = decimal.Parse(Console.ReadLine());
                            Console.Write("Zinssatz: "); double z = double.Parse(Console.ReadLine());
                            Console.Write("Monate: "); int m = int.Parse(Console.ReadLine());
                            k.FestgeldAnlegen(b, z, m);
                            DatenSpeichern();
                            break;
                        case "5":
                            Console.Write("Betrag: "); decimal kB = decimal.Parse(Console.ReadLine());
                            Console.Write("Zinssatz: "); double kZ = double.Parse(Console.ReadLine());
                            Console.Write("Monate: "); int kM = int.Parse(Console.ReadLine());
                            k.KreditAufnehmen(kB, kZ, kM);
                            DatenSpeichern();
                            break;
                        case "6":
                            k.MonatsAbrechnung();
                            Console.WriteLine("Abrechnung durchgeführt.");
                            DatenSpeichern();
                            break;
                        case "0":
                            Console.ResetColor();
                            return;
                    }
                }
            }




            static void AdminMenu(BankMitarbeiter admin)
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nAdmin-Menü:");
                    Console.WriteLine("1. Alle Kunden anzeigen");
                    Console.WriteLine("2. Kontostand eines Kunden einsehen");
                    Console.WriteLine("3. Festgeld & Kredit eines Kunden prüfen");
                    Console.WriteLine("4. Kunden entfernen");
                    Console.WriteLine("0. Logout");

                    string? eingabe = Console.ReadLine();
                    if ((eingabe == "1" || eingabe == "2" || eingabe =="3" || eingabe == "4") && kunden.Count == 0)
                    {
                        Console.WriteLine("Noch kein Kunde registriert!");
                    }
                    else
                        switch (eingabe)
                        {
                            case "1":
                                Console.WriteLine("\nListe der Kunden:");
                                admin.Log("Alle Kunden angezeigt.");
                                foreach (var kunde in kunden)
                                {
                                    Console.WriteLine($"- {kunde.BenutzerName}");
                                }
                                if (kunden.Count == 0)
                                    Console.WriteLine("Noch kein Kunde registriert!");
                                break;

                            case "2":
                                Kunde? k = KundenAuswahl();
                                if (k != null)
                                {
                                    Console.WriteLine($"Kontostand von {k.BenutzerName}: {k.Kontostand} EUR");
                                    admin.Log($"Kontostand von {k.BenutzerName} eingesehen.");
                                }
                                break;

                            case "3":
                                Kunde? k2 = KundenAuswahl();
                                if (k2 != null)
                                {
                                    Console.WriteLine($"Festgeld: {k2.Festgeld} EUR, Kredit: {k2.Kredit} EUR");
                                    admin.Log($"Festgeld und Kredit von {k2.BenutzerName} eingesehen.");
                                }
                                break;

                            case "4":
                                Kunde? k3 = KundenAuswahl();
                                if (k3 != null)
                                {
                                    kunden.Remove(k3);
                                    Console.WriteLine($"Kunde {k3.BenutzerName} wurde entfernt.");
                                    admin.Log($"Kunde {k3.BenutzerName} wurde gelöscht.");
                                    DatenSpeichern();
                                }
                                break;

                            case "0":
                                Console.WriteLine("Logout erfolgreich.");
                                Console.ResetColor();
                                return;
                        }
                }
            }

            // Hilfsmethode zur Auswahl eines Kunden
            static Kunde? KundenAuswahl()
            {
                Console.WriteLine("Bitte Kundennamen eingeben:");
                string? name = Console.ReadLine();
                var kunde = kunden.Find(k => k.BenutzerName == name);

                if (kunde == null)
                    Console.WriteLine("Kunde nicht gefunden!");

                return kunde;
            }

            static void DatenSpeichern()
            {
                File.WriteAllText(kundenDatei, JsonSerializer.Serialize(kunden));
                File.WriteAllText(adminDatei, JsonSerializer.Serialize(admins));
            }
            
        }
    }
}


    
