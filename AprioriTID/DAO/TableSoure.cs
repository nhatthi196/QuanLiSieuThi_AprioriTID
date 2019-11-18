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
        public static DataTable D_SetDataTable(Dictionary<string, List<Item>> data, HashSet<Item> column)
        {
            DataTable table = new DataTable();
            //Add column
            table.Columns.Add("TID",typeof(string));
            foreach(var item in column)
            {
                table.Columns.Add(item.Code,typeof(int));
            }

            //Add row
            foreach(var item in data)
            {
                DataRow row = table.NewRow();
                row[0] = item.Key;
                for(int i=0;i< item.Value.Count;i++)
                {

                    row[i+1] = item.Value[i].Value;
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
            table.Columns.Add("Item set", typeof(string));
            foreach(var item in data)
            {
                DataRow row = table.NewRow();
                row[0] = item.Key;
                row[1] = ItemCovnert.Display(item.Value);
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable L_SetDataTable(Dictionary<List<Item>, int> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Item set", typeof(string));
            table.Columns.Add("Support(%)", typeof(double));
            foreach(var item in data)
            {
                DataRow row = table.NewRow();
                double supp = item.Value*100 / ProcessData.TotalTransaction;
                row[0] = ItemCovnert.DisplayItem(item.Key);
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
    }
}
