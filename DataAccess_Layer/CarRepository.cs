using DataAccess;
using DataBase_Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace DataAccess
{
    public class InfoRepository
    {
        private readonly string connectionString;

        public InfoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<DataCars> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var c = new List<DataCars>();

                try
                {
                    var command = new SqlCommand("GetCarsInfo", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var data_of_car = new DataCars();
                            data_of_car.Id = reader.GetInt32(0);
                            data_of_car.Name = reader.GetString(1);
                            data_of_car.Year = reader.GetDateTime(2);
                            data_of_car.Country = reader.GetString(3);
                            data_of_car.Cost = reader.GetInt32(4);
                            data_of_car.Garanty_Mounthes = reader.GetString(5);

                            c.Add(data_of_car);
                        }
                    }
                    reader.Close();

                    return c;
                }
               
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}