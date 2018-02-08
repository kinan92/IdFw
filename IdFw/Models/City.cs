using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdFw.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Country Countries { get; set; }
       
        public virtual List<Person> People { get; set; }
    }
}