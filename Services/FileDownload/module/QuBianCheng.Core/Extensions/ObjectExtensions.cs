using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuBianCheng.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void CheckNull(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }
    }
}