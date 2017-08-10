using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        public static string GetDescription(this object value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? string.Empty : attribute.Description;
        }
        public static T GetAttribute<T>(this object value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            if (memberInfo.Count() == 0)
            {
                return null;
            }
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
            {
                return (T)attributes[0];
            }
            return null;
        }
        public static string GetName(this object value)
        {
            return nameof(value);
        }
    }
}
