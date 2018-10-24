using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSelector.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public short Year { get; set; }
        public decimal Price { get; set; }
        public float TCC { get; set; }
        public float MPG { get; set; }
    }
}
