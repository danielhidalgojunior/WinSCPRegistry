using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSCPRegistry
{
    class dec_return
    {
        public byte Byte { get; set; }
        public byte[] Bytes { get; set; }

        public dec_return(byte b, byte[] bs)
        {
            Byte = b;
            Bytes = bs;
        }
    }
}
