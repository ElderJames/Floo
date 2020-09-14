﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floo.Core.Shared
{
    public class BaseQuery
    {
        public int Limit { get; set; } = 10;

        public int Offset { get; set; } = 0;

        public IEnumerable<string> OrderBy { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> OrderByDesc { get; set; } = Enumerable.Empty<string>();
    }
}