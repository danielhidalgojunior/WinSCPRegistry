using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSCPRegistry
{
    public class WinSCPDecrypt
    {
        const int PW_MAGIC = 0xA3;
        const int PW_FLAG = 0xFF;

        public string Decrypt(string host, string username, string password)
        {
            var key = username + host;
            byte[] passbytes = new byte[password.Length];

            for (int j = 0; j < password.Length; j++)
            {
                var hexvalue = password[j].ToString();
                var numhex = Convert.ToInt32(hexvalue, 16);
                passbytes[j] = (byte)numhex;
            }
          
            var dec_char = dec_next_char(passbytes);
            var flag = dec_char.Byte;
            passbytes = dec_char.Bytes;

            byte length = 0;

            if (flag == PW_FLAG)
            {
                passbytes = dec_next_char(passbytes).Bytes;

                var dummy1 = dec_next_char(passbytes);
                length = dummy1.Byte;
                passbytes = dummy1.Bytes;
            }
            else
            {
                length = flag;
            }

            var dummy = dec_next_char(passbytes);
            var tobedeleted = dummy.Byte;
            passbytes = dummy.Bytes;

            passbytes = passbytes.Skip(tobedeleted * 2).ToArray();

            var clearpass = "";

            byte i;
            byte val;

            for (i = 0; i < length; i++)
            {
                var dummy2 = dec_next_char(passbytes);

                val = dummy2.Byte;
                passbytes = dummy2.Bytes;

                clearpass += (char)val;
            }

            if (flag == PW_FLAG)
                clearpass = clearpass.Substring(key.Length);

            return clearpass;

        }

        private dec_return dec_next_char(byte[] passbytes)
        {
            if (passbytes.Length <= 0)
                return new dec_return(0, passbytes);

            var a = passbytes[0];
            var b = passbytes[1];

            passbytes = passbytes.Skip(2).ToArray();

            byte _byte = Convert.ToByte(~(((a << 4) + b) ^ PW_MAGIC) & 0xff);

            return new dec_return(_byte, passbytes);
        }
    }
}
