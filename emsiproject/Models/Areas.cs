using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emsiproject.Models
{
    public class Areas
    {
        public string name { get; set; }
        public string abbr { get; set; }
        public string display_id { get; set; }
        public int child { get; set; }
        public int parent { get; set; }
        public int aggregation_path { get; set; }
        public int level { get; set; }

    }
}
