using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ArpTable
{
    public enum ArpType
    {
        Dynamic,
        Static
    }

    public class ArpTabeItem
    {
        public IPAddress IPAddress { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public ArpType ArpType { get; set; }
    }

    public class ArpTabeEntity
    {
        public NetworkInterface NetworkInterface { get; set; }
        public List<ArpTabeItem> Items { get; set; }
    }


    public class ArpTabe
    {
        public List<ArpTabeEntity> Items { get; set; }
    }

    public class ArpTableParser
    {
        private NetworkInterface[] _networkInterfaces;

        private ArpTabe _arpTabe;

        public ArpTableParser()
        {
            _arpTabe = new ArpTabe
            {
                Items = new List<ArpTabeEntity>()
            };

            _networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        }

        public void Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
                return;

            var split = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            GetPasert(split)?.Invoke(split);
        }

        public ArpTabe GetTable()
        {
            return _arpTabe;
        }

        private Action<string[]> GetPasert(string[] data)
        {
            if (data.Length > 2 && data[0].StartsWith("Interface", StringComparison.InvariantCultureIgnoreCase) && IPAddress.TryParse(data[1], out _))
            {
                return ParseAsInterface;
            }
            if (data.Length == 3 && IPAddress.TryParse(data[0], out _))
            {
                return ParseAsItem;
            }

            return null;

        }

        private void ParseAsItem(string[] data)
        {
            var item = new ArpTabeItem
            {
                IPAddress = IPAddress.Parse(data[0]),
                PhysicalAddress = PhysicalAddress.Parse(data[1]),
                ArpType = Enum.Parse<ArpType>(data[2], true)
            };

            _arpTabe.Items.Last().Items.Add(item);
        }

        private void ParseAsInterface(string[] data)
        {
            if (IPAddress.TryParse(data[1], out var address) == false)
                throw new Exception(); //to do: add eception message

            var nic = FindNic(address);
            if (nic == null)
                throw new Exception(); //to do: add eception message

            var entity = new ArpTabeEntity
            {
                NetworkInterface = nic,
                Items = new List<ArpTabeItem>()
            };

            _arpTabe.Items.Add(entity);
        }

        private NetworkInterface FindNic(IPAddress address)
        {
            return _networkInterfaces.FirstOrDefault(networkInterfaces => networkInterfaces.GetIPProperties().UnicastAddresses.FirstOrDefault(unicastAddress => unicastAddress.Address.Equals(address)) != null);
        }
    }

    public static class AprTableHelper
    {
        private static void Execute(string command, Action<string?> outputDataReceived, Action<string?> errorDataReceived = null)
        {
            var process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = errorDataReceived != null;
            process.StartInfo.FileName = "cmd.exe";
            //process.StartInfo.FileName = "arp";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "/c " + command;
            //process.StartInfo.Arguments = command;
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                outputDataReceived(e.Data);
            };

            if (errorDataReceived != null)
            {
                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    errorDataReceived(e.Data);
                };
            }


            process.Start();
            process.BeginOutputReadLine();
            if (errorDataReceived != null)
            {
                process.BeginErrorReadLine();
            }

            process.WaitForExit();
        }
        private static void Execute(string fileName, string arguments, Action<string?> outputDataReceived, Action<string?> errorDataReceived = null)
        {
            var process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = errorDataReceived != null;
            process.StartInfo.FileName = fileName;

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = arguments;

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                outputDataReceived(e.Data);
            };

            if (errorDataReceived != null)
            {
                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    errorDataReceived(e.Data);
                };
            }


            process.Start();
            process.BeginOutputReadLine();
            if (errorDataReceived != null)
            {
                process.BeginErrorReadLine();
            }

            process.WaitForExit();
        }

        public static ArpTabe GetTable()
        {

            var parser = new ArpTableParser();

            Execute("arp", "-a", outputData => parser.Parse(outputData ?? string.Empty));
            //Execute("-a", outputData => parser.Parse(outputData ?? string.Empty));
            var arpTabe = parser.GetTable();

            return arpTabe;
        }

        public static IEnumerable<string> GetArpTable()
        {
            var list = new List<string>();

            Execute("arp", "-a", outputData => list.Add(outputData ?? string.Empty));

            return list;
        }

        public static IEnumerable<string> GetRouteTable()
        {
            var list = new List<string>();

            Execute("route", "print", outputData => list.Add(/*outputData?.Trim(' ')*/ outputData ?? string.Empty));

            return list;
        }


        public static IEnumerable<string> GetIpConfig()
        {

            var list = new List<string>();

            Execute("ipconfig", "", outputData => list.Add(outputData ?? string.Empty));

            return list;
        }
        public static void Execute(string fileName, string arguments, out string message, out string error)
        {
            string m = string.Empty;
            string e = string.Empty;

            Execute(fileName, arguments,
                outputData =>
                {
                    if (string.IsNullOrEmpty(outputData)) return;
                    if (string.IsNullOrEmpty(m) == false) m += Environment.NewLine;
                    m += outputData;
                },
         errorData =>
         {
             if (string.IsNullOrEmpty(errorData)) return;
             if (string.IsNullOrEmpty(e) == false) e += Environment.NewLine;
             e += errorData;
         });

            message = m;
            error = e;
        }

        public static void AddEntryToArpCache(IPAddress ipAddress, PhysicalAddress physicalAddress, out string message, out string error)
        {
            string physicalAddressString = physicalAddress.ToString();
            string formatedphysicalAddressString = string.Join("-", Enumerable.Range(0, physicalAddressString.Length / 2).Select(i => physicalAddressString.Substring(i * 2, 2)));

            Execute($"arp", $"-s {ipAddress} {formatedphysicalAddressString}", out message, out error);
        }

        public static void DeleteEntireArpCache(IPAddress ipAddress, out string message, out string error)
        {
            Execute($"arp", $"-d {ipAddress}", out message, out error);
        }
    }

}

