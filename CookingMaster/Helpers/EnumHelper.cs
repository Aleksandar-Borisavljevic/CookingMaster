using System.ComponentModel;

namespace CookingMaster.Helpers
{
    public static class EnumHelper
    {
        public static T GetEnumValue<T>(this string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            return Enum.TryParse(str, true, out T val) ? val : default;
        }

        public static T GetEnumValue<T>(this int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            return (T)Enum.ToObject(enumType, intValue);
        }

        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }

        public static List<string> GetEnumValuesAsStrings<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var stringValues = new List<string>();
            foreach (var value in values)
            {
                stringValues.Add(value.ToString());
            }
            return stringValues;
        }
    }
}
