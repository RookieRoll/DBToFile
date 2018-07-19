using DBToFile.Service;
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

namespace DBToFile
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            exportbtn.IsEnabled = true;
            var win = new Message();
            win.Show();
           // MessageBox.Show(win,"正在导出文件，请稍后");
            var str = constr.Text;
            if(string.IsNullOrWhiteSpace(str))
            {
                win.Close();   
                MessageBox.Show("请输入链接字符串");
                exportbtn.IsEnabled = false;
                return;
            }
            MySqlDBService dbService = new MySqlDBService(str);
            dbService.HanderDbToFile();
            win.Close();
            MessageBox.Show("完成导出");

            exportbtn.IsEnabled = true;
        }

    }
}
