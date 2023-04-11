using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_CONSOLA
{
    internal class Conexion
    {
        //Atributos
        private string rutaArchivo;
        private string cadenaConexion;


        //Propiedades
        public string RutaArchivo
        {
            get { return rutaArchivo; }
            set { rutaArchivo = value; }
        }

 
        public string CadenaConexion
        {
            get { return cadenaConexion; }
            set { cadenaConexion = value; }
        }

        //Constructor
        public Conexion()
        {
            rutaArchivo = @"C:\Users\user\Desktop\CSV\datos.csv";
            cadenaConexion = "Data Source=localhost;Initial Catalog=BDCSV;Integrated Security=True"; 
        }
    }
}
