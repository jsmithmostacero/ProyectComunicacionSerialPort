using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPorts
{
    internal class ClassFile
    {

        public int number { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public long size { get; set; }
        public long advance { get; set; }
        public Boolean asset { get; set; }

        public ClassFile()
        {

        }

        public ClassFile(int number, string name, string path, long size, long advance, bool asset)
        {
            this.number = number;
            this.name = name;
            this.path = path;
            this.size = size;
            this.advance = advance;
            this.asset = asset;
        }
    }
}
