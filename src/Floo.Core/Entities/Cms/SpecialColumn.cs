using Floo.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms
{
    public class SpecialColumn : BaseEntity
    {
        public string Cover { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }
    }
}