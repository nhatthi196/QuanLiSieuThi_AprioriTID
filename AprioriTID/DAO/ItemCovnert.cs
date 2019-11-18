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
        public static string Display(List<List<Item>> itemset)
        {
            string s = "";
            foreach(var item in itemset)
            {
                s += DisplayItem(item) + "  ";
            }   

           
            return s;
        }
        public static string DisplayItem(List<Item> item)
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

        public static int ConvertToId(string id)
        {
            var tmp = id.Split('H');
            var result = Int32.Parse(tmp[1]);
            return result;
        }
    }
}
