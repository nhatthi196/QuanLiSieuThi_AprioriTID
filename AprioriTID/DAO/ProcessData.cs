using AprioriTID.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.DAO
{
   public static class ProcessData
    {
        public static Dictionary<string, List<I>> D_Set;
        public static Dictionary<List<I>, int> FinalFI;
        public static HashSet<I> ItemSet = new HashSet<I>();
        public static List<Step> steps;
        public static int MinSup;
        public static int TotalTransaction = 0;
       

        public static void WriteToFile(string s)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"G:\WriteLines1.txt", true))
            {
                file.WriteLine(s);
            }
        }
        public static string Print(I[] a)
        {
            string s = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != null)
                {
                    s += a[i].Item + " ";
                }
            }
            return s;
        }
        public static List<List<I>> SubSets(List<I> subset, int n)
        {
            List<List<I>> result = new List<List<I>>();
            int[] b = new int[100];
            I[] a = new I[n];
            for (int i = 0; i < subset.Count; i++)
            {
                b[subset[i].Id] = 1;
            }
            backtrack(1, b, subset, n, result, a);
            return result;
        }
        public static List<List<I>> Intersect(List<List<I>> a, List<List<I>> b)
        {
            List<List<I>> interset = new List<List<I>>();
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

        public static void backtrack(int k, int[] b, List<I> set, int n, List<List<I>> subsets, I[] a)
        { // hàm quay lui

            List<I> subsetTmp;
            int max = set.Max(x => x.Id);
            for (int j = 1; j <= max; j++)
            {
                if (b[j] == 1)
                {
                    a[k - 1] = set.Find(x => x.Id == j);
                    b[j] = 0;
                    if (k == n)
                    {
                        subsetTmp = new List<I>(a);
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

        public static void GetData(int minSup)
        {

            SqlCommand command;
            SqlDataReader reader;
            string sql = string.Format("exec [dbo].[SP_GIAOTAC] {0}", minSup);
            command = new SqlCommand(sql, Connection.conn);
            reader = command.ExecuteReader();
            D_Set = new Dictionary<string, List<I>>();
            ItemSet = new HashSet<I>();
            MinSup = 0;
            TotalTransaction = 0;
            while (reader.Read())
            {
                TotalTransaction++;
                List<I> set = new List<I>();

                for (int i = 0; i < reader.FieldCount - 1; i++)
                {
                    
                        int id = ItemCovnert.ConvertToId(reader.GetName(i + 1));
                        I item = new I(reader.GetName(i + 1), Int32.Parse(reader[i + 1].ToString()), id);
                        set.Add(item);
                        ItemSet.Add(item);
                    
                }
                D_Set.Add(reader[0].ToString(), set);
            }
            MinSup = (minSup * TotalTransaction) / 100;
            //ConnectionObj.CloseConnection();
            reader.Close();
        }

        public static Dictionary<List<I>, int> apriori_gen(Dictionary<List<I>, int> l, int k)
        {
            Dictionary<List<I>, int> c = new Dictionary<List<I>, int>();
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

        private static bool IsSubSetOfL(List<List<I>> set, Dictionary<List<I>, int> L_Set)
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

        public static Dictionary<string, List<List<I>>> Generate_F1(Dictionary<string, List <I>> D_Set)
        {
            Dictionary<string, List<List<I>>> f1 = new Dictionary<string, List<List<I>>>();
            for (int i = 0; i < D_Set.Count; i++)
            {
                string key = D_Set.ElementAt(i).Key;
                List<List<I>> items = new List<List<I>>();
                foreach (var item in D_Set.ElementAt(i).Value)
                {
                    if (item.Value==1)
                    {
                        List<I> set = new List<I>();
                        set.Add(new I(item));
                        items.Add(set);
                    }
                }

                f1.Add(key, items);
            }

            return f1;
        }
        public static bool CompareItemset(List<I> itemset1, List<I> itemset2)
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
        public static bool IsContainKey(Dictionary<List<I>, int> c, List<I> key)
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
        public static bool IsContain(List<List<I>> c, List<I> key)
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
        public static Dictionary<List<I>, int> Generate_L1(Dictionary<string, List<List<I>>> F1)
        {
            Dictionary<List<I>, int> l1 = new Dictionary<List<I>, int>();
            Dictionary<List<I>, int> c1 = new Dictionary<List<I>, int>();
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
                if (item.Value >= MinSup)
                {
                    l1.Add(item.Key, item.Value);
                }
            }
            return l1;
        }
        public static void Init()
        {
            
           

            Dictionary<string, List<List<I>>> F1 = Generate_F1(D_Set);
            Dictionary<List<I>, int> L1 = Generate_L1(F1);
            steps = new List<Step>();
            steps.Add(new Step(F1, L1));
        }

        public static void Generate()
        {
            Dictionary<List<I>, int> C_Set;
            Dictionary<string, List<List<I>>> F_Set;
            Dictionary<List<I>, int> L_Set;
            int k = 2;
            while (true)
            {

                F_Set = new Dictionary<string, List<List<I>>>();
                L_Set = new Dictionary<List<I>, int>();
                C_Set = apriori_gen(steps[k - 2].L_Set, k);

                foreach (var transaction in steps[k - 2].F_Set)
                {
                    var c = new List<List<I>>();
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
                    if (item.Value >= MinSup)
                    {
                        L_Set.Add(item.Key, item.Value);
                    }
                }
                if (L_Set.Count <= 0)
                    break;
                steps.Add(new Step(F_Set, L_Set));
                k++;
            }

            FinalFI = new Dictionary<List<I>, int>();
            foreach(var step in steps)
            {
                foreach(var item in step.L_Set)
                {
                    FinalFI.Add(item.Key, item.Value);
                }
            }
        }
        public static void Run()
        {
           
            Init();
            Generate();

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
    }
}
