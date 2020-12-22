using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigManager
{
    public class Functional
    {
        internal string Target { get; set; }

        internal string Source { get; set; }

        internal string Archive { get; set; }

        public Functional()
        {
            Target = @"C:\tdir";
            Source = @"C:\sdir";
            Archive = @"C:\tdir\tarchive";
        }
    }
}