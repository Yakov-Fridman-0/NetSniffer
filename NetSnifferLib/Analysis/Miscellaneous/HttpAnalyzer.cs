using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Http;
using NetSnifferLib.General;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    public class HttpAnalyzer 
    {
        public override string ProtocolString => "HTTP";

        private const string HttpMagicString = "HTTP";
        private static readonly int HttpMagicStringLength;

        static HttpAnalyzer()
        {
            HttpMagicStringLength = HttpMagicString.Length;
        }

        private static bool IsAscii(byte val) => val >= 0 && val <= 255;
        
        public static bool DatagramMatches(Datagram datagram)
        {
            if (datagram.Equals(Datagram.Empty))
                return false;

            byte[] byteArray = datagram.ToArray();
   
            int newlineIndex = Array.IndexOf(byteArray, (byte)'\n');

            if (newlineIndex == -1)
                return false;

            int spaceIndex = Array.LastIndexOf(byteArray, (byte)' ');
            int slashIndex = Array.LastIndexOf(byteArray, (byte)'/');

            if (spaceIndex == -1 || slashIndex == -1)
                return false;

            if (slashIndex - spaceIndex != HttpMagicStringLength + 1)
                return false;

            byte[] subArray = new byte[HttpMagicStringLength];

            if (!subArray.All(IsAscii))
                return false;

            return Encoding.UTF8.GetString(subArray) == HttpMagicString;              
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram) => null;

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            var httpDatagram = (HttpDatagram)datagram;
            return httpDatagram.Body;
        }

        public override string GetDatagramInfo(Datagram datagram)
        {
            var httpDatagram = (HttpDatagram)datagram;
            return string.Empty;
        }
    }
}
