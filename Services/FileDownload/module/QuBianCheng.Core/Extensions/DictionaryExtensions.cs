using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuBianCheng.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void CheckDictionaryNull<TKey,TValue>(this Dictionary<TKey, TValue> obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if(obj.Count<=0)
            {
                throw new Exception("字典长度不能为0");
            }
        }
    }
}