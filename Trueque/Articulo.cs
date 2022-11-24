using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trueque {
    internal class Articulo {
        public int Id { get; set; } 
        public string Descripcion { get; set; }
        public int ValorAprox { get; set; }
        public DateTime FechaIngreso { get; set; }

        //public List<string> Preferencias = new List<string>();
        public String Preferencias = "";
        public bool Intercambiado { get; set; }

        public Articulo(int id, string descrip, int valor ) { 
            Id = id;
            Descripcion = descrip;
            ValorAprox = valor;
            Preferencias = "";// new List<string>();        
            this.FechaIngreso= DateTime.Now;
            Intercambiado = false;
        }

        public string ToString()
        {
            string fecha_texto = string.Format("{0}/{1}/{2} {3}:{4}", FechaIngreso.Day.ToString("00"), FechaIngreso.Month.ToString("00"), FechaIngreso.Year.ToString("0000"), FechaIngreso.Hour.ToString("00"), FechaIngreso.Minute.ToString("00"));

            return this.Id.ToString() + "|" + this.Descripcion + "|" + this.ValorAprox.ToString() + "|" + fecha_texto + "|" + this.Preferencias ;
        }

    }
}
