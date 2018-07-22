using DBToFile.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlUtils;
using System.Configuration;
using DBToFile.ViewModels;

namespace DBToFile.Service
{
    public class XMLService
    {
        private readonly string _path = ConfigurationManager.AppSettings["Path"];
        private readonly XmlHelper<SaveEntity> _helper;
        public XMLService(string path)
        {
            _path = path;
            _helper = new XmlHelper<SaveEntity>(_path);
        }
        public XMLService()
        {
            _helper = new XmlHelper<SaveEntity>(_path);
        }

        public void Add(string name,string connection)
        {
            _helper.Add(new SaveEntity
            {
                Name = name,
                Connect = connection
            });
        }

        public List<RecordsViewModel> GetSaves()
        {
            return _helper.Finds().Select(m=>new RecordsViewModel
            {
                Id=m.HideId,
                ConStr=m.Connect,
                Name=m.Name
            }).ToList();
            //return null;
        }

        public void Delete(string guid)
        {
            var node = _helper.FirstOrDefault(m => m.HideId == guid);
            _helper.Remove(node);
        }
    }
}
