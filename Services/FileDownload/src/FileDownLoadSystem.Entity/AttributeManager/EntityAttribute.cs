using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Entity.AttributeManager
{
    public class EntityAttribute : Attribute
    {
        /// <summary>
        /// 当前表的真实名称
        /// </summary>
        /// <value></value>
        public string TableName { get; set; }

        public string TableCnName { get; set; }

        public Type[] DetailTable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ForeignKeyName { get; set; }
    }
}