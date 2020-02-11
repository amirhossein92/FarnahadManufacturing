using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.UI.Common
{
    public class HeaderService
    {
        private static string GenerateHeaderTitle(string userControlName, string title = null)
        {
            if (string.IsNullOrEmpty(title))
                return $"{userControlName}";

            return $"{userControlName} - {title}";
        }

        public static string GenerateAddHeaderTitle(string userControlName)
        {
            return GenerateHeaderTitle(userControlName, "جدید");
        }

        public static string GenerateEditHeaderTitle(string userControlName, string title)
        {
            return GenerateHeaderTitle(userControlName, title);
        }

        public static string GenerateInActiveHeaderTitle(string userControlName)
        {
            return GenerateHeaderTitle(userControlName);
        }
    }
}
