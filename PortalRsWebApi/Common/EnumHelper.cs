using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PortalRSApi.Common
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Count() > 0)
                {
                    return ((DescriptionAttribute)attrs.First()).Description;
                }
            }

            return en.ToString();
        }
    }
}