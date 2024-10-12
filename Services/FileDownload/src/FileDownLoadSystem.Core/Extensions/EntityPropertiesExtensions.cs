using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FileDownLoadSystem.Core.Extensions
{
    public static class EntityPropertiesExtensions
    {
        public static PropertyInfo GetKeyProperty(this Type entity)
        {
            return entity.GetProperties().GetKeyProperty();
        }

        public static PropertyInfo GetKeyProperty(this IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(c=>c.IsKey()).FirstOrDefault();
        }
        public static bool IsKey(this PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(KeyAttribute),false).Any();
        }
    }
}