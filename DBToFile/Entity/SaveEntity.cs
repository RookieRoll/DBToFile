using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlUtil.Models;

namespace DBToFile.Entity
{
    public class SaveEntity: IXmlEntity
    {
        public string Name { get; set; }
        public string Connect { get; set; }
    }
}
