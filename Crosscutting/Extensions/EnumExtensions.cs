using System.ComponentModel;

namespace Crosscutting.Extensions;

public static class EnumExtension
{
    public static string GetEnumDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());

        return fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false) is not DescriptionAttribute[] attributes || attributes.Length == 0
            ? value.ToString()
            : attributes[0].Description;
    }
}