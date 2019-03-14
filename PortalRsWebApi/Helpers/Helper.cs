using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PortalRSApi.Helpers
{
    public static class Helper
    {
        public static T Duplicate<T>(this T source)
        {
            Type OfT = typeof(T);
            T result = (T)OfT.GetConstructor(System.Type.EmptyTypes).Invoke(null);

            FieldInfo[] objectFields = OfT.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance
                                                        | BindingFlags.FlattenHierarchy);

            foreach (FieldInfo fi in objectFields)
            {
                if (fi.FieldType == typeof(string))
                {
                    if (fi.GetValue(source) != null)
                    {
                        string sourcestring = (string)fi.GetValue(source);
                        fi.SetValue(result, new string(sourcestring.ToCharArray()));
                    }
                }
                else
                    fi.SetValue(result, fi.GetValue(source));
            }

            return result;
        }
    }
}