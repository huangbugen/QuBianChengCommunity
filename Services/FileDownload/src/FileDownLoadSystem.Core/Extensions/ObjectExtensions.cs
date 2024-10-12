using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static List<T> DicToList<T>(this List<Dictionary<string, object>> dicList)
        {
            // List<T> list = new();
            // foreach (var dic in dicList)
            // {
            //     list.Add(dic.DicToEntity<T>());
            // }
            // return list;
            return dicList.DicToEnumerable<T>().ToList();
        }
        public static T DicToEntity<T>(this Dictionary<string, object> dic)
        {
            return new List<Dictionary<string, object>>() { dic }.DicToList<T>().ToList()[0];
            // return dic.DicToEnumerable<T>();
        }

        public static IEnumerable<T> DicToEnumerable<T>(this List<Dictionary<string, object>> dicList)
        {
            // 具体转换逻辑
            foreach (var dic in dicList)
            {
                T obj = Activator.CreateInstance<T>();
                // T obj = new();
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in properties)
                {
                    if (prop != null)
                    {
                        if (!dic.TryGetValue(prop.Name, out object value)) continue;
                        prop.SetValue(obj, value.ChangeType(prop.PropertyType));
                    }
                }
                yield return obj;
            }
        }
        public static object ChangeType(this object convertibleValue, Type type)
        {
            if (null == convertibleValue) return null;
            try
            {
                if (type == typeof(Guid) || type == typeof(Guid?))
                {
                    string value = convertibleValue.ToString();
                    if (value == "") return null;
                    return Guid.Parse(value);
                }
                if (!type.IsGenericType) return Convert.ChangeType(convertibleValue, type);
                if (type.ToString() == "System.Nullable`1[System.Boolean]" || type.ToString() == "System.Boolean")
                {
                    if (convertibleValue.ToString() == "0")
                        return false;
                    return true;
                }
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                if(genericTypeDefinition == typeof(Nullable<>))
                {
                    return Convert.ChangeType(convertibleValue,Nullable.GetUnderlyingType(type));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;

        }
    }
}