using AprioriTID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.DAO
{
    public static class FindLaw
    {
        public static List<Law> AllLaws;
        public static List<LawsByItemSet> lawByItemSet;
        public static double minConf=0;
        public static void Generate(double _minConf)
        {
            minConf = _minConf;
            AllLaws = new List<Law>();
            lawByItemSet = new List<LawsByItemSet>();
            foreach (var itemset in FindFI.FinalFI.Keys)
            {
                if(itemset.Count>=2)
                { 
                var subset = findSubset(itemset);
                
                var tmplaws = new List<Law>();
                foreach (var item in subset)
                {
                        
                    foreach (var item2 in subset)
                    {
                        if (item.Intersect(item2).ToList().Count <= 0)
                        {
                                var law = new Law();
                                law.X = item;
                                law.Y = item2;

                            if (law.CalculateConf() >= minConf)
                            {
                                tmplaws.Add(law);
                                if (AllLaws.Contains(law) == false)
                                 {
                                    AllLaws.Add(law);
                                }
                            }
                        }
                    }
                }

                lawByItemSet.Add(new LawsByItemSet(itemset, tmplaws));
            }
            }
        }

        public static List<List<Item>> findSubset(List<Item> itemset)
        {
            List<List<Item>> subsets = new List<List<Item>>();
            for(int i = 1;i<itemset.Count;i++)
            {
                 Union(subsets,ItemUtil.GetSubSets(itemset,i));
            }
            return subsets;
        }

        public static void Union(List<List<Item>> dest, List<List<Item>> source)
        {
            foreach(var itemset in source)
            {
                dest.Add(itemset);
            }
        }

        public static List<Law>  getLawsByItemSet(List<Item> _itemset)
        {
            var laws = new List<Law>();
            foreach(var item in lawByItemSet)
            {
                if(ItemUtil.CompareItemset(item.ItemSet,_itemset)==true)
                {
                    laws = item.Laws;
                    break;
                }
            }
            return laws;
        }
        

    }
}
