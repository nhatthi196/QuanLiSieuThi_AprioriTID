using AprioriTID.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.DAO
{
   public static class FindFI
    {
        public static Dictionary<string, List<Item>> D_Set;
        public static Dictionary<List<Item>, int> FinalFI;
        public static HashSet<Item> ItemSet = new HashSet<Item>();
        public static List<Step> steps;
        public static double MinSup;
        public static int TotalTransaction = 0;
       

        public static void WriteToFile(string s)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"G:\WriteLines1.txt", true))
            {
                file.WriteLine(s);
            }
        }
       
        public static List<List<Item>> SubSets(List<Item> subset, int n)
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

        public static bool GetData(double _minSup)
        {
            bool end = false;
            MinSup = _minSup;
            SqlCommand command;
            SqlDataReader reader;
            string sql = string.Format("exec [dbo].[SP_GIAOTAC] {0}", _minSup);
            command = new SqlCommand(sql, Connection.conn);
            reader  =  command.ExecuteReader();
            D_Set = new Dictionary<string, List<Item>>();
            ItemSet = new HashSet<Item>();
           
            TotalTransaction = 0;
            while (reader.Read())
            {
                TotalTransaction++;
                List<Item> set = new List<Item>();

                for (int i = 0; i < reader.FieldCount - 1; i++)
                {
                    
                        int id = ItemUtil.ConvertToId(reader.GetName(i + 1));
                        Item item = new Item(reader.GetName(i + 1), Int32.Parse(reader[i + 1].ToString()), id);
                        set.Add(item);
                        ItemSet.Add(item);
                    
                }
                D_Set.Add(reader[0].ToString(), set);
            }

            Constant.PageSize = D_Set.Count / Constant.pageRange;
            reader.Close();

            end = true;
            return end;

        }

        public static Dictionary<List<Item>, int> apriori_gen(Dictionary<List<Item>, int> l, int k)
        {
            Dictionary<List<Item>, int> c = new Dictionary<List<Item>, int>();
            if (k <= 2)
            {
                foreach (var item1 in l.Keys)
                {
                    foreach (var item2 in l.Keys)
                    {
                        if (CompareItemset(item1, item2) == false)
                        {
                            var key = item1.Union(item2).ToList();
                            if (IsContainKey(c, key) == false)
                            {
                                c.Add(key, 0);
                            }

                        }
                    }
                }
            }
            else
            {
                foreach (var item1 in l.Keys)
                {
                    foreach (var item2 in l.Keys)
                    {
                        var interset = item1.Intersect(item2).ToList();
                        if (interset.Count == k - 2)
                        {
                            var key = item1.Union(item2).ToList();
                            if (IsContainKey(c, key) == false)
                            {
                                var check = IsSubSetOfL(SubSets(key, k - 1), l);
                                if (check)
                                {
                                    c.Add(key, 0);
                                }
                            }
                        }
                    }
                }
            }
            return c;
        }

        private static bool IsSubSetOfL(List<List<Item>> set, Dictionary<List<Item>, int> L_Set)
        {
            bool check = false;
            if (set.Count <= L_Set.Keys.Count)
            {
                var interset = Intersect(L_Set.Keys.ToList(), set);
                if (interset.Count == set.Count)
                {
                    check = true;
                }
            }
            return check;
        }

        public static Dictionary<string, List<List<Item>>> Generate_F1(Dictionary<string, List <Item>> D_Set)
        {
            Dictionary<string, List<List<Item>>> f1 = new Dictionary<string, List<List<Item>>>();
            for (int i = 0; i < D_Set.Count; i++)
            {
                string key = D_Set.ElementAt(i).Key;
                List<List<Item>> items = new List<List<Item>>();
                foreach (var item in D_Set.ElementAt(i).Value)
                {
                    if (item.Value==1)
                    {
                        List<Item> set = new List<Item>();
                        set.Add(new Item(item));
                        items.Add(set);
                    }
                }

                f1.Add(key, items);
            }

            return f1;
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
        public static bool IsContainKey(Dictionary<List<Item>, int> c, List<Item> key)
        {
            bool check = false;
            foreach (var item in c.Keys)
            {
                if (CompareItemset(item, key))
                {
                    check = true;
                    break;
                }
            }
            return check;
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
        public static Dictionary<List<Item>, int> Generate_L1(Dictionary<string, List<List<Item>>> F1)
        {
            Dictionary<List<Item>, int> l1 = new Dictionary<List<Item>, int>();
            Dictionary<List<Item>, int> c1 = new Dictionary<List<Item>, int>();
            foreach (var f1 in F1)
            {
                foreach (var set in f1.Value)
                {
                    if (IsContainKey(c1, set))
                    {
                        foreach (var key in c1.Keys.ToList())
                        {
                            if (CompareItemset(key, set))
                            {
                                c1[key]++;

                            }
                        }
                    }
                    else
                    {
                        c1.Add(set, 1);
                    }
                }
            }
            foreach (var item in c1)
            {
                if (getMinSup(item.Value) >= MinSup)
                {
                    l1.Add(item.Key, item.Value);
                }
            }
            return l1;
        }
        public static void Init()
        {
            
           

            Dictionary<string, List<List<Item>>> F1 = Generate_F1(D_Set);
            Dictionary<List<Item>, int> L1 = Generate_L1(F1);
            steps = new List<Step>();
            steps.Add(new Step(F1, L1));
        }

        public static void Generate()
        {
            Dictionary<List<Item>, int> C_Set;
            Dictionary<string, List<List<Item>>> F_Set;
            Dictionary<List<Item>, int> L_Set;
            int k = 2;
            while (true)
            {

                F_Set = new Dictionary<string, List<List<Item>>>();
                L_Set = new Dictionary<List<Item>, int>();
                C_Set = apriori_gen(steps[k - 2].L_Set, k);

                foreach (var transaction in steps[k - 2].F_Set)
                {
                    var c = new List<List<Item>>();
                    for (var item1 = 0; item1 < transaction.Value.ToList().Count; item1++)
                    {
                        for (var item2 = item1; item2 < transaction.Value.ToList().Count; item2++)
                        {
                            var key = transaction.Value[item1].Union(transaction.Value[item2]).ToList();
                            if (IsContainKey(C_Set, key))
                            {
                                foreach (var item in C_Set.Keys.ToList())
                                {
                                    if (CompareItemset(item, key))
                                    {
                                        if (IsContain(c, item) == false)
                                        {
                                            C_Set[item]++;
                                            c.Add(item);
                                        }
                                    }
                                }
                            }

                        }
                    }

                    if (c.Count > 0)
                    {
                        F_Set.Add(transaction.Key, c);
                    }
                }

                foreach (var item in C_Set)
                {
                    if (getMinSup(item.Value) >= MinSup)
                    {
                        L_Set.Add(item.Key, item.Value);
                    }
                }
                if (L_Set.Count <= 0)
                    break;
                steps.Add(new Step(F_Set, L_Set));
                k++;
            }

            FinalFI = new Dictionary<List<Item>, int>();
            foreach(var step in steps)
            {
                foreach(var item in step.L_Set)
                {
                    FinalFI.Add(item.Key, item.Value);
                }
            }
        }
        public static int Run()
        {
           
            Init();
            Generate();
           
            return 1;
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            SqlCommand command;
            SqlDataReader reader;
            string sql = string.Format("select * from MATHANG");
            command = new SqlCommand(sql, Connection.conn);
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Product product = new Product(reader[0].ToString(),reader[1].ToString());
                products.Add(product);
            }
            
            reader.Close();
            return products;
        }

        public static List<Transaction> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            SqlCommand command;
            SqlDataReader reader;
            string sql = string.Format("select * from CTHD");
            command = new SqlCommand(sql, Connection.conn);
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Transaction transaction = new Transaction(reader[0].ToString(), reader[1].ToString(), Int32.Parse(reader[2].ToString()), Int32.Parse(reader[3].ToString()));
                transactions.Add(transaction);
            }
            reader.Close();
            return transactions;
        }

        public static double getMinSup(int value)
        {
            return (value * 100) / TotalTransaction;
        }
    }
}
