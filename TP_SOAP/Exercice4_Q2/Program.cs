using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Exercice4_App.Forms;
using System.Threading.Tasks;
using Exercice4_App.IbisHotelServices;
using Exercice4_App.Formule1HotelServices;
using Newtonsoft.Json;

namespace Exercice4_App
{
    public class Program
    {
        private static IbisServices ibisServices = new IbisServices();
        private static Formule1Services formule1Services = new Formule1Services();
        static void Main(string[] args)
        {

            // --- Services des hotels
            ibisServices.GenerateHotel(); // Met en place l'hotel (chambres, agence partenaire, etc..). Remplace une base de données
            formule1Services.GenerateHotel(); // Met en place l'hotel (chambres, agence partenaire, etc..). Remplace une base de données

            string ibisHotel = ibisServices.GetHotel();
            string formule1Hotel = formule1Services.GetHotel();
            string[] hotels = new string[2] { ibisHotel, formule1Hotel };

            ConsoleWriteLineWithForegroundColor(ConsoleColor.DarkYellow, "\t\t\tBienvenue sur l'agence Trivaga.\n");
            ConsoleWriteLineWithForegroundColor(ConsoleColor.DarkYellow, "\t\tL'agence d'hotel au plus proche de ses clients.");

            while (true)
            {
                // --- On demande l'hotel à consulter
                int hotelChoose = askHotel(hotels);

                // --- Authentification simple (login: agenceName || anything, password: admin)
                string[] auth = HandleAuthentification("Trivaga");

                int flag = 0;
                switch(hotelChoose)
                {
                    case 1:
                        while (!ibisServices.Authentification(auth[0], auth[1]))
                        {
                            if (auth[0].Equals(auth[1]) && auth[0].Equals("exit"))
                            {
                                flag = -1;
                                break;
                            }
                            ConsoleWriteLineWithFlag(3, "Login ou mot de passe incorrect. Veuillez recommencer");
                            auth = HandleAuthentification("Trivaga");
                            flag = 0;
                        }
                        break;
                    case 2:
                        while (!formule1Services.Authentification(auth[0], auth[1]))
                        {
                            if (auth[0].Equals(auth[1]) && auth[0].Equals("exit"))
                            {
                                flag = -1;
                                break;
                            }
                            ConsoleWriteLineWithFlag(3, "Login ou mot de passe incorrect. Veuillez recommencer");
                            auth = HandleAuthentification("Trivaga");
                            flag = 0;
                        }
                        break;
                    default:
                        flag = -1;
                        break;
                }
                    
                if (flag != -1)
                {
                    // --- On demande à l'utilisateur ce qu'il recherche
                    string[] filter = askOffresFilter();
                    string jsonOffres = "";
                    DateTime date = ParseDate(filter[1]);
                    int nbPersonne = int.Parse(filter[0]);
                    switch (hotelChoose)
                    {
                        case 1:
                            jsonOffres = ibisServices.GetOffres(date, nbPersonne);
                            break;
                        case 2:
                            jsonOffres = formule1Services.GetOffres(date, nbPersonne);
                            break;
                    }
                    List<Chambre> offres = JsonConvert.DeserializeObject<List<Chambre>>(jsonOffres);

                    // --- On affiche à l'utilisateur les offres qui correspondent
                    Chambre offreChoose;
                    while ((offreChoose = askOffre(offres)) != null)
                    {
                        Client client;
                        string reservationResponse = null;
                        offreChoose.Display();

                        // --- On demande les informations clients, si non fournis on annule la réservation.
                        client = askClientInformation();
                        if (client == null) ConsoleWriteLineWithFlag(3, "Annulation de la réservation...", true);
                        else
                        {
                            if (confirmReservation(offreChoose, client))
                            {
                                // TODO: offreChoose ID problème
                                Reservation reservation = new Reservation(ParseDate(filter[1]), ParseDate(filter[2]), int.Parse(filter[0]), client, offreChoose.Id);
                                String jsonReservation = JsonConvert.SerializeObject(reservation);
                                switch (hotelChoose)
                                {
                                    case 1:
                                        reservationResponse = ibisServices.MakeReservation(jsonReservation);
                                        break;
                                    case 2:
                                        reservationResponse = formule1Services.MakeReservation(jsonReservation);
                                        break;
                                }
                                if (reservationResponse == null) ConsoleWriteLineWithFlag(3, "Cette offre n'existe pas ou une erreur est survenue, veuillez réessayer.", true);
                                else
                                {
                                    ConsoleWriteLineWithFlag(0, reservationResponse, true);
                                    Console.WriteLine("\n(appuyer sur une touche pour revenir au choix d'hotel ou quitter l'application...)");
                                    Console.ReadLine();
                                    break;
                                }
                            }
                            else ConsoleWriteLineWithFlag(3, "Annulation de la réservation...", true);
                        }
                    }
                }
            }
        }

        static DateTime ParseDate(string date)
        {
            string[] dateSplit = date.Split('/');
            return new DateTime(int.Parse(dateSplit[2]), int.Parse(dateSplit[1]), int.Parse(dateSplit[0]));
        }

        static string[] HandleAuthentification(string agenceName)
        {
            string[] auth = new string[2];
            ConsoleWriteLineWithFlag(1, "Connexion via l'agence:\n", true);
            ConsoleWriteLineWithForegroundColor(ConsoleColor.Yellow, "TIPS: en vous connectant via l'agence vous aurez les meilleurs prix\n");

            Console.Write("\tLogin    :(" + agenceName + ") ");
            auth[0] = Console.ReadLine();


            Console.Write("\tPassword :(admin) ");
            auth[1] = Console.ReadLine();
            return auth;
        }

        static int askHotel(string[] hotels)
        {
            int choice;
            do
            {
                ConsoleWriteLineWithFlag(1, "Quel hotel souhaitez-vous consulter ?\n", true);

                int i = 0;
                foreach (string hotel in hotels)
                {
                    ConsoleWriteLineWithForegroundColor(ConsoleColor.Blue, "\n\nHotel#" + (i + 1) + " --------------");
                    Console.Write(hotel);
                    i++;
                }

                Console.Write("\n\nVotre choix: Hotel#");
                choice = int.Parse(Console.ReadLine());
            } while (choice < 1 || choice > hotels.Length);

            return choice;
        }

        static string[] askOffresFilter()
        {
            ConsoleWriteLineWithFlag(1, "Donner nous plus d'information sur les offres que vous recherchez\n\n", true);

            Console.Write("\tNombre de personnes (entre 1 et 5): ");
            string nbPersonne = Console.ReadLine();

            Console.Write("\tDate de début (jj/mm/aaaa)        : (choisir une date en mars 2021 ex: 10/03/2021) ");
            string dateDebut = Console.ReadLine();

            Console.Write("\tDate de fin (jj/mm/aaaa)          : ");
            string dateFin = Console.ReadLine();

            return new string[] { nbPersonne, dateDebut, dateFin };
        }

        static Chambre askOffre(List<Chambre> offres)
        {
            if (offres == null || offres.Equals(""))
            {
                ConsoleWriteLineWithFlag(2, "Nous sommes désolé mais aucune offre n'est disponible", false);
                return null;
            }

            ConsoleWriteLineWithForegroundColor(ConsoleColor.Blue, "\n\n\t# ------------------------------------ Offres disponibles ------------------------------------ #");
            
            int choice = -1;
            do
            {
                int i = 1;
                foreach (Chambre offre in offres)
                {
                    ConsoleWriteLineWithForegroundColor(ConsoleColor.Blue, "\n\nOffre#" + i + " --------------");
                    offre.Display();

                    Form1 form = new Form1(offre.ImageURL, " - Offre " + i + "\n", offre.ToString(), "");
                    form.ShowDialog();
                    i++;
                }

                ConsoleWriteLineWithForegroundColor(ConsoleColor.Red, "\n\n(utiliser 0 pour annuler et revenir en arrière)");
                Console.Write("Vous souhaitez réserver l'Offre#");
                choice = int.Parse(Console.ReadLine());
            } while (choice < 0 || choice > offres.Count);

            if (choice > 0)
            {
                return offres.ElementAt(choice - 1);
            }

            return null;
        }

        static Client askClientInformation()
        {
            Client client = new Client();
            ConsoleWriteLineWithFlag(1, "Veuillez saisir vos informations pour compléter la réservation: \n", true);
            ConsoleWriteLineWithForegroundColor(ConsoleColor.Red, "(pour annuler la réservation taper sur entrée)\n");
            
            Console.Write("\tVotre nom               : ");
            string nom = Console.ReadLine();
            if (nom.Equals("")) return null;
            else client.Nom = nom;
            
            Console.Write("\tVotre prénom            : ");
            client.Prenom = Console.ReadLine();
            
            Console.Write("\tVos coordonnée bancaire : (anything you want) ");
            client.CarteCredit = Console.ReadLine();

            return client;
        }

        static bool confirmReservation(Chambre offre, Client client)
        {
            Form1 form = new Form1(offre.ImageURL, "Récapitulatif de réservation: " + "\n", offre.ToString(), client.ToString());
            form.ShowDialog();

            ConsoleWriteLineWithFlag(2, "Confirmer la réservation ? (NON: 0, OUI: 1) ", true);

            return (int.Parse(Console.ReadLine()) == 1);
        }

        private static void ConsoleWriteLineWithForegroundColor(ConsoleColor color, String message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        //
        //Paramètres :
        //  flagType:
        //      0: [+]
        //      1: [~]
        //      2: [!]
        //      3: [-]
        private static void ConsoleWriteLineWithFlag(int flagType, string message, bool addForeground=false)
        {
            switch (flagType)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n\n[+] ");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\n\n[~] ");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n\n[!] ");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n\n[-] ");
                    break;
                default:
                    break;
            }
            if (addForeground)
            {
                Console.Write(message);
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
                Console.Write(message);
            }
        }
    }
}
