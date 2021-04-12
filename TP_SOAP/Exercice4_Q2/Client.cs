using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice4_App
{
    public class Client
    {
        private static int compteur = 0;

        public Client()
        {
            compteur = compteur + 1;
            this.Id = compteur;
        }

        public Client(String nom, String prenom, String carteCredit)
        {
            compteur = compteur + 1;
            this.Id = compteur;
            this.Nom = nom;
            this.Prenom = prenom;
            this.CarteCredit = carteCredit;
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string CarteCredit { get; set; }
        public int Compteur { get; set; }

        public string[] toArrString()
        {
            return new string[3] { this.Nom, this.Prenom, this.CarteCredit };
        }

        public override String ToString()
        {
            return
                "\tNom                    : " + this.Nom
                + "\nPrénom               : " + this.Prenom
                + "\nCoordonées bancaires : " + this.CarteCredit;
        }
    }
}
