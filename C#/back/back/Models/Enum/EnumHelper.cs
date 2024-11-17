using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

public static class EnumHelper
{
    public static string GetDisplayName(Enum enumValue)
    {
        var enumMember = enumValue.GetType().GetMember(enumValue.ToString());

        if (enumMember.Length > 0)
        {
            var displayAttribute = enumMember[0].GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name ?? enumValue.ToString();
            }
        }

        return enumValue.ToString();
    }
}
