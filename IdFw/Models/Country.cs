using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdFw.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

       public virtual List<City>  Cities { get; set; }
        public virtual List< Person> Persons { get; set; }

    }
}