namespace Utilities.DataModels.MainFunctionality
{
    public class SortingAndFiltering
    {
        public static List<MarriageRegistry> SortingByAdmArea(in List<MarriageRegistry> data, bool isSortingReverse)
        {
            if (data is null || data.Count == 0)
                throw new ArgumentNullException(nameof(data));

            if (!isSortingReverse) return data.OrderBy(z => z.Address!.AdmArea).ToList();
            else return data.OrderByDescending(z => z.Address!.AdmArea).ToList();
        }

        public static List<MarriageRegistry> FilteringByAdmAreaOrAdmAreaCode(in List<MarriageRegistry> data,
                                                                             string? filterValue, bool choice)
        {
            if (data is null || data.Count == 0)
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrEmpty(filterValue) || filterValue is null)
                throw new ArgumentNullException(nameof(filterValue));

            if (choice)
            {
                if (data.Where(z => z.Address!.AdmArea == filterValue).ToList().Count == 0)
                    throw new ArgumentException("Значение не найдено.");
                else return data.Where(z => z.Address!.AdmArea == filterValue).ToList();
            }
            else
            {
                if (data.Where(z => z.Address!.AdmAreaCode == filterValue).ToList().Count == 0)
                    throw new ArgumentException("Значение не найдено.");
                else return data.Where(z => z.Address!.AdmAreaCode == filterValue).ToList();
            }
        }
    }
}
