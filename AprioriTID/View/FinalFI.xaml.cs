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
namespace AprioriTID.View
{
    /// <summary>
    /// Interaction logic for FinalFI.xaml
    /// </summary>
    public partial class FinalFI : MetroWindow
    {
        public FinalFI()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            this.FI_dataGrid.ItemsSource = TableSoure.L_SetDataTable(ProcessData.FinalFI).DefaultView;
        }

        private void FI_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
           
            TableSoure.setGridColumnWidth(this.FI_dataGrid);
        }
    }
}
