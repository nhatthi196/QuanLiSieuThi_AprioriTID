using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
  public class Product
    {
        public String ID { get; set; }
        public String Name{get; set;}

        public Product(string _id, string _name)
        {
            ID = _id;
            Name = _name;
        }
    }
}
