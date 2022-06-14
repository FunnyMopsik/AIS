using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Furniture
{
    public class Furniture
    {
        [Key]
        public int Furniture_number { get; set; }
        public string Furniture_Name { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
        public Type Type { get; set; }
    }
}
