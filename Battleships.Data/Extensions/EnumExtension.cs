using System;
using System.ComponentModel;
using System.Linq;

namespace Battleships.Data.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum GenericEnum)
        {
            var genericEnumType = GenericEnum.GetType();
            var memberInfo = genericEnumType.GetMember(GenericEnum.ToString());

            if ((memberInfo.Length <= 0))
                return GenericEnum.ToString();

            var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attribs.Any() ? ((DescriptionAttribute)attribs.ElementAt(0)).Description : GenericEnum.ToString();
        }
    }
}
