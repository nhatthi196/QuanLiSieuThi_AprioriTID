using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
    public class ItemSet<I> : List<I>
    {
        public ItemSet(I[] item): base(item) 
        {
            
        }
        public ItemSet(): base() { }
        public override string ToString()
        {
            string s = "";

            return s;
        }
    }
}
