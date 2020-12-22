using DataAccess.Repositories;

namespace ServiceLayer
{
    public class InfoCar
    {
        public InfoRepository carRepository { get; set; }
        public InfoCar(string connectionString)
        {
            carRepository = new InfoRepository(connectionString);
        }
    }
}