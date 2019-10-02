using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace databasr2.Extension
{

    public static class EnumExtension
    {
        public static string GetDisNameplay(this Enum enumvalue)
        {
            return enumvalue.GetType().GetMember(enumvalue.ToString()).First()
                    .GetCustomAttribute<DisplayAttribute>().GetName();
        }
    }
}
