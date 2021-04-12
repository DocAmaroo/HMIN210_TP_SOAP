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

        public Chambre(int numChambre, int nbPersonneMax, int nbLit, double prix, string imageURL, bool disp)
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

        public Chambre(int numChambre, int nbPersonneMax, int nbLit, double prix, string imageURL, bool disp, List<Reservation> reservations)
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
        public double Prix { get; set; }
        public string ImageURL { get; set; }
        public bool Disponible { get; set; }
        public List<Reservation> Reservations { get; set; }
        public void Display()
        {
            Console.WriteLine("Chambre n°" + this.NumChambre);
            Console.WriteLine("Nombre lits   : " + this.NbLit);
            Console.WriteLine("Prix          : " + this.Prix + "euros/nuit");
            Console.WriteLine("Disponible " + (this.Reservations.Count != 0 ? "le : " + this.Reservations.Last().DateDepart : "dès maintenant"));
        }
        public override string ToString()
        {
            return "Chambre n°" + this.NumChambre
            + "\nNombre lits   : " + this.NbLit
            + "\nPrix          : " + this.Prix + "euros/nuit"
            + "\nDisponible " + (this.Reservations.Count != 0 ? "le : " + this.Reservations.Last().DateDepart : "dès maintenant");
        }
    }
}
