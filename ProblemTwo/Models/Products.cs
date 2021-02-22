using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProblemTwo.Models
{
    public class Products
    {
        public int idProduct { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
    }
}
