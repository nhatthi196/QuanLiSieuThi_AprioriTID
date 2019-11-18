using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
   public class TransactionDetail
    {
        public string TID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }

        public TransactionDetail(string _tid, string _prodId, int _quantity, int _unit)
        {
            TID = _tid;
            ProductID = _prodId;
            Quantity = _quantity;
            Unit = _unit;
        }
    }
}
