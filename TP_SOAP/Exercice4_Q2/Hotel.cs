using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice4_App
{
    public class Hotel
    {
        private static int compteur = 0;

        public Hotel(string nom, string pays, string ville, string rue, string lieuDit, string positionGPS, int nbEtoile, List<Chambre> chambres, List<Agence> agencesPartenaire)
        {
            compteur = compteur + 1;
            this.Id = compteur;
            this.Nom = nom;
            this.Pays = pays;
            this.Ville = ville;
            this.Rue = rue;
            this.LieuDit = lieuDit;
            this.PositionGPS = positionGPS;
            this.NbEtoile = nbEtoile;
            this.Chambres = chambres;
            this.AgencesPartenaire = agencesPartenaire;
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }
        public string Rue { get; set; }
        public string LieuDit { get; set; }
        public string PositionGPS { get; set; }
        public int NbEtoile { get; set; }
        public List<Chambre> Chambres { get; set; }
        public List<Agence> AgencesPartenaire { get; set; }

        public string FullAdresseToString()
        {
            return this.Rue + ", " + this.LieuDit + ", " + this.Ville + ", " + this.Pays;
        }
        public override string ToString()
        {
            return "Nom     : " + this.Nom
                + "\nAdresse : " + this.FullAdresseToString()
                + "\nGPS     : " + this.PositionGPS
                + "\nEtoiles : " + this.NbEtoile +"*";
        }
    }
}
