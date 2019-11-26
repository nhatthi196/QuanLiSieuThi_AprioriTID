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
  public static  class Constant
    {
        public static string sqlUnvailible = "Server is unavailible!";
        public static string curentUser = "";
        public static int currentStep = 0;
        public static Frame parentFrame = null;
        public static Window parentWindown = null;
        public static DataTable D_SetDataTable = null;
        public static DataTable I_SetDataTable = null;
        public static DataTable F_SetDataTable = null;
        public static DataTable L_SetDataTable = null;
        public static bool HaveLaw = false;
    }
}
