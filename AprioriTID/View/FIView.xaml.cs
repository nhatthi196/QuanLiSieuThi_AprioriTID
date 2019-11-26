using AprioriTID.DAO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AprioriTID.View
{
    /// <summary>
    /// Interaction logic for FIView.xaml
    /// </summary>
    public partial class FIView : Page
    {
        public FIView()
        {
            InitializeComponent();
        }

        private void FI_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            TableSoure.setGridColumnWidth(this.FI_dataGrid);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.FI_dataGrid.ItemsSource = TableSoure.L_SetDataTable(FindFI.FinalFI).DefaultView;
        }

        private void btnfindlaw_Click(object sender, RoutedEventArgs e)
        {
            Constant.parentFrame.Content = new LawView();
        }
    }
}
