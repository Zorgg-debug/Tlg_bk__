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

namespace Tlg_bk__
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Telegram client;
        public string Telnumber;
        List<WebSelenium> Browsers;
        TlgManipulation usingTlg;
        public MainWindow()
        {
            InitializeComponent();
            ShowWindowGetPhone();
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            lb_status.Content = "Старт автомат";
            usingTlg = new TlgManipulation(client.client, Browsers);
        }

        private void ShowWindowGetPhone()
        {
            GetPhone gp = new GetPhone(this);
            gp.btn_cancel.Click += CloseWindow;
            gp.btn_ok.Click += GetTelNumber;
            gp.ShowDialog();
        }
        private void GetTelNumber(object sender, RoutedEventArgs e)
        {
            client = new Telegram(Telnumber);
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_autorization_Click(object sender, RoutedEventArgs e)
        {
            lb_status.Content = "Авторизация"; 
            client.GetAutorisation(tb_code.Text);
            Browsers = new List<WebSelenium>() { new WebSelenium(), new WebSelenium()};
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            lb_status.Content = "Обновление кэф";
            TlgManipulation usingTlg = new TlgManipulation(client.client);
        }

        private void tb_stop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
