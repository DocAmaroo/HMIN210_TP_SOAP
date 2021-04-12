using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice4_App
{
    public class Chambre
    {
        public Chambre () { }

        public Chambre(int numChambre, int nbPersonneMax, int nbLit, int prix, string imageURL, bool disp)
        {
            this.Id = numChambre - 1;
            this.NumChambre = numChambre;
            this.NbLit = nbLit;
            this.Prix = prix;
            this.NbPersonneMax = nbPersonneMax;
            this.ImageURL = imageURL;
            this.Disponible = disp;
            this.Reservations = new List<Reservation>();
        }

        public Chambre(int numChambre, int nbPersonneMax, int nbLit, int prix, string imageURL, bool disp, List<Reservation> reservations)
        {
            this.Id = numChambre - 1;
            this.NumChambre = numChambre;
            this.NbLit = nbLit;
            this.Prix = prix;
            this.NbPersonneMax = nbPersonneMax;
            this.ImageURL = imageURL;
            this.Disponible = disp;
            this.Reservations = reservations;
        }

        public int Id { get; set; }
        public int NumChambre { get; set; }
        public int NbPersonneMax { get; set; }
        public int NbLit { get; set; }
        public int Prix { get; set; }
        public string ImageURL { get; set; }
        public bool Disponible { get; set; }
        public List<Reservation> Reservations { get; set; }
        public void Display()
        {
            Console.WriteLine("\tChambre n°" + this.NumChambre);
            Console.WriteLine("\tNombre lits   : " + this.NbLit);
            Console.WriteLine("\tPrix          : " + this.Prix + "euros/nuit");
            Console.WriteLine("\tDisponible " + (this.Reservations.Count != 0 ? "le :" + this.Reservations.Last().DateDepart : "dès maintenant"));
        }
        public override string ToString()
        {
            return "\tChambre n°" + this.NumChambre
            + "\n\tNombre lits   : " + this.NbLit
            + "\n\tPrix          : " + this.Prix + "euros/nuit"
            + "\n\tDisponible " + (this.Reservations.Count != 0 ? "le :" + this.Reservations.Last().DateDepart : "dès maintenant");
        }
    }
}
