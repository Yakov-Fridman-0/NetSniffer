using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NetSnifferLib
{
    public interface IReportPacketLength
    {
        public void ReportPacket(ushort packetLength);
    }

    public interface IReportPacketLengthByHost<TAddress>
    {
        public void ReportPacket(TAddress address, ushort packetLength);
    }

    public interface IReportPacketPayloadLength
    {
        public void ReportPacket(ushort payloadLegnth);
    }

    public interface IReportPacketPayloadLengthByHost<TAddress>
    {
        public void ReportPacket(TAddress address, ushort payloadLegnth);

        public TAddress MostActiveHost { get; }
    }

    public interface IPacketNumber
    {
        public int PacketNumber { get; }
    }

    public interface IHostNumber
    {
        public int HostNumber { get;}
    }

    public interface ILength
    {
        public int LengthSum { get;}
        public double LengthMean { get; }
    }

    public interface IPayloadLength
    {
        public int PayloadLengthSum { get; }

        public double PayloadLenghMean { get; }
    }

    public class TrafficByHost
    {
        public int Number { get; private set; }
        public int LengthSum { get; private set; }

        public TrafficByHost(ushort length)
        {
            Number = 0;
            LengthSum = length;
        }

        public void ReportLength(ushort length)
        {
            Number++;
            LengthSum++;
        }
    }

    public class NetworkLayerStatistics : IPacketNumber, IHostNumber, IPayloadLength
    {
        private readonly Dictionary<IPAddress, TrafficByHost> trafficbyHost = new();

        public int PacketNumber
        {
            get => trafficbyHost.Sum(kvp => kvp.Value.Number);
        }

        public int HostNumber
        {
            get => trafficbyHost.Count;
        }

        public int PayloadLengthSum
        {
            get => trafficbyHost.Sum(kvp => kvp.Value.LengthSum);
        }

        public double PayloadLenghMean => PayloadLengthSum / PacketNumber;

        public void ReportPacket(IPAddress host, ushort payloadLength)
        {
            if (trafficbyHost.ContainsKey(host))
                trafficbyHost[host].ReportLength(payloadLength); 
            else
                trafficbyHost.Add(host, new TrafficByHost(payloadLength));
        }
    }
}
