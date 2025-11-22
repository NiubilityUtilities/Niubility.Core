namespace Niubility.Core
{
    public class PagingSearchCriteria : SearchCriteria
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}