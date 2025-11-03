using System.ComponentModel;

namespace Almostengr.Refrigerator.Helpers;

public static class WebUiHelper
{
    public static string ToDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
        {
            return enumValue.ToString();
        }
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute?.Description ?? enumValue.ToString();
    }

    public static string ToOnOff(this bool value)
    {
        return value ? "On" : "Off";
    }
}