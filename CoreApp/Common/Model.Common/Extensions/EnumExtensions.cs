using System.ComponentModel;
using System.Reflection;

namespace Model.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (null == field) return value.ToString();
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            if (attribute is DescriptionAttribute description) return description.Description;
            return value.ToString();
        }

        public static T? GetAttribute<T>(this Enum value) where T : Attribute
        {
            var fields = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            if (fields == null) return default;
            var field = fields.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            if (field is T t) return t;
            return default;
        }
    }
}
