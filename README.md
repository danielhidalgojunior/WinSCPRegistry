# WinSCPRegistry

This code will scan the registry and retrieve all the information (including the decrypted password, if it exists) of the saved sessions on WinSCP.

The decryption methods were based on this project: https://github.com/anoopengineer/winscppasswd

This code is written in C#.

## Example of usage
```
var hosts = WinSCPRegistryReader.GetHosts(true);

foreach (var host in hosts)
{
    try
    {
        var pw = host.Keys["Password"];
        Console.WriteLine(pw);
    }
    catch { }
}
```
