using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice4_App
{
    public class Agence
    {
        public Agence(string nom, double pourcentage) 
        {
            this.Nom = nom;
            this.Pourcentage = pourcentage;
        }

        public string Nom { get; set; }

        public double Pourcentage { get; set; }
    }
}
