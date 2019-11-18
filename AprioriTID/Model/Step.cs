using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
  public class Step
    {

        public Dictionary<List<Item>, int> L_Set { get; set; }
        public Dictionary<string, List<List<Item>>> F_Set { get; set; }
        public Step(Dictionary<string, List<List<Item>>> f, Dictionary<List<Item>, int> l)
        {
            this.F_Set = f;
            this.L_Set = l;
        }
    }
}
