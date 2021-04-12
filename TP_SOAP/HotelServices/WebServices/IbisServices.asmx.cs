using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Exercice4_App;
using Newtonsoft.Json;

namespace HotelServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class IbisServices : System.Web.Services.WebService
    {
        private static Hotel hotel;
        private static string defaultPassword = "admin";
        private static bool isAuth = false;
        private static double tarifPropre = 1.00;

        public IbisServices() { }

        [WebMethod]
        public bool Authentification(string login, string password)
        {
            if (password.Equals(defaultPassword))
            {
                if (login.Equals("Trivaga") || login.Equals("admin"))
                {
                    isAuth = true;

                    if (login.Equals("Trivaga"))
                    {
                        tarifPropre = 0.98;
                    }

                    return isAuth;
                }
            }

            isAuth = false;
            return isAuth;
        }

        [WebMethod]
        public double GetTarifPropre()
        {
            return tarifPropre;
        }

        [WebMethod]
        public string GetOffres(DateTime date, int nbPersonne)
        {
            if (isAuth)
            {

                List<Chambre> offresFound = new List<Chambre>();

                foreach (Chambre chambre in hotel.Chambres)
                {
                    if (chambre.Disponible || (chambre.Reservations.Count != 0 && DateTime.Compare(chambre.Reservations.Last().DateDepart, date) <= 0))
                    {
                        if (chambre.NbPersonneMax >= nbPersonne) offresFound.Add(chambre);
                    }
                }

                return JsonConvert.SerializeObject(offresFound);
            }

            return null;
        }

        [WebMethod]
        public string GetOffre(int id)
        {
            foreach (Chambre chambre in hotel.Chambres)
            {
                if (chambre.Id.Equals(id))
                {
                    return JsonConvert.SerializeObject(chambre);
                }
            }
            return null;
        }

        [WebMethod]
        public string MakeReservation(string jsonReservation)
        {
            Reservation reservation = JsonConvert.DeserializeObject<Reservation>(jsonReservation);
            if (hotel.Chambres[reservation.ChambreId] == null) return null;

            Chambre chambre = hotel.Chambres[reservation.ChambreId];
            chambre.Disponible = false;
            chambre.Reservations.Add(reservation);
            return "[+] Réservation effectué avec succées!";
        }

        [WebMethod]
        public void GenerateHotel()
        {
            Client client = new Client("Toto", "Toto", "42069");
            Random rnd = new Random();

            Chambre c1 = new Chambre(1, 2, 2, 300, "https://www.usine-digitale.fr/mediatheque/3/9/8/000493893_homePageUne/hotel-c-o-q-paris.jpg", false);
            int day = rnd.Next(1, 31);
            c1.Reservations = new List<Reservation>() { new Reservation(new DateTime(2021, 3, day), new DateTime(2021, 3, day).AddDays(3), c1.NbPersonneMax, client, 1) };

            Chambre c2 = new Chambre(2, 3, 2, 400, "https://d397xw3titc834.cloudfront.net/images/original/2/b8/2b8e013e6c5fb747415b8a916eff7eb7.jpg", false);
            day = rnd.Next(1, 31);
            c2.Reservations = new List<Reservation>() { new Reservation(new DateTime(2021, 3, day), new DateTime(2021, 3, day).AddDays(3), c2.NbPersonneMax, client, 2) };

            Chambre c3 = new Chambre(3, 5, 5, 800, "http://www.furnotel.co.uk/wp-content/uploads/2015/11/furnotel-gold-and-black-boutique-hotel-suite-1024x683.jpg", false);
            day = rnd.Next(1, 31);
            c3.Reservations = new List<Reservation>() { new Reservation(new DateTime(2021, 3, day), new DateTime(2021, 3, day).AddDays(3), c3.NbPersonneMax, client, 3) };

            Chambre c4 = new Chambre(4, 3, 3, 250, "https://media.cntraveler.com/photos/53dabff3dcd5888e145ca051/4:3/w_480,c_limit/eccleston-square-hotel-london-england-2-113144.jpg", false);
            day = rnd.Next(1, 31);
            c4.Reservations = new List<Reservation>() { new Reservation(new DateTime(2021, 3, day), new DateTime(2021, 3, day).AddDays(3), c4.NbPersonneMax, client, 4) };

            Chambre c5 = new Chambre(5, 2, 1, 50, "https://cdn-image.travelandleisure.com/sites/default/files/styles/1600x1000/public/images/amexpub/0043/3588/201406-w-top-rated-hotel-beds-in-america-sofitel-new-york.jpg?itok=mmEvcf4W", false);
            day = rnd.Next(1, 31);
            c5.Reservations = new List<Reservation>() { new Reservation(new DateTime(2021, 3, day), new DateTime(2021, 3, day).AddDays(3), c5.NbPersonneMax, client, 5) };

            Chambre c6 = new Chambre(6, 4, 4, 250, "https://tse3.mm.bing.net/th?id=OIP.psw5wUf9RayO747hxsN05wAAAA&pid=Api", true);
            Chambre c7 = new Chambre(7, 3, 1, 100, "https://tse4.mm.bing.net/th?id=OIP.JgNjG3rpPEF2s4ekGYskqgAAAA&pid=Api", true);
            Chambre c8 = new Chambre(8, 4, 2, 150, "https://tse4.mm.bing.net/th?id=OIP.b6TDBFr8E0sNFDH5GJuxwQHaGP&pid=Api", true);
            Chambre c9 = new Chambre(9, 4, 3, 175, "https://tse2.mm.bing.net/th?id=OIP.9OAGkziLUQ85E0nY8ydEegAAAA&pid=Api", true);
            Chambre c10 = new Chambre(10, 5, 4, 200, "https://d397xw3titc834.cloudfront.net/images/original/2/31/231d85a09fb1959145b71f1408345f10.jpg", true);
            Chambre c11 = new Chambre(11, 5, 3, 250, "https://media-cdn.tripadvisor.com/media/photo-s/06/28/8c/9f/balladins-sarcelles.jpg", true);
            Chambre c12 = new Chambre(12, 2, 1, 60, "https://tse2.mm.bing.net/th?id=OIP.RFpDSVZNvh5qn8_cw5ZNSQHaHY&pid=Api", true);
            Chambre c13 = new Chambre(13, 1, 1, 70, "https://media.cntraveler.com/photos/53dabff3dcd5888e145ca051/master/w_1200,c_limit/eccleston-square-hotel-london-england-2-113144.jpg", true);
            Chambre c14 = new Chambre(14, 1, 1, 80, "https://businesstravellife.com/wp-content/uploads/2015/09/best-hotel-bed-business-travel-life.jpg", true);
            Chambre c15 = new Chambre(15, 2, 2, 200, "https://www.vendee-hotel-restaurant.com/wp-content/uploads/2014/10/IMG_9063-700x467.jpg", true);

            List<Chambre> chambres = new List<Chambre>() { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15 };

            List<Agence> agencesPartenaire = new List<Agence> { new Agence("Trivaga", 0.98) };
            hotel = new Hotel("Palace Hotel", "France", "Montpellier", "69 rue des lavandiers", "", "49.726x3.648", 2, chambres, agencesPartenaire);
        }
    }
}
