using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdFw.Models
{
   
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual City City { get; set; }

    }
}