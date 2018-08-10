using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSCPRegistry
{
    public class WinSCPRegistryReader
    {
        public Dictionary<string, object> Keys { get; set; }

        public static List<WinSCPRegistryReader> GetHosts(bool decrypted = true)
        {
            var result = new List<WinSCPRegistryReader>();

            try
            {
                using (RegistryKey regk = Registry.CurrentUser.OpenSubKey("Software\\Martin Prikryl\\WinSCP 2\\Sessions"))
                {
                    if (regk != null)
                    {
                        var hosts = regk.GetSubKeyNames();
                        if (hosts != null)
                        {
                            foreach (var host in hosts)
                            {
                                var hostkey = regk.OpenSubKey(host);
                                var regs = hostkey.GetValueNames();

                                var wrr = new WinSCPRegistryReader() { Keys = new Dictionary<string, object>() };

                                foreach (var reg in regs)
                                {
                                    var value = hostkey.GetValue(reg);

                                    wrr.Keys.Add(reg, value);                                   
                                }

                                if (decrypted)
                                {
                                    try
                                    {
                                        var dec = new WinSCPDecrypt();

                                        wrr.Keys["Password"] = dec.Decrypt((string)wrr.Keys["HostName"], (string)wrr.Keys["UserName"], (string)wrr.Keys["Password"]);
                                        byte[] bytes = Encoding.Default.GetBytes((string)wrr.Keys["Password"]);
                                        wrr.Keys["Password"] = Encoding.UTF8.GetString(bytes);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }

                                }

                                result.Add(wrr);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
