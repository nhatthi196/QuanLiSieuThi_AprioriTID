using AprioriTID.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AprioriTID.DAO
{
  public static class TableSoure
    {
        public static DataTable D_SetDataTable(Dictionary<string, List<Item>> data, HashSet<Item> column,int page)
        {
            
            DataTable table = new DataTable();
            //Add column
            table.Columns.Add("TID",typeof(string));
            foreach(var item in column)
            {
                table.Columns.Add(item.Code,typeof(int));
            }
            int startPage = 50 * page ;
            int endPage = 50 * page + Constant.pageRange;
            //Add row
            for (int i=startPage;i<endPage ; i++)
            {
                DataRow row = table.NewRow();
                row[0] = data.Keys.ElementAt(i);
                for (int k = 0; k < data.Values.ElementAt(i).Count;k ++)
                {

                    row[k+1] = data.Values.ElementAt(i)[k].Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable D_SetDataTable(Dictionary<string, List<Item>> data, HashSet<Item> column)
        {
            DataTable table = new DataTable();
            //Add column
            table.Columns.Add("TID", typeof(string));
            foreach (var item in column)
            {
                table.Columns.Add(item.Code, typeof(int));
            }
           
            //Add row
            for (int i = 0; i < data.Count; i++)
            {
                DataRow row = table.NewRow();
                row[0] = data.Keys.ElementAt(i);
                for (int k = 0; k < data.Values.ElementAt(i).Count; k++)
                {

                    row[k + 1] = data.Values.ElementAt(i)[k].Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable ItemSetDataTable(HashSet<Item> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Mã mặt hàng", typeof(string));
            table.Columns.Add("Id",typeof(int));

            foreach(var item in data)
            {
                DataRow row = table.NewRow();
                row[0] = item.Code;
                row[1] = item.Id;
                table.Rows.Add(row);      
            }
            return table;
        }

        public static DataTable F_SetDataTable(Dictionary<string, List<List<Item>>> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("TID",typeof(string));
            table.Columns.Add("Tập mục", typeof(string));
            foreach(var item in data)
            {
                DataRow row = table.NewRow();
                row[0] = item.Key;
                row[1] = ItemUtil.Display(item.Value);
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable F_SetDataTable(Dictionary<string, List<List<Item>>> data, int page)
        {
            DataTable table = new DataTable();
            table.Columns.Add("TID", typeof(string));
            table.Columns.Add("Tập mục", typeof(string));
            int pageSize = data.Count / 50;
            int startPage = 50 * page;
            int endPage = 50 * page + pageSize;
            for(int i = startPage;i <endPage;i++)
            {
                DataRow row = table.NewRow();
                row[0] = data.ElementAt(i).Key;
                row[1] = ItemUtil.Display(data.ElementAt(i).Value);
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable L_SetDataTable(Dictionary<List<Item>, int> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Tập thường xuyên", typeof(string));
            table.Columns.Add("Độ phổ biến(%)", typeof(double));
            foreach(var item in data)
            {
                DataRow row = table.NewRow();
                double supp = item.Value*100 / FindFI.TotalTransaction;
                row[0] = ItemUtil.DisplayItem(item.Key);
                row[1] = supp;
                table.Rows.Add(row);
            }
            return table;
        }
        
        public static void setGridColumnWidth(DataGrid dataGrid)
        {
            foreach(var column in dataGrid.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public static DataTable LawDataTable(List<Law> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Luật", typeof(string));
            table.Columns.Add("Độ tin cậy(%)", typeof(double));
            foreach (var item in data)
            {
                DataRow row = table.NewRow();

                row[0] = item.ToString();
                row[1] = item.Conf;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
