using AprioriTID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.DAO
{
    public static class ItemCovnert 
    {
        public static string Display(List<List<I>> itemset)
        {
            string s = "";
            foreach(var item in itemset)
            {
                s += DisplayItem(item) + "  ";
            }   

           
            return s;
        }
        public static string DisplayItem(List<I> item)
        {
            string s = "";
            s += "{ ";
            foreach (var i in item)
            {
                s +=  i.Id + " ";
            }
            s += "}";
            return s;
        }
    }
}
