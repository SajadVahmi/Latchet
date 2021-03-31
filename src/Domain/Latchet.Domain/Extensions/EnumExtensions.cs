using System;


namespace Latchet.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {

            Type genericEnumType = @enum.GetType();
            System.Reflection.MemberInfo[] memberInfo =
                        genericEnumType.GetMember(@enum.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {

                var attributes = memberInfo[0].GetCustomAttributes
                      (typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description;
                }
            }

            return @enum.ToString();
        }
        public static bool Any<T>(this T source, params T[] items)
            where T : Enum
        {
            if (source != null && items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (source.Equals(items[i]))
                        return true;
                }
            }

            return false;
        }

        public static bool Any<T>(this T? source, params T[] items)
            where T : struct, Enum
        {
            return source.HasValue && source.Value.Any(items);
        }

        public static bool NotAny<T>(this T source, params T[] items)
            where T : Enum
        {
            return !source.Any(items);
        }

        public static bool NotAny<T>(this T? source, params T[] items)
            where T : struct, Enum
        {
            return !source.Any(items);
        }
    }
}
