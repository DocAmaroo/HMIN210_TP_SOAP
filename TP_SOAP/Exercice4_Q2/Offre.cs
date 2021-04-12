using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice4_App
{
    public class Offre
    {

        public Offre() { }

        public Offre(int id, Chambre chambre, DateTime dateDisponibilite, double prix, string imageURL, bool disponible)
        {
            this.Id = id;
            this.Chambre = chambre;
            this.DateDisponibilite = dateDisponibilite;
            this.Prix = prix;
            this.ImageURL = imageURL;
            this.Disponible = disponible;
        }

        public int Id { get; set; }
        public Chambre Chambre { get; set; }
        public DateTime DateDisponibilite { get; set; }
        public double Prix { get; set; }
        public bool Disponible { get; set; }
        public string ImageURL { get; set; }
    }
}
