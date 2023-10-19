using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Healper
{
    public static class StringExtensions
    {
        public static bool IsStringLink(this string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp;
        }

    }

    public static class EnumExtensions
    {

        public static string? GetDisplayName(this System.Enum enumValue)
        {
            var strValue = enumValue.ToString();
            var attr = enumValue.GetType()
                                .GetMember(strValue)
                                .First()
                                .GetCustomAttribute<DisplayAttribute>(true);
            return attr == null ? strValue : attr.Name;
        }

    }
}
