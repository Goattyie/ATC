using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ATC.Wpf.Extensions
{
    internal static class Extensions
    {
        public static async Task<IEnumerable<TModel>> ReadToList<TModel>(this NpgsqlDataReader reader)
        {
            var list = new List<TModel>();

            while (await reader.ReadAsync())
            {
                var model = Activator.CreateInstance<TModel>();
                var properties = model.GetType().GetProperties();

                foreach(var property in properties)
                {
                    try
                    {
                        var propertyName = property.GetAttribute<DisplayAttribute>();
                        var ordinal = reader.GetOrdinal(propertyName);
                        var value = reader.GetValue(ordinal);
                        
                        property.SetValue(model, value);
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                list.Add(model);
            }

            return list;
        }

        public static string GetAttribute<TAttribute>(this MemberInfo propertyInfo)
           where TAttribute : Attribute 
            => propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true)
            .Cast<DisplayNameAttribute>()
            .Single()
            .DisplayName;
    }
}
