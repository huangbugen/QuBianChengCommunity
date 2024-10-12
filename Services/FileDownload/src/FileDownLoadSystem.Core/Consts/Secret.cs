using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Core.Consts
{
    public class Secret
    {
        public string User { get; set; }
        public string DB { get; set; }
        public string Redis { get; set; }
        public string JWT { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}