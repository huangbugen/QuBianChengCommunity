using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Entity.Core
{
    public class SaveModel
    {
        public Dictionary<string, object> MainData { get; set; }
        public List<Dictionary<string,object>> DetailData { get; set; }
        public List<object> DelKeys { get; set; }
        public object Extra { get; set; }
    }
}