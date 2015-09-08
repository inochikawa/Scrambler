using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Attributes
{
    public class Strategy: Attribute
    {
        public Type Type { get; set; }
        public string Name { get; set; }
    }
}
