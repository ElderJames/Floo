using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floo.Core.Shared
{
    public class BaseQueryDto
    {
        public string Sort { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public IEnumerable<string> OrderBy { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> OrderByDesc { get; set; } = Enumerable.Empty<string>();
    }
}