namespace Niubility.Core
{
    public class PagingData<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public T[] Items { get; set; }
    }
}