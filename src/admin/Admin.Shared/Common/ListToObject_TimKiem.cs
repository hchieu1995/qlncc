using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Admin.Common
{
    public static class ListToObject_TimKiem
    {
        public static T ParseListToObject<T>(List<string> list) where T : new()
        {
            var result = new T();
            var type = typeof(T);

            foreach (var item in list)
            {
                var parts = item.Split(new[] { "@@" }, StringSplitOptions.None);
                if (parts.Length != 2) continue;

                string propName = parts[0];
                string valuePart = parts[1];

                var prop = type.GetProperty(propName,
                             BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (prop == null) continue;

                // Tách giá trị theo "!!"
                var values = valuePart.Split(new[] { "!!" }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(v => v.Trim())
                                      .ToList();

                // Gán giá trị nếu property là List<string>
                if (prop.PropertyType == typeof(List<string>))
                {
                    prop.SetValue(result, values);
                }
            }

            return result;
        }
        public static List<string> ObjectToList<T>(T obj)
        {
            var result = new List<string>();
            var type = typeof(T);

            foreach (var prop in type.GetProperties())
            {
                if (prop.PropertyType == typeof(List<string>))
                {
                    var values = prop.GetValue(obj) as List<string>;
                    if (values?.Any() == true)
                    {
                        result.Add($"{prop.Name}@@{string.Join("!!", values)}");
                    }
                }
            }

            return result;
        }
    }
}
