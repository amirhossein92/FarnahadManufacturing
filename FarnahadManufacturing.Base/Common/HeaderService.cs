// CHECK
namespace FarnahadManufacturing.Base.Common
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

        public static string GenerateInactiveHeaderTitle(string userControlName)
        {
            return GenerateHeaderTitle(userControlName);
        }
    }
}
