using DBToFile.Entity;
using DBToFile.Service;
using DBToFile.ViewModels;
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
        private readonly FileViewModel model = new FileViewModel();
        public MainWindow()
        {
            DataContext = model;
            InitializeComponent();

        }

        private void Export(object sender, RoutedEventArgs e)
        {
            exportbtn.IsEnabled = true;
            var win = MessageService.GetInstance();
            win.ShowMessage("正在导出.....");
            var task = Task.Factory.StartNew(() => model.HandleDbToFile());
            task.GetAwaiter().OnCompleted(() =>
            {
                win.CloseMessage();
                MessageBox.Show("完成导出");
                exportbtn.IsEnabled = true;
            });
        }

        private void GetTables(object sender, RoutedEventArgs e)
        {
            searchbtn.IsEnabled = false;
            exportbtn.IsEnabled = false;
            var win = MessageService.GetInstance();
            win.ShowMessage("正在加载......");
            var temp = Task.Factory.StartNew(() =>
              {
                  model.GetTables();
              });
            temp.GetAwaiter().OnCompleted(() =>
            {
                win.CloseMessage();
                searchbtn.IsEnabled = true;
                exportbtn.IsEnabled = true;
            });
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            model.SaveXML();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            var select =savelist.SelectedItem as SaveEntity;
            model.Connection = select.Connect;
            model.Name = select.Name;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var select = savelist.SelectedItem as SaveEntity;
            model.Delete(select.HideId);
        }
    }
}
