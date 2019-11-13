using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
  public class Step
    {

        public Dictionary<List<I>, int> L_Set { get; set; }
        public Dictionary<string, List<List<I>>> F_Set { get; set; }
        public Step(Dictionary<string, List<List<I>>> f, Dictionary<List<I>, int> l)
        {
            this.F_Set = f;
            this.L_Set = l;
        }
    }
}
