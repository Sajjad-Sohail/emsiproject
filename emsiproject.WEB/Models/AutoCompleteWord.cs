using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emsiproject.Models
{
    public class AutoCompleteWord : IEquatable<AutoCompleteWord>
    {
        public string label { get; set; }
        public string val { get; set; }

        public bool Equals(AutoCompleteWord other)
        {
            return this.label.Equals(other.label);
        }
        public override int GetHashCode()
        {
            return this.label.GetHashCode();

        }
    }
}
