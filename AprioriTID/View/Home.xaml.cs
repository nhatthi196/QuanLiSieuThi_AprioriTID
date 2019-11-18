using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using AprioriTID.DAO;
using MahApps.Metro.Controls.Dialogs;

namespace AprioriTID.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : MetroWindow
    {
        public Home()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            currentUser.Content = Constant.curentUser;
            btnRun.IsEnabled = false;
            //this.Product_dataGrid.ItemsSource = ProcessData.GetProducts();
            //this.Transaction_dataGrid.ItemsSource = ProcessData.GetTransactions();

           
            TableSoure.setGridColumnWidth(I_dataGrid);
            //this.
        }

        private void slide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
            btnRun.IsEnabled = false;
            txtVal.Text = Math.Round(slide.Value, 0).ToString();
        }



        private void txtVal_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                btnRun.IsEnabled = false;
                if (String.IsNullOrEmpty(txtVal.Text) == false)
                {
                    slide.Value = Double.Parse(txtVal.Text);
                }
            }
            catch (FormatException ex)
            {
                txtVal.Clear();
                this.ShowMessageAsync("Warning!", ex.Message);
                txtVal.Focus();
            }

        }

        private void findbtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtVal.Text)==false)
            {
                btnRun.IsEnabled = true;
                int minsup = Int32.Parse(txtVal.Text);
                ProcessData.GetData(minsup);
                //this.D_dataGrid.Columns.Clear();
                this.D_dataGrid.DataContext = null;
                this.D_dataGrid.ItemsSource = TableSoure.D_SetDataTable(ProcessData.D_Set, ProcessData.ItemSet).DefaultView;
                this.I_dataGrid.ItemsSource = TableSoure.ItemSetDataTable(ProcessData.ItemSet).DefaultView;
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            ProcessData.Run();
            Process process = new Process();
            process.ShowDialog();
        }

        private void D_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            TableSoure.setGridColumnWidth(this.D_dataGrid);
        }

        private void I_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var column in this.I_dataGrid.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
    }
}
