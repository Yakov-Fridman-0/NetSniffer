using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using PcapDotNet.Base;

namespace NetSnifferLib.General
{
    /// <summary>
    /// Same as original, but copyied
    /// </summary>
    public class CopiedDatagram : Datagram
    {
        public CopiedDatagram(byte[] buffer)
            : base(buffer)
        {
            Buffer = buffer;
        }

        public CopiedDatagram(byte[] buffer, int offset, int length)
            : base(buffer, offset, length)
        {
            Buffer = buffer;
        }

        protected void Write(byte[] buffer, ref int offset)
        {
            Buffer.BlockCopy(StartOffset, buffer, offset, Length);
            offset += Length;
        }

        protected void Write(byte[] buffer, int offset)
        {
            Write(buffer, ref offset);
        }

        /// <summary>
        /// The original buffer that holds all the data for the segment.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        protected byte[] Buffer { get; private set; }

        /// <summary>
        /// The offset of the first byte of the segment in the buffer.
        /// </summary>
        protected int StartOffset { get; private set; }

        /// <summary>
        /// Reads a requested number of bytes from a specific offset in the segment.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The bytes read from the segment starting from the given offset and in the given length.</returns>
        protected byte[] ReadBytes(int offset, int length)
        {
            return Buffer.ReadBytes(StartOffset + offset, length);
        }

        protected DataSegment Subsegment(ref int offset, int length)
        {
            DataSegment subSegemnt = new DataSegment(Buffer, StartOffset + offset, length);
            offset += length;
            return subSegemnt;
        }

        protected bool ReadBool(int offset, byte mask)
        {
            return (this[offset] & mask) == mask;
        }

        /// <summary>
        /// Reads 2 bytes from a specific offset in the segment as a ushort with a given endianity.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="endianity">The endianity to use to translate the bytes to the value.</param>
        /// <returns>The value converted from the read bytes according to the endianity.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "ushort")]
        protected ushort ReadUShort(int offset, Endianity endianity)
        {
            return Buffer.ReadUShort(StartOffset + offset, endianity);
        }

        /// <summary>
        /// Reads 3 bytes from a specific offset in the segment as a UInt24 with a given endianity.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="endianity">The endianity to use to translate the bytes to the value.</param>
        /// <returns>The value converted from the read bytes according to the endianity.</returns>
        protected UInt24 ReadUInt24(int offset, Endianity endianity)
        {
            return Buffer.ReadUInt24(StartOffset + offset, endianity);
        }

        /// <summary>
        /// Reads 4 bytes from a specific offset in the segment as an int with a given endianity.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="endianity">The endianity to use to translate the bytes to the value.</param>
        /// <returns>The value converted from the read bytes according to the endianity.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int")]
        protected int ReadInt(int offset, Endianity endianity)
        {
            return Buffer.ReadInt(StartOffset + offset, endianity);
        }

        /// <summary>
        /// Reads 4 bytes from a specific offset in the segment as a uint with a given endianity.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="endianity">The endianity to use to translate the bytes to the value.</param>
        /// <returns>The value converted from the read bytes according to the endianity.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "uint")]
        protected uint ReadUInt(int offset, Endianity endianity)
        {
            return Buffer.ReadUInt(StartOffset + offset, endianity);
        }

        /// <summary>
        /// Reads 6 bytes from a specific offset in the segment as a UInt48 with a given endianity.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="endianity">The endianity to use to translate the bytes to the value.</param>
        /// <returns>The value converted from the read bytes according to the endianity.</returns>
        protected UInt48 ReadUInt48(int offset, Endianity endianity)
        {
            return Buffer.ReadUInt48(StartOffset + offset, endianity);
        }

        protected ulong ReadULong(int offset, Endianity endianity)
        {
            return Buffer.ReadULong(StartOffset + offset, endianity);
        }

        protected BigInteger ReadUnsignedBigInteger(int offset, int length, Endianity endianity)
        {
            return Buffer.ReadUnsignedBigInteger(StartOffset + offset, length, endianity);
        }

        /// <summary>
        /// Reads 4 bytes from a specific offset in the segment as an IpV4Address with a given endianity.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="endianity">The endianity to use to translate the bytes to the value.</param>
        /// <returns>The value converted from the read bytes according to the endianity.</returns>
        protected IpV4Address ReadIpV4Address(int offset, Endianity endianity)
        {
            return Buffer.ReadIpV4Address(StartOffset + offset, endianity);
        }

        /// <summary>
        /// Reads 4 bytes from a specific offset in the segment as an IpV4Address with a given endianity.
        /// </summary>
        /// <param name="offset">The offset in the segment to start reading.</param>
        /// <param name="endianity">The endianity to use to translate the bytes to the value.</param>
        /// <returns>The value converted from the read bytes according to the endianity.</returns>
        protected IpV6Address ReadIpV6Address(int offset, Endianity endianity)
        {
            return Buffer.ReadIpV6Address(StartOffset + offset, endianity);
        }

        protected uint Sum16Bits()
        {
            return Sum16Bits(Buffer, StartOffset, Length);
        }

        /// <summary>
        /// Converts the given 16 bits sum to a checksum.
        /// Sums the two 16 bits in the 32 bits value and if the result is bigger than a 16 bits value repeat.
        /// The result is one's complemented and the least significant 16 bits are taken.
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        protected static ushort Sum16BitsToChecksum(uint sum)
        {
            // Take only 16 bits out of the 32 bit sum and add up the carrier.
            // if the results overflows - do it again.
            while (sum > 0xFFFF)
                sum = (sum & 0xFFFF) + (sum >> 16);

            // one's complement the result
            sum = ~sum;

            return (ushort)sum;
        }

        protected static uint Sum16Bits(IpV6Address address)
        {
            return Sum16Bits(address.ToValue());
        }

        protected static uint Sum16Bits(IpV4Address address)
        {
            return Sum16Bits(address.ToValue());
        }


        protected static uint Sum16Bits(UInt128 value)
        {
            return Sum16Bits((ulong)(value >> 64)) + Sum16Bits((ulong)value);
        }

        protected static uint Sum16Bits(ulong value)
        {
            return Sum16Bits((uint)(value >> 32)) + Sum16Bits((uint)value);
        }

        protected static uint Sum16Bits(uint value)
        {
            return (value >> 16) + (value & 0xFFFF);
        }

    }
}
