using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trueque {
    internal class Persona {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NumeroContacto { get; set; }
        public List<int> ArticulosAsociados = new List<int>();
        public Persona(int id, string nom, int contacto) { 
            Id= id;
            Nombre= nom;
            NumeroContacto= contacto;
        }
        
        public string ToString()
        {
            return this.Id.ToString()+ "|"+this.Nombre+"|"+this.NumeroContacto.ToString() ;
        }
    }
}
