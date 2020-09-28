using System.Collections.Generic;

namespace Floo.App.Shared
{
    public class ListResult<TDto> : ListResult
    {
        public IEnumerable<TDto> Items { get; set; }

        public ListResult()
        {
        }

        public ListResult(ListResult result)
        {
            this.Offset = result.Offset;
            this.PageSize = result.PageSize;
            this.Count = result.Count;
        }

        public ListResult(int offset, int pageSize)
        {
            this.Offset = offset;
            this.PageSize = pageSize;
        }
    }

    public class ListResult
    {
        public int Count { get; set; }

        public int PageSize { get; set; }

        public int Offset { get; set; }
    }
}