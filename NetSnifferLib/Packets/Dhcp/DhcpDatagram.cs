using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.IpV4;

using NetSnifferLib.General;
using NetSnifferLib.Packets.Bootp;

namespace NetSnifferLib.Packets.Dhcp
{
    public sealed class DhcpDatagram : CopiedDatagram
    { 
        public int FindFirst<T>(IEnumerable<T> enumerbale, T value)
        {
            foreach (var item in enumerbale.Select((thisVal, i) => new { i, thisVal }))
            {
                if (item.thisVal.Equals(value)) return item.i;
            }

            return -1;
        }

        public int FindLast<T>(IEnumerable<T> enumerable, T value)
        {
            int count = enumerable.Count();
            int first = FindFirst(enumerable.Reverse(), value);

            return first == -1 ? -1 : count - first;
        }

        public int FindFirst<T>(IEnumerable<T> enumerable, IEnumerable<T> subEnumerable)
        {
            int count = enumerable.Count();
            int subCount = subEnumerable.Count();
            int lastPossibleIndex = count - subCount;
            

            for (int i=0; i <= lastPossibleIndex; i++)
            {
                if (enumerable.Skip(i).Take(subCount).SequenceEqual(subEnumerable)) return i;
            }

            return -1;
        }

        public int FindLast<T>(IEnumerable<T> enumerable, IEnumerable<T> subEnumerable)
        {
            int count = enumerable.Count();
            int subCount = subEnumerable.Count();

            int first = FindFirst(enumerable.Reverse(), subEnumerable.Reverse());

            return first == -1 ? -1 : count - subCount - first; 
        }


        public static byte End => 0xFF;

        public static int MinimalLength => 228;

        public static byte[] MagickCookie { get; } = new byte[] { 0x63, 0x82, 0x53, 0x63};

        public int ExpectedLength { get; private set; }

        public BootpMessageType MessageType { get; private set; }

        public ArpHardwareType HardwareType { get; private set; }

        public byte HardwareLength { get; private set; }

        public byte Hops { get; private set; }

        public int TransactionId { get; private set; }

        public ushort SecondsElapsed { get; private set; }

        public bool IsBroadcast { get; private set; }

        public IpV4Address ClientIpAddress { get; private set; }

        public IpV4Address YourIpAddress { get; private set; }

        public IpV4Address NextServerIpAddress { get; private set; }

        public IpV4Address RelayAgentIpAddress { get; private set; }

        public MacAddress ClientMacAddress { get; private set; }

        public ReadOnlyCollection<byte> ClientMacAddressPadding { get; private set; }

        public string ServerHostName { get; private set; }

        public string BootFileName { get; private set; }

        public ReadOnlyCollection<byte> VendorSpecificArea { get; private set; }

        public ReadOnlyCollection<byte> Padding { get; private set; }

        private bool parsingFailed = false;

        private class FieldLength
        {
            public int ClientHardwareAddressPadding { get; set; }

            public int ServerHostName => 64;

            public int BootFileName => 128;

            public int MagikCookie => 4;

            public int Options { get; set; }

            public int End => 1;
        }

        private class FieldOffset
        {
            public int MessageType => 0;
            public int HardwareType => 1;
            public int HardwareLength => 2;
            public int Hops => 3;
            public int TransactionId => 4;
            public short SecondsElapsed => 8;
            public int BootFlags => 10;

            public int ClientIpAddress => 12;
            public int YourIpAddress => 16;
            public int NextServerIpAddress => 20;
            public int RelayAgentIpAddress => 24;
            public int ClientMacAddress => 28;

            public int ServerHostName { get; set; }

            public int BootFileName { get; set; }
            public int MagikCookie { get; set; }

            public int Options { get; set; }

            public int End { get; set; }

            public int Padding { get; set; }
        }

        public DhcpDatagram(byte[] buffer) : base(buffer)
        {
            FieldOffset fieldOffset = new();
            FieldLength fieldLength = new();

            if (Length < MinimalLength)
            {
                parsingFailed = true;
                return;
            }

            MessageType = (BootpMessageType)this[fieldOffset.MessageType];
            HardwareType = (ArpHardwareType)this[fieldOffset.HardwareType];
            HardwareLength = this[fieldOffset.HardwareLength];

            Hops = this[fieldOffset.Hops];

            TransactionId = ReadInt(fieldOffset.TransactionId, Endianity.Big);

            SecondsElapsed = ReadUShort(fieldOffset.SecondsElapsed, Endianity.Big);

            IsBroadcast = ReadBool(fieldOffset.BootFlags, 0b1000_0000);

            ClientIpAddress = ReadIpV4Address(fieldOffset.ClientIpAddress, Endianity.Big);
            YourIpAddress = ReadIpV4Address(fieldOffset.YourIpAddress, Endianity.Big);
            NextServerIpAddress = ReadIpV4Address(fieldOffset.NextServerIpAddress, Endianity.Big);
            RelayAgentIpAddress = ReadIpV4Address(fieldOffset.RelayAgentIpAddress, Endianity.Big);

            ClientMacAddress = ReadMacAddress(fieldOffset.ClientMacAddress, Endianity.Big);

            fieldOffset.End = FindLast(this, End);
            fieldOffset.MagikCookie = FindLast(this, MagickCookie);

            if (fieldOffset.End == -1 || fieldOffset.MagikCookie == -1)
            {
                parsingFailed = true;
                return;
            }

            fieldOffset.Padding = fieldOffset.End == Length - 1 ? -1 : fieldOffset.End + fieldLength.End;
            fieldLength.Options = fieldOffset.MagikCookie + fieldLength.MagikCookie;

            fieldOffset.BootFileName = fieldOffset.MagikCookie - fieldLength.BootFileName;
            fieldOffset.ServerHostName = fieldOffset.BootFileName - fieldLength.ServerHostName;

            try
            {
                ServerHostName = Encoding.UTF8.GetString(Subsegment(fieldOffset.ServerHostName, fieldLength.ServerHostName).ToArray());
                BootFileName = Encoding.UTF8.GetString(Subsegment(fieldOffset.BootFileName, fieldLength.BootFileName).ToArray());
            }
            catch (ArgumentException)
            {
                parsingFailed = true;
            }
        }

        protected override bool CalculateIsValid()
        {
            return !parsingFailed && base.CalculateIsValid();
        }
    }

    /*public sealed class DhcpDatagram : BootpDatagram
    {
        new public const int ExpectedLength = 300;

        private const int MinimalLength = 300;


        new private class Offset : BootpDatagram.Offset
        {
            protected Offset()
            {

            }

            [Obsolete("Replaced by \"Options\" in DHCP")]
            new public const int VendorSpecificArea = 232;

            public const int Options = 236;
        }


        public override int ExpectedMagickCookie => 0x63825363;

        public DhcpDatagram(byte[] buffer) : base(buffer, false)
        {
            if (Length < Offset.Options)
            {
                parsingSucceeded = false;
                return;
            }
        }

        [Obsolete("Named \"NextServerIpAddress\" in DHCP", true)]
        new public IpV4Address ServerIpAddress => throw new NotImplementedException();

        public IpV4Address NextServerIpAddress => base.ServerIpAddress;

        [Obsolete("Named \"GatewatIpAddress\" in DHCP", true)]
        new public IpV4Address GatewayIpAddress => throw new NotImplementedException();

        public IpV4Address RelayAgentIpAddress => base.GatewayIpAddress;

        [Obsolete("Replaced by \"Options\" in DHCP")]
        new public ReadOnlyCollection<byte> VendorSpecificArea => _vendorSpecificArea;

        protected override bool CheckLength()
        {
            return Length >= MinimalLength;
        }
    }*/
}
