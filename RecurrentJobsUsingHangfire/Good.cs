using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecurrentJobsUsingHangfire
{
    public class Good
    {
        public int GoodId { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public int MinCount { get; set; }
    }
}
