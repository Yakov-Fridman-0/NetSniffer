using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using NetSnifferLib.General;


namespace NetSnifferLib.Packets.Bootp
{
    public class BootpDatagram : CopiedDatagram
    {
        protected class Offset
        {
            protected Offset()
            {

            }

            public const int MessageType = 0;
            public const int HardwareType = 1;
            public const int HardwareLength = 2;
            public const int Hops = 3;
            public const int TransactionId = 4;
            public const int SecondsElapsed = 8;
            public const int BootFlags = 10;

            public const int ClientIpAddress = 12;
            public const int YourIpAddress = 16;
            public const int ServerIpAddress = 20;
            public const int GatewayIpAddress = 24;
            public const int ClientMacAddress = 28;

            public const int ServerHostName = 34;
            public const int BootFileName = 98;

            public const int MagicCookie = 226;

            public const int VendorSpecificArea = 226;
        }


        protected int ExpectedLength { get; set; }


        protected const byte End = 0xFF;


        public virtual int ExpectedMagickCookie => 0x63825363;



        public int MagickCookie { get; protected set; } 

        protected ReadOnlyCollection<byte> _vendorSpecificArea = null;

        protected bool _isBroadcast = false;

        protected bool parsingSucceeded = false;
        
        protected BootpDatagram(byte[] buffer, bool checkLength) : base(buffer)
        {
            if (checkLength && Length != ExpectedLength)
            {
                parsingSucceeded = false;
                return;
            }

            try
            {
                MessageType = (BootpMessageType)this[Offset.MessageType];
                HardwareType = (ArpHardwareType)this[Offset.HardwareType];
                HardwareLength = this[Offset.HardwareLength];
                Hops = this[3];

                TransactionId = ReadInt(Offset.TransactionId, Endianity.Small);
                SecondsElapsed = ReadUShort(Offset.SecondsElapsed, Endianity.Small);
                IsBroadcast = ReadBool(Offset.BootFlags, 0b1000_0000);

                ClientIpAddress = ReadIpV4Address(Offset.ClientIpAddress, Endianity.Small);
                YourIpAddress = ReadIpV4Address(Offset.YourIpAddress, Endianity.Small);
                ServerIpAddress = ReadIpV4Address(Offset.ServerIpAddress, Endianity.Small);
                GatewayIpAddress = ReadIpV4Address(Offset.GatewayIpAddress, Endianity.Small);
                ClientMacAddress = ReadMacAddress(Offset.ClientMacAddress, Endianity.Small);

                ServerHostName = Encoding.UTF8.GetString(Subsegment(Offset.ServerHostName, 64).ToArray());
                BootFileName = Encoding.UTF8.GetString(Subsegment(Offset.BootFileName, 128).ToArray());

                MagickCookie = ReadInt(Offset.MagicCookie, Endianity.Small);

                parsingSucceeded = true;
            }
            catch
            {
                parsingSucceeded = false;
            }
        }

        
        public BootpDatagram(byte[] buffer) : this(buffer, true)
        {
            
        }


        #region Properties
        public BootpMessageType MessageType { get; protected set; }
        public ArpHardwareType HardwareType { get; protected set; }
        public byte HardwareLength { get; protected set; }
        public byte Hops { get; protected set; }
        public int TransactionId { get; protected set; }

        public ushort SecondsElapsed { get; protected set; }
        public bool IsBroadcast { get; protected set; }

        public IpV4Address ClientIpAddress { get; protected set; }

        public IpV4Address YourIpAddress { get; protected set; }

        public IpV4Address ServerIpAddress { get; protected set; }

        public IpV4Address GatewayIpAddress { get; protected set; }

        public MacAddress ClientMacAddress { get; protected set; }

        public ReadOnlyCollection<byte> ClientMacAddressPadding { get; protected set; }

        public string ServerHostName { get; protected set; }

        public string BootFileName { get; protected set; }

        
        public ReadOnlyCollection<byte> VendorSpecificArea { get; protected set; }


        public ReadOnlyCollection<byte> Padding;

        #endregion Properties

        #region Validity Check
        protected override bool CalculateIsValid()
        {
            return parsingSucceeded && CheckLength() && CheckMagickCookie();
        }
        
        protected virtual bool CheckLength()
        {
            return Length == ExpectedLength;
        }

        protected virtual bool CheckMagickCookie()
        {
            return MagickCookie == ExpectedMagickCookie;
        }
        #endregion Validity Check
    }
}
