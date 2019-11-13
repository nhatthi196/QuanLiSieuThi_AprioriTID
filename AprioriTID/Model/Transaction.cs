using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
   public class Transaction
    {
        public string TID { get; set; }
        public string CustomerID { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }

        public Transaction(string _tid, string _cusId, int _quantity, int _unit)
        {
            TID = _tid;
            CustomerID = _cusId;
            Quantity = _quantity;
            Unit = _unit;
        }
    }
}
