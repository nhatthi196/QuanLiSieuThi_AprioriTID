using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AprioriTID.DAO;
namespace AprioriTID.Model
{
   public class Law
    {
        public List<Item> X { get; set; }
        public List<Item> Y { get; set; }

        public double Conf { get; set; }

       public Law() {  }
        public Law(List<Item> _x, List<Item> _y, double _conf)
        {
            this.X = _x;
            this.Y = _y;
            this.Conf = _conf;
        }
        public override string ToString()
        {
           
            return ItemUtil.DisplayItem(this.X.ToList()) + " => " + ItemUtil.DisplayItem(this.Y.ToList());
            
        }

        public double CalculateConf()
        {
            double result = 0;

            int supX;
            int supXY;

            supX = FindSupport(X);
            supXY = FindSupport(X.Union(Y).ToList());

            result = (supXY*100 / supX);

            this.Conf = result;
            return result;
        }

        public int FindSupport(List<Item> items)
        {
            int result = 0;

            foreach(var itemset in FindFI.FinalFI)
            {
                if(ItemUtil.CompareItemset(itemset.Key,items.ToList()))
                {
                    result = itemset.Value;
                    break;
                }
            }

            return result;
        }

        

        public override bool Equals(object obj)
        {
            var other = obj as Law;
            if (other == null) return false;

            if (ItemUtil.CompareItemset(X, other.X) == true && ItemUtil.CompareItemset(Y, other.Y) == true)
                return true;
            else
                return false;
        }
    }
}
