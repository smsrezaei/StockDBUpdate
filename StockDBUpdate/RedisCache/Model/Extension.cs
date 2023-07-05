using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public static class Extension
{
    public static string GetDisplayName(this System.Enum enumValue)
    {
        try
        {
            return
                enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .Name;
        }

        catch
        {
            return
                enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First().Name;

        }
    }
}

