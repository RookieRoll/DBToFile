using DBToFile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBToFile.ViewModels
{
    public class FileViewModel : BaseViewModel, IDataErrorInfo
    {
        private string connection;
        private List<string> tablenames;
        private List<string> constrs;
        private string selectedItem;
        private MySqlDBService dbService;
        public FileViewModel()
        {
            tablenames = new List<string>() { "全部", "123", "234" };
            constrs = new List<string>();
            selectedItem = tablenames.FirstOrDefault();
        }
        public string Connection
        {
            get
            {
                return connection;
            }
            set
            {
                this.connection = value;
                base.RaisePropertyChanged("Connection");
            }
        }

        public List<string> TableNames
        {
            get
            {
                return tablenames;
            }
            set
            {
                this.tablenames = value;
                base.RaisePropertyChanged("TableNames");
            }
        }



        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                base.RaisePropertyChanged("SelectedItem");
            }
        }

        public List<string> ConStrs
        {
            get
            {
                return constrs;
            }
            set
            {
                this.constrs = value;
                base.RaisePropertyChanged("ConStrs");
            }
        }


        public string this[string propertyname]
        {
            get
            {
                string result = null;
                if (propertyname == "Connection")
                {
                    if (string.IsNullOrWhiteSpace(connection))
                        result = "数据库连接字符串不能为空";
                }
                return result;
            }
        }

        public string Error
        {
            get { return ""; }
        }

        public void InitTabelNames()
        {
            dbService = new MySqlDBService(connection);
            this.TableNames.Clear();
            this.TableNames.Add("全部");
            this.TableNames.AddRange(dbService.GetTableName());
        }
    }
}
