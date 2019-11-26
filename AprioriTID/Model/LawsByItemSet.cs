using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
  public  class LawsByItemSet
    {
       public List<Item> ItemSet { get; set; }
        public List<Law>  Laws { get; set; }

        public LawsByItemSet() {  }
        public LawsByItemSet(List<Item> _itemset, List<Law> _laws)
        {
            ItemSet = _itemset;
            Laws = _laws;
        }
    }
}
