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

namespace Tlg_bk__
{
    /// <summary>
    /// Логика взаимодействия для GetPhone.xaml
    /// </summary>
    public partial class GetPhone : Window
    {
        MainWindow mw;
        public GetPhone(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            mw.Telnumber = tb_telnumber.Text;
            this.Close();
        }
    }
}
