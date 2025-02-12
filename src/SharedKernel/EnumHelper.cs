using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SharedKernel;
public static class EnumHelper
{
    public static string GetDisplayName(Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        if (field is not null)
        {
            DisplayAttribute attribute = field.GetCustomAttribute<DisplayAttribute>();
            return attribute != null ? attribute.Name : value.ToString();
        }
        return string.Empty;
    }
}
