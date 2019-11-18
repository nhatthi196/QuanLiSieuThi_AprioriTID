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
    /// Interaction logic for ProcessSupportPage.xaml
    /// </summary>
    public partial class ProcessSupportPage : Page
    {
        public ProcessSupportPage()
        {
            InitializeComponent();
        }
        

        private void btnPre_Click(object sender, RoutedEventArgs e)
        {
            this.btnNext.IsEnabled = true;

            this.btnFinal.Visibility = Visibility.Hidden;
            this.btnNext.Visibility = Visibility.Visible;
            Constant.currentStep--;
            if (Constant.currentStep <= 0)
                this.btnPre.IsEnabled = false;
            if (Constant.currentStep >= 0)
            {
                StepView(Constant.currentStep);
            }

        }
        public void StepView(int step)
        {
            this.tbF.Text = "TẬP F" + (step + 1).ToString();
            this.tbL.Text = "TẬP L" + (step + 1).ToString();
            this.F_SetdataGrid.ItemsSource = TableSoure.F_SetDataTable(ProcessData.steps[step].F_Set).DefaultView;
            this.L_SetdataGrid.ItemsSource = TableSoure.L_SetDataTable(ProcessData.steps[step].L_Set).DefaultView;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.btnPre.IsEnabled = true;
            Constant.currentStep++;
            if (Constant.currentStep >= ProcessData.steps.Count - 1)
            {
                this.btnNext.IsEnabled = false;
                this.btnFinal.Visibility = Visibility.Visible;
                this.btnNext.Visibility = Visibility.Hidden;
            }
            if (Constant.currentStep <= ProcessData.steps.Count - 1)
            {
                StepView(Constant.currentStep);
            }
        }

        private void btnFinal_Click(object sender, RoutedEventArgs e)
        {
            Constant.parentFrame.Content = new FIView();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            TableSoure.setGridColumnWidth(this.F_SetdataGrid);
            TableSoure.setGridColumnWidth(this.L_SetdataGrid);
            Constant.currentStep = 0;
            StepView(Constant.currentStep);
            this.btnPre.IsEnabled = false;
            this.btnNext.IsEnabled = true;
            this.btnNext.Visibility = Visibility.Visible;
            this.btnFinal.Visibility = Visibility.Hidden;
            
            
        }
    }
}
