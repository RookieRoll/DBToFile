using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBToFile.ViewModels
{
    public class RecordsViewModel : BaseViewModel
    {
        private string id;
        private string name;
        private string constr;

        public string Id
        {
            get { return this.id; }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                this.name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string ConStr
        {
            get { return constr; }
            set { this.constr = value; RaisePropertyChanged("ConStr"); }
        }
    }
}
