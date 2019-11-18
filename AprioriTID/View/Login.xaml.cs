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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var isConnect = Connection.ConnectToDatabase(Connection.serverName, Connection.databaseName, txtUsername.Text, txtPassword.Password);
            if(isConnect.State)
            {
                Constant.curentUser = txtUsername.Text;
                Constant.parentWindown = this;
                Home home = new Home();
                home.Show();
                this.Hide();
            }
            else
            {
                this.ShowMessageAsync("Warning!",isConnect.Message);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MetroWindow_AccessKeyPressed(object sender, AccessKeyPressedEventArgs e)
        {

        }

        private void MetroWindow_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void MetroWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.txtPassword.Clear();
        }
    }
}
