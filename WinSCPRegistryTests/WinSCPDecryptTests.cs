using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinSCP_Reg_Decrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSCPRegistry.Tests
{
    [TestClass()]
    public class WinSCPDecryptTests
    {
        [TestMethod()]
        public void DecryptTest()
        {
            string host = "testing.com";
            string username = "testing";
            string password = "A35C47501B70C17D87CBD1D1984C28E828392F2835323B28392F2835323B723F33316D28392F2835323B6E42BFBA5CA4ABAE";

            var w = new WinSCPDecrypt();
            string result = w.Decrypt(host, username, password);

            if (result != "1testing2")
                Assert.Fail();
        }
    }
}