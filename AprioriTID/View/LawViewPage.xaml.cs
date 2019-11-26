using System;
using System.Collections.Generic;
using System.Data;
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
using AprioriTID.DAO;
using MahApps.Metro.Controls;


namespace AprioriTID.View
{
    /// <summary>
    /// Interaction logic for LawView.xaml
    /// </summary>
    public partial class LawView : Page
    {
        public LawView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Constant.HaveLaw = false;
            this.btnFindLaw.IsEnabled = false;
            this.ItemSetdataGrid.ItemsSource = TableSoure.L_SetDataTable(FindFI.FinalFI).DefaultView;
        }

        private void ItemSetdataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Constant.HaveLaw == true)
            {
                string data = ((DataRowView)ItemSetdataGrid.SelectedItem).Row[0].ToString();
                var itemset = ItemUtil.ConvsertStringToItemSet(data);
                var laws = FindLaw.getLawsByItemSet(itemset);

                this.LawSetdataGrid.ItemsSource = TableSoure.LawDataTable(laws).DefaultView;
            }
        }


        private  void btnFindLaw_Click(object sender, RoutedEventArgs e)
        {
            double minconf = Double.Parse(txtMinConf.Text);
            FindLaw.Generate(minconf);
            MessageBox.Show("Tìm luật hoàn tất. Nhấn chọn từng dòng của tập item set thường xuyên để xem luật tương ứng.");
            Constant.HaveLaw = true;
        }

        private void txtMinConf_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtMinConf.Text) == false)
                {
                    btnFindLaw.IsEnabled = true;
                    this.minconfSlide.Value = Double.Parse(txtMinConf.Text);
                }
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void minconfSlide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int minconf = (Int32)this.minconfSlide.Value; 
            txtMinConf.Text = minconf.ToString();
        }
    }
}
