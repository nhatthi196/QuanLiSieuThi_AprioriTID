using AprioriTID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.DAO
{
    public static class ItemUtil 
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

        public static bool CompareItemset(List<Item> itemset1, List<Item> itemset2)
        {
            bool check = true;
            if (itemset1.Count == itemset2.Count)
            {
                var interset = itemset1.Intersect(itemset2).ToList();
                if (interset.Count != itemset1.Count && interset.Count != itemset2.Count)
                    check = false;
            }
            else
                check = false;
            return check;
        }



        public static List<List<Item>> GetSubSets(List<Item> subset, int n)
        {
            List<List<Item>> result = new List<List<Item>>();
            int[] b = new int[100];
            Item[] a = new Item[n];
            for (int i = 0; i < subset.Count; i++)
            {
                b[subset[i].Id] = 1;
            }
            backtrack(1, b, subset, n, result, a);
            return result;
        }

        public static void backtrack(int k, int[] b, List<Item> set, int n, List<List<Item>> subsets, Item[] a)
        { // hàm quay lui

            List<Item> subsetTmp;
            int max = set.Max(x => x.Id);
            for (int j = 1; j <= max; j++)
            {
                if (b[j] == 1)
                {
                    a[k - 1] = set.Find(x => x.Id == j);
                    b[j] = 0;
                    if (k == n)
                    {
                        subsetTmp = new List<Item>(a);
                        if (IsContain(subsets, subsetTmp) == false)
                        {

                            subsets.Add(subsetTmp);
                        }

                    }
                    else
                        backtrack((k + 1), b, set, n, subsets, a);
                    b[j] = 1;
                }
            }
        }

        public static List<List<Item>> Intersect(List<List<Item>> a, List<List<Item>> b)
        {
            List<List<Item>> interset = new List<List<Item>>();
            foreach (var item1 in a)
            {
                foreach (var item2 in b)
                {
                    if (CompareItemset(item2, item1))
                    {
                        interset.Add(item2);
                        break;
                    }
                }
            }
            return interset;
        }

        public static bool IsContain(List<List<Item>> c, List<Item> key)
        {
            bool check = false;
            foreach (var item in c)
            {
                if (CompareItemset(item, key))
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        public static List<Item> ConvsertStringToItemSet(string s)
        {
            List<Item> itemset = new List<Item>();

            string[] tmp = s.Split(' ');
            for(int i = 1;i<=tmp.Length-2;i++)
            {
                var id = Int32.Parse(tmp[i]);
                var item = getItemById(id);
                itemset.Add(item);
            }
            return itemset;
        }

        public static Item getItemById(int _id)
        {
            return FindFI.ItemSet.ToList().Find(x=>x.Id==_id);
        }
    }
}
