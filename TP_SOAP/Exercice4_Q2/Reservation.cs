using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice4_App
{
    public class Reservation
    {
        private static int compteur = 0;

        public Reservation(DateTime dateArrivee, DateTime dateDepart, int nbPersonne, Client client, int chambreId)
        {
            compteur++;
            this.Id = compteur;
            this.DateArrivee = dateArrivee;
            this.DateDepart = dateDepart;
            this.NbPersonne = nbPersonne;
            this.Client = client;
            this.ChambreId = chambreId;
        }

        public int Id { get; set; }
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }
        public int NbPersonne { get; set; }
        public Client Client { get; set; }
        public int ChambreId { get; set; }
    }
}
