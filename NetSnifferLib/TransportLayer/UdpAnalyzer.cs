using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Transport;

namespace NetSnifferLib.TransportLayer
{
    public class UdpAnalyzer : BaseTransportLayerAnalyzer<UdpDatagram>
    {
        public override string GetDatagramInfo(UdpDatagram datagram)
        {
            return $"{GetSourcePort(datagram)} → {GetDestinationPort(datagram)} Len={GetPayloadLength(datagram)}";
        }
    }
}
