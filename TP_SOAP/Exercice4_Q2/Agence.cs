using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice4_App
{
    public class Agence
    {
        public Agence(string nom, double tarifPropre) 
        {
            this.Nom = nom;
            this.Prix = tarifPropre;
        }

        public string Nom { get; set; }

        public double Prix { get; set; }
    }
}
