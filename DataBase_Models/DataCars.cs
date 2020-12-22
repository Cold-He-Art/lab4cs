
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase_Models
{
    public class DataCars
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Year { get; set; }
        public string Country { get; set; }
        public int Cost { get; set; }
        public string Garanty_Mounthes { get; set; }
    }
}