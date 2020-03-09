using System.Collections.ObjectModel;
using System.Linq;

// CHECK
namespace FarnahadManufacturing.Base.Common
{
    public static class PaginationUtility
    {
        public static ObservableCollection<T> Paginate<T>(this IQueryable<T> queryable, int currentPage)
        {
            return new ObservableCollection<T>(queryable.Skip((currentPage - 1) * ApplicationSetting.PageRecordNumber)
                .Take(ApplicationSetting.PageRecordNumber).ToList());
        }

        public static string GetRecordsDetailText(int currentPage, int currentPageCount, int totalRecordsCount)
        {
            var fromRecordNumber = (currentPage - 1) * ApplicationSetting.PageRecordNumber + 1;
            var toRecordNumber = fromRecordNumber + currentPageCount - 1;
            return $"{fromRecordNumber}-{toRecordNumber} از {totalRecordsCount}";
        }

        public static int MaximumPageNumber(int totalRecordsCount)
        {
            return (totalRecordsCount / ApplicationSetting.PageRecordNumber);
        }
    }
}
