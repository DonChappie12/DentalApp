using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalWebApi.Models
{
    public class Precsription
    {
        public int Id { get; set;}
        public string PrecsriptionName { get; set; }
        public string PrecsriptionDosage { get; set;}
    }
}