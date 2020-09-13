using System.Collections.Generic;

namespace Floo.Core.Shared
{
    public class ListResult<TDto>
    {
        public int Count { get; set; }

        public int PageSize { get; set; }

        public int Offset { get; set; }

        public IEnumerable<TDto> Items { get; set; }

        public ListResult()
        {
        }

        public ListResult(int offset, int pageSize)
        {
            this.Offset = offset;
            this.PageSize = pageSize;
        }
    }
}