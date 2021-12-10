using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetSnifferLib
{
    public static class PcapFile
    {
        public static void Save(string destination, IEnumerable<Packet> packets, UInt32 maxPacketSize = 65536, UInt32 networkType = 1)
        {
            using var writer = new BinaryWriter(File.Open(destination, FileMode.Create));
            //    The File Header has the following format
            //    0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
            //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // 0 |                          Magic Number                         |
            //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // 4 |          Major Version        |         Minor Version         |
            //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // 8 |                           Reserved1                           |
            //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //12 |                           Reserved2                           |
            //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //16 |                            SnapLen                            |
            //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //20 | FCS |f|                   LinkType                            |
            //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            
            //Magic Number(32 bits): an unsigned magic number, whose value is either the hexadecimal number 0xA1B2C3D4 or the hexadecimal number 0xA1B23C4D.
            //If the value is 0xA1B2C3D4, time stamps in Packet Records are in seconds and microseconds; if it is 0xA1B23C4D, time stamps in Packet Records are in seconds and nanoseconds.
            //These numbers can be used to distinguish sections that have been saved on little-endian machines from the ones saved on big - endian machines, and to heuristically identify pcap filesUInt32 magicNumber = 0xa1b2c3d4;
            UInt32 magicNumber = 0xa1b2c3d4;

            //Major Version (16 bits): an unsigned value, giving the number of the current major version of the format.The value for the current version of the format is 2
            UInt16 versionMajor = 2;

            //Minor Version(16 bits): an unsigned value, giving the number of the current minor version of the format.The value is for the current version of the format is 4.
            UInt16 versionMinor = 4;

            //Reserved1(32 bits): not used - SHOULD be filled with 0 by pcap file writers, and MUST be ignored by pcap file readers.
            UInt32 reserved1 = 0;

            //Reserved2(32 bits): not used - SHOULD be filled with 0 by pcap file writers, and MUST be ignored by pcap file readers.
            UInt32 reserved2 = 0;

            //SnapLen(32 bits): an unsigned value indicating the maximum number of octets captured from each packet.
            UInt32 snapLen = maxPacketSize;

            //linkType (32 bits): an unsigned value that defines, in the lower 28 bits, the link layer type of packets in the file
            UInt32 linkType = networkType;

            writer.Write(magicNumber);
            writer.Write(versionMajor);
            writer.Write(versionMinor);
            writer.Write(reserved1);
            writer.Write(reserved2);
            writer.Write(snapLen);
            writer.Write(linkType);

            //A Packet Record is the standard container for storing the packets coming from the network
            //     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
            //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //  0 |                      Timestamp (Seconds)                      |
            //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //  4 |            Timestamp (Microseconds or nanoseconds)            |
            //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //  8 |                    Captured Packet Length                     |
            //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // 12 |                    Original Packet Length                     |
            //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // 16 /                                                               /
            //    /                          Packet Data                          /
            //    /                        variable length                        /
            //    /                                                               /
            //    +---------------------------------------------------------------+

            var unixEpoch = new DateTime(1970, 1, 1);
            foreach (var packet in packets)
            {
                TimeSpan timestamp = packet.Timestamp.Subtract(unixEpoch);
                UInt32 seconds = (UInt32)timestamp.TotalSeconds;
                UInt32 microseconds = 1000 * ((UInt32)timestamp.TotalMilliseconds - 1000 * ((UInt32)timestamp.TotalSeconds));
                UInt32 capturedLength = (UInt32)packet.Buffer.Length;

                if (capturedLength > maxPacketSize)
                {
                    Debug.WriteLine($"Packet with size {capturedLength} exceeded max packet size {maxPacketSize}, packet ignored.");
                    continue;
                }

                UInt32 originalLength = capturedLength;

                writer.Write(seconds);
                writer.Write(microseconds);
                writer.Write(capturedLength);
                writer.Write(originalLength);
                writer.Write(packet.Buffer);
            }
        }

        public static IEnumerable<Packet> Read(string source)
        {
            var packets = new List<Packet>();

            using (var reader = new BinaryReader(File.Open(source, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                //    The File Header has the following format
                //    0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
                //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                // 0 |                          Magic Number                         |
                //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                // 4 |          Major Version        |         Minor Version         |
                //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                // 8 |                           Reserved1                           |
                //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                //12 |                           Reserved2                           |
                //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                //16 |                            SnapLen                            |
                //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                //20 | FCS |f|                   LinkType                            |
                //   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

                //Magic Number(32 bits): an unsigned magic number, whose value is either the hexadecimal number 0xA1B2C3D4 or the hexadecimal number 0xA1B23C4D.
                //If the value is 0xA1B2C3D4, time stamps in Packet Records are in seconds and microseconds; if it is 0xA1B23C4D, time stamps in Packet Records are in seconds and nanoseconds.
                //These numbers can be used to distinguish sections that have been saved on little-endian machines from the ones saved on big - endian machines, and to heuristically identify pcap filesUInt32 magicNumber = 0xa1b2c3d4;
                UInt32 magicNumber = reader.ReadUInt32();

                ///Major Version (16 bits): an unsigned value, giving the number of the current major version of the format.The value for the current version of the format is 2
                UInt16 versionMajor = reader.ReadUInt16();

                //Minor Version(16 bits): an unsigned value, giving the number of the current minor version of the format.The value is for the current version of the format is 4.
                UInt16 versionMinor = reader.ReadUInt16();

                //Reserved1(32 bits): not used - SHOULD be filled with 0 by pcap file writers, and MUST be ignored by pcap file readers.
                UInt32 reserved1 = reader.ReadUInt32();

                //Reserved2(32 bits): not used - SHOULD be filled with 0 by pcap file writers, and MUST be ignored by pcap file readers.
                UInt32 reserved2 = reader.ReadUInt32();

                //SnapLen(32 bits): an unsigned value indicating the maximum number of octets captured from each packet.
                UInt32 snapLen = reader.ReadUInt32();

                //linkType (32 bits): an unsigned value that defines, in the lower 28 bits, the link layer type of packets in the file
                UInt32 linkType = reader.ReadUInt32();

                //A Packet Record is the standard container for storing the packets coming from the network
                //     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
                //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                //  0 |                      Timestamp (Seconds)                      |
                //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                //  4 |            Timestamp (Microseconds or nanoseconds)            |
                //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                //  8 |                    Captured Packet Length                     |
                //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                // 12 |                    Original Packet Length                     |
                //    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                // 16 /                                                               /
                //    /                          Packet Data                          /
                //    /                        variable length                        /
                //    /                                                               /
                //    +---------------------------------------------------------------+

                var unixEpoch = new DateTime(1970, 1, 1);
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    UInt32 seconds = reader.ReadUInt32();
                    UInt32 microseconds = reader.ReadUInt32();

                    DateTime timestamp = unixEpoch.AddSeconds(seconds).AddMilliseconds(microseconds / 1000);

                    UInt32 capturedLength = reader.ReadUInt32();
                    UInt32 originalLength = reader.ReadUInt32();

                    byte[] buffer = reader.ReadBytes((int)originalLength);

                    var packet = new Packet(buffer, timestamp, DataLinkKind.Ethernet);

                    packets.Add(packet);
                }
            }

            return packets;
        }
    }
}
