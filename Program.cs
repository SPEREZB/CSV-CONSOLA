// See https://aka.ms/new-console-template for more information 
using System.Data.SqlClient; 
using CsvHelper;
using CsvHelper.Configuration;


namespace CSV_CONSOLA
{
    class Program
    {
        static void Main(string[] args)
        {
            //OBJETO CONEXION
            Conexion conexion = new Conexion(); 
            //POR MEDIO DE ESTE OBJ ACCEDEREMOS A LAS PROPIEDADES DE LA CADENA DE CONEXION
            //Y A LA RUTA DEL ARCHIVO CSV
        
              
            try
            {
                // Configuración de CsvHelper
                var csvConfiguration = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,  
                    Delimiter = ",",  
                    MissingFieldFound = null,  
                    HeaderValidated = null  
                };


                // Leer el archivo CSV
                using (var reader = new StreamReader(conexion.RutaArchivo))
                using (var csv = new CsvReader(reader, csvConfiguration))
                {
                    var records = csv.GetRecords<CSVData>(); // Cambiar CSVData por la clase que representa la estructura del archivo CSV

                    // Conectar a la base de datos
                    using (SqlConnection connection = new SqlConnection(conexion.CadenaConexion))
                    {
                        connection.Open();

                        // Iterar a través de los registros del archivo CSV y guardarlos en la base de datos
                        foreach (var record in records)
                        {
                            // Ejecutar consulta SQL para insertar en la base de datos
                            using (SqlCommand command = new SqlCommand("INSERT INTO CSV (Id, Name, isregistered) VALUES (@Id, @Name, @IsRegistered)", connection))
                            {
                                command.Parameters.AddWithValue("@Id", record.Id);
                                command.Parameters.AddWithValue("@Name", record.Name);
                                command.Parameters.AddWithValue("@IsRegistered", DBNull.Value); // Establecer como valor nulo para isregistered

                                command.ExecuteNonQuery();
                            }
                        }

                        connection.Close();
                    }
                }

                Console.WriteLine("Datos insertados en la base de datos SQL Server exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    // Clase para representar la estructura del archivo CSV
    class CSVData
    {
        public int Id { get; set; }
        public string Name { get; set; }
         
    }
     
}