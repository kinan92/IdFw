using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdFw.Models
{
    public class ViewModelPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public City City { get; set; }

        public List<Country> country { get; set; }
    }
}