using DBToFile.Entity;
using DBToFile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBToFile.ViewModels
{
    public class FileViewModel : BaseViewModel
    {
        private string connection;
        private List<string> tablenames;
        private List<RecordsViewModel> constrs;
        private string selectedItem;
        private string name;
        public FileViewModel()
        {
            tablenames = new List<string>() { "全部" };
            constrs = service.GetSaves();
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

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
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

        public List<RecordsViewModel> ConStrs
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

        public void GetTables()
        {
            var temp = new MySqlDBService(connection).GetTableNames();
            temp.Insert(0, "全部");
            this.TableNames = temp;
        }

        public void HandleDbToFile()
        {
            MySqlDBService dbService = new MySqlDBService(connection);
            if (string.IsNullOrWhiteSpace(selectedItem))
                dbService.HanderDbToFile();
            else
                dbService.HandleTableToFile(selectedItem);
        }

        private XMLService service = new XMLService();
        public void SaveXML()
        {
            if (string.IsNullOrWhiteSpace(connection))
                throw new Exception("err connection");
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("err name");
            service.Add(name,connection);
            ConStrs = service.GetSaves();
        }

        public void Delete(string guid)
        {
            service.Delete(guid);
            ConStrs = service.GetSaves();
        }
    }
}
