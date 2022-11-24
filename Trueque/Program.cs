using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Trueque {
    internal class Program {
        //public static Propiedades GLOBALES = new Propiedades();
        public static List<Persona> lasPersonas = new List<Persona>();
        public static List<Articulo> losArticulos = new List<Articulo>();
        static void Main(string[] args) {
            //Validar productos existentes (listar)
            //Ingresar un artículo /persona
            //Buscar artículo
            //consultar articulos coincidentes con el artículo buscado
            //Consultar artículos de mucho tiempo -> Crear ofertas de artículos
            //cambiar estado objeto intercambiado

            MENU_PRINCIPAL();

        }

        static void MENU_PRINCIPAL()
        {
            string opcion = "";
            do
            {
                //Console.Clear();
                Console.WriteLine("Ingrese una opción para continuar:");
                Console.WriteLine("\t 0:\t Salir");
                Console.WriteLine("\t 1:\t Ingresar una persona");
                Console.WriteLine("\t 2:\t Ingresar un artículo");
                Console.WriteLine("\t 3:\t Listar personas");
                Console.WriteLine("\t 4:\t Listar artículos");
                Console.WriteLine("\t 5:\t Consultar artículos antigüos");
                Console.WriteLine("\t 6:\t Buscar artículos por nombre");
                opcion = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine("");
                //Console.Clear();
                switch (opcion)
                {
                    case "0":
                        //Console.WriteLine("Saliendo");
                        break;
                    case "1":
                        IngresarPersona();
                        break;
                    case "2":
                        IngresarArticulo();
                        break;
                    case "3":
                        LeerPersonas();
                        break;
                    case "4":
                        LeerArticulos(true);
                        break;
                    case "5":
                        ConsultarAntiguos();
                        break;
                    case "6":
                        Buscar();
                        break;
                    //case "a":
                    //    Console.WriteLine("Mayor id de artículo: "+LeerMayorIdArticulo().ToString());
                    //    break;
                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }
                if (opcion != "0")
                {
                    Console.WriteLine("\t Presione una tecla para continuar");
                    opcion = Console.ReadKey().KeyChar.ToString();
                }
                
                Console.Clear();
            } while (opcion != "0");
        }


        public static void IngresarPersona() {
            int id_persona = 0;
            string nombre_persona = "";
            int numero_persona = 0;

            Boolean DatosCorrectos= false;
            while(DatosCorrectos == false)
            {
                try
                {
                    Console.WriteLine("Ingrese id de la persona");
                    id_persona=int.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese nombre de la persona");
                    nombre_persona = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese número de contacto de la persona");
                    numero_persona = int.Parse(Console.ReadLine());
                    DatosCorrectos = true;
                }
                catch
                {
                    Console.WriteLine("Por favor ingrese datos correctos");
                }
            }
            
            

            Persona x=new Persona(id_persona, nombre_persona,numero_persona);

            if (true) //escribir en archivo
            {
                string ruta = "BasePersonas.log";
                StreamWriter sw = new StreamWriter(ruta,true);
                try
                {
                    sw.WriteLine(id_persona.ToString() + "|" + nombre_persona + "|" + numero_persona.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error ->" + ex.ToString());
                }
                finally
                {
                    sw.Dispose();
                    sw.Close();
                }
            }

            if (false)
            {
                try
                {
                    string ruta = Environment.CurrentDirectory + "/BasePersonas.log";
                    System.Diagnostics.Process.Start(ruta);
                }
                catch (Exception ex)
                {

                }
            }
            

        }

        public static void IngresarArticulo()
        {
            int id_persona = 0;

            int id_articulo = 0;
            string descripcion_articulo = "";
            int valor_articulo = 0;

            //List<string> lista_preferencias = new List<string>();
            string preferencias = "";

            Boolean DatosCorrectos = false;
            while (DatosCorrectos == false)
            {
                try
                {
                    id_articulo = LeerMayorIdArticulo() + 1;
                    Console.WriteLine("Ingresando artículo: "+id_articulo.ToString());
                    Console.WriteLine("Ingrese id del dueño del artículo");
                    id_persona = int.Parse(Console.ReadLine());
                    //Console.WriteLine("Ingrese id del artículo");
                    //id_articulo = int.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese descripción del artículo");
                    descripcion_articulo = Console.ReadLine();
                    Console.WriteLine("Ingrese valor aproximado del artículo");
                    valor_articulo = int.Parse(Console.ReadLine());

                    Boolean cantidad_articulos_minima = false;
                    while (cantidad_articulos_minima==false)
                    {
                        Console.WriteLine("Ingrese al menos 3 artículos separados por \";\" como preferencias de trueque");
                        preferencias = Console.ReadLine();
                        if (preferencias.Count() - preferencias.Replace(";", "").Count() > 1)
                        {
                            cantidad_articulos_minima = true;
                        }
                    }

                    DatosCorrectos = true;
                }
                catch
                {
                    Console.WriteLine("Por favor ingrese datos correctos");
                }
            }



            Articulo x = new Articulo(id_articulo,descripcion_articulo,valor_articulo);

            if (true) //escribir en archivo
            {
                string ruta = "BaseArticulos.log";
                StreamWriter sw = new StreamWriter(ruta, true);
                try
                {
                    string fecha_texto = string.Format("{0}/{1}/{2} {3}:{4}",x.FechaIngreso.Day.ToString("00"),x.FechaIngreso.Month.ToString("00"), x.FechaIngreso.Year.ToString("0000"), x.FechaIngreso.Hour.ToString("00"), x.FechaIngreso.Minute.ToString("00"));
                    sw.WriteLine(id_articulo.ToString() + "|" + descripcion_articulo + "|" + valor_articulo.ToString()+"|"+fecha_texto+"|"+preferencias);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error ->" + ex.ToString());
                }
                finally
                {
                    sw.Dispose();
                    sw.Close();
                }
            }

            if (false)
            {
                try
                {
                    string ruta = Environment.CurrentDirectory + "/BaseArticulos.log";
                    System.Diagnostics.Process.Start(ruta);
                }
                catch (Exception ex)
                {

                }
            }
           

        }


        public static void LeerPersonas()
        {
            string ruta = "BasePersonas.log";
            lasPersonas.Clear();
            using (StreamReader sr = new StreamReader(ruta))
            {
                try
                {
                    string linea;

                    linea = sr.ReadLine();
                    do
                    {
                        string[] campos = linea.Split('|');

                        int id_persona = int.Parse(campos[0]);
                        string nombre_persona = campos[1];
                        int numero_persona = int.Parse(campos[2]);

                        Persona P = new Persona(id_persona, nombre_persona, numero_persona);
                        lasPersonas.Add(P);
                        Console.WriteLine(P.ToString());
                        //Console.WriteLine("Rut : " + campos[0] + ", Nombre : " + campos[1] + ", Correo : " + campos[2] + ", Sueldo : " + campos[3]);
                        linea = sr.ReadLine();
                    } while (linea != null);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error ->" + ex.ToString());
                }
                finally
                {
                    sr.Dispose();
                    sr.Close();
                }
            }
        }


        public static void LeerArticulos(bool imprimir)
        {
            string ruta = "BaseArticulos.log";
            losArticulos.Clear();
            using (StreamReader sr = new StreamReader(ruta))
            {
                try
                {
                    string linea;

                    linea = sr.ReadLine();
                    do
                    {
                        string[] campos = linea.Split('|');

                        int id_articulo = int.Parse(campos[0]);
                        string descripcion_articulo = campos[1];
                        int valor_articulo = int.Parse(campos[2]);
                        string fecha_string = campos[3];
                        string preferencias = campos[4];
                        DateTime fecha = new DateTime(
                            int.Parse(fecha_string.Substring(6,4)),
                            int.Parse(fecha_string.Substring(3,2)),
                            int.Parse(fecha_string.Substring(0, 2)),
                            int.Parse(fecha_string.Substring(11,2)),
                            int.Parse(fecha_string.Substring(14, 2)),
                            0
                            );

                        Articulo A = new Articulo(id_articulo, descripcion_articulo, valor_articulo);
                        A.Preferencias = preferencias;
                        A.FechaIngreso = fecha;
                        losArticulos.Add(A);
                        if (imprimir)
                        {
                            Console.WriteLine(A.ToString());
                        }
                        
                        //Console.WriteLine("Rut : " + campos[0] + ", Nombre : " + campos[1] + ", Correo : " + campos[2] + ", Sueldo : " + campos[3]);
                        linea = sr.ReadLine();
                    } while (linea != null);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error ->" + ex.ToString());
                }
                finally
                {
                    sr.Dispose();
                    sr.Close();
                }
            }
        }

        public static int LeerMayorIdArticulo()
        {
            int resultado = 0;
            List<int> ids = new List<int>();
            List<string> rutas = new List<string>();
            rutas.Add("BaseArticulos.log");

            foreach (String ruta in rutas)
            {
                if (File.Exists(ruta))
                {
                    using (StreamReader sr = new StreamReader(ruta))
                    {

                        try
                        {
                            string textocompleto = sr.ReadToEnd();
                            List<string> lineas = textocompleto.Split('\n').ToList<string>();
                            List<string> lineas2 = (from x in lineas where x.Trim() != "" select x.Split('|')[0]).ToList();
                            ids.AddRange((from x in lineas2 select int.Parse(x)).ToList());

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error ->" + ex.ToString());
                        }
                        finally
                        {
                            sr.Dispose();
                            sr.Close();
                        }
                    }
                }
            }
            if (ids.Count > 0)
            {
                int mayorid = (from x in ids select x).Max();
                if (mayorid > 0){
                    resultado = mayorid;
                }
            }
            
            
            
            return resultado;
        }


        public static void ConsultarAntiguos()
        {
            int cantidad_dias = 3;
            DateTime hoy = DateTime.Now;
            DateTime antiguo = hoy.AddDays(-1 * cantidad_dias);
            //if (losArticulos.Count == 0)
            //{
                LeerArticulos(false);
            //}
            Console.WriteLine("Artículos antigüos:");
            List<Articulo> antiguos = (from x in losArticulos where x.FechaIngreso < antiguo select x).OrderBy(x=>x.FechaIngreso).ToList();
            foreach(Articulo A in antiguos)
            {
                Console.WriteLine(A.ToString());
            }
            
        }


        public static void Buscar()
        {
            LeerArticulos(false);
            Console.WriteLine("Ingrese un texto a buscar en la descripción de los artículos");
            string texto = Console.ReadLine().Trim();
            List<Articulo> listado = (from x in losArticulos where x.Descripcion.ToLower().Contains(texto.ToLower()) select x).ToList();
            foreach(Articulo A in listado)
            {
                Console.WriteLine(A.ToString());
            }
        }



    }
}
