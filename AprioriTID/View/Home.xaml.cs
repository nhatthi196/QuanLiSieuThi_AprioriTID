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
        public int _currentPage = 0;
        public Home()
        {
            InitializeComponent();
        }

       
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            currentUser.Content = Constant.curentUser;
            this.findbtn.IsEnabled = false;
            btnRun.IsEnabled = false;
            //this.Product_dataGrid.ItemsSource = ProcessData.GetProducts();
            //this.Transaction_dataGrid.ItemsSource = ProcessData.GetTransactions();
            this.btnFinding.IsEnabled = false;
            this.btnFinding.Visibility = Visibility.Hidden;
           
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
                    this.findbtn.IsEnabled = true;
                }
            }
            catch (FormatException ex)
            {
                txtVal.Clear();
                this.ShowMessageAsync("Warning!", ex.Message);
                txtVal.Focus();
            }

        }

        private async void findbtn_Click(object sender, RoutedEventArgs e)
        {
            this.findbtn.IsEnabled = false;
            this._currentPage = 0;
            if (String.IsNullOrEmpty(txtVal.Text)==false)
            {
                int minsup = Int32.Parse(txtVal.Text);
                Task<bool> task = processD(minsup);
                bool result = await task;
                
                btnRun.IsEnabled = true;
                //this.D_dataGrid.Columns.Clear();
                if (Constant.PageSize <= 0)
                {
                    this.txtPageCount.Text = "1 of 1";
                    this.D_dataGrid.ItemsSource = TableSoure.D_SetDataTable(FindFI.D_Set, FindFI.ItemSet).DefaultView;
                    this.btnBeginPage.IsEnabled = false;
                    this.btnEndPage.IsEnabled = false;
                    this.btnPrePage.IsEnabled = false;
                    this.btnNextPage.IsEnabled = false;
                }
                else
                {
                    this.txtPageCount.Text = this._currentPage + 1 + " of " + Constant.PageSize;
                    this.D_dataGrid.ItemsSource = TableSoure.D_SetDataTable(FindFI.D_Set, FindFI.ItemSet, _currentPage).DefaultView;
                }
                this.I_dataGrid.ItemsSource = TableSoure.ItemSetDataTable(FindFI.ItemSet).DefaultView;
              
            }
        }
         
      public async Task<bool> processD(int min)
        {
         
            return await Task.Factory.StartNew(()=>
            {
                return FindFI.GetData(min);
                
            }   
            ).ConfigureAwait(true);
        }
      public  async Task<int> processFI()
        {
            return await Task.Factory.StartNew(()=> {
                return FindFI.Run();
            }).ConfigureAwait(true);
        }
        private async void btnRun_Click(object sender, RoutedEventArgs e)
        {
            Task<int> task = processFI();
            //var result1 = await this.ShowProgressAsync("Thông báo", "Đang tìm... Vui lòng chờ trong ít phút!").ConfigureAwait(true);
            this.btnRun.Visibility = Visibility.Hidden;
            this.btnFinding.Visibility = Visibility.Visible;
            var run = await task.ConfigureAwait(true);
            var result = await this.ShowMessageAsync("Thông báo", "Đã hoàn tất việc tìm tập item set thường xuyên!", MessageDialogStyle.Affirmative).ConfigureAwait(true);
            Process process = new Process();
            process.ShowDialog();
            this.btnFinding.Visibility = Visibility.Hidden;
            this.btnRun.Visibility = Visibility.Visible;
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

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Constant.parentWindown.Show();
            
        }

        private async void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.ShowMessageAsync("Thông báo","Bạn muốn đăng xuất?",MessageDialogStyle.AffirmativeAndNegative).ConfigureAwait(true);
            if(result == MessageDialogResult.Affirmative)
            {
                this.Close();
            }
            
            
        }

        private void btnPrePage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage>0)
            {
                _currentPage--;
                this.D_dataGrid.ItemsSource = TableSoure.D_SetDataTable(FindFI.D_Set, FindFI.ItemSet, _currentPage).DefaultView;
                
                this.txtPageCount.Text = this._currentPage + 1 + " of " + Constant.PageSize;
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage<Constant.PageSize)
            {
                _currentPage++;

                this.D_dataGrid.ItemsSource = TableSoure.D_SetDataTable(FindFI.D_Set, FindFI.ItemSet, _currentPage).DefaultView;

                this.txtPageCount.Text = this._currentPage + 1 + " of " + Constant.PageSize;
            }
        }

        private void btnBeginPage_Click(object sender, RoutedEventArgs e)
        {
            this._currentPage=0;
            this.D_dataGrid.ItemsSource = TableSoure.D_SetDataTable(FindFI.D_Set, FindFI.ItemSet, 0).DefaultView;
            this.txtPageCount.Text = 1 + " of " + Constant.PageSize;
        }

        private void btnEndPage_Click(object sender, RoutedEventArgs e)
        {
            this._currentPage = Constant.PageSize;
            this.D_dataGrid.ItemsSource = TableSoure.D_SetDataTable(FindFI.D_Set, FindFI.ItemSet, Constant.PageSize - 1).DefaultView;
            this.txtPageCount.Text = Constant.PageSize + " of " + Constant.PageSize;
        }
    }
}
