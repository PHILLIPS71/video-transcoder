using System.ComponentModel;
using System.Reflection;

namespace Giantnodes.Infrastructure
{
    public static class DescriptionAttributeExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();

            FieldInfo? info = type.GetField(value.ToString());
            DescriptionAttribute[] attribute = info?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]
                ?? Array.Empty<DescriptionAttribute>();

            return attribute.Any() ? attribute.First().Description : string.Empty;
        }
    }
}
