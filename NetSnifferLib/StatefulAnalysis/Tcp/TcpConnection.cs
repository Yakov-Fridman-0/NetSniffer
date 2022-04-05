using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PcapDotNet.Packets.Transport;

using NetSnifferLib.StatefulAnalysis;

namespace NetSnifferLib.StatefulAnalysis.Tcp
{
    public sealed class TcpConnection
    {
        object statucLock = new();

        TcpConnectionStatus _status;

        public TcpConnectionStatus Status => _status;


        public IPEndPoint SenderEndPoint { get; init; }
        
        uint senderInitialRawSequenceNumber;

        uint senderNextSequenceNumber = 1;

        uint ExpectedSenderRawSequenceNumber => senderInitialRawSequenceNumber + senderNextSequenceNumber;

        public uint DataSent => senderNextSequenceNumber - 1;




        public IPEndPoint ReceiverEndPoint { get; init; }

        uint receiverInitialRawSequenceNumber;

        uint receiverNextSequnceNumber = 1;

        uint ExpectedReceiverRawSequenceNumber => receiverInitialRawSequenceNumber + receiverNextSequnceNumber;

        public uint DataReceived => receiverNextSequnceNumber - 1;



        //public bool SenderInitiatedOpening => true;

        public bool SenderInitialtedClosing { get; private set; }


        public TcpConnection(IPEndPoint receiverEndPoint, IPEndPoint senderEndPoint)
        {
            ReceiverEndPoint = receiverEndPoint;
            SenderEndPoint = senderEndPoint;

            _status = TcpConnectionStatus.None;
        }

        public void ReportSentPacket(TcpControlBits flags, uint rawSequenceNumber, uint rawAcknowledgementNumber, uint payloadLength)
        {
            switch (_status)
            {
                case TcpConnectionStatus.None:
                    if ((flags & TcpControlBits.Synchronize) != 0)
                    {
                        _status = TcpConnectionStatus.Syn;

                        Interlocked.Exchange(ref senderInitialRawSequenceNumber, rawSequenceNumber);
                        Interlocked.Exchange(ref senderNextSequenceNumber, 1);
                    }
                    else
                    {
                        _status = TcpConnectionStatus.Established;
                        Interlocked.Exchange(ref senderInitialRawSequenceNumber, rawSequenceNumber);
                        Interlocked.Exchange(ref senderNextSequenceNumber, 0);
                        Interlocked.Exchange(ref receiverInitialRawSequenceNumber, rawAcknowledgementNumber);
                        Interlocked.Exchange(ref receiverNextSequnceNumber, 0);

                        UpdateSentSequenceAndAcknowledgmentNumbers(rawSequenceNumber, rawAcknowledgementNumber, payloadLength);
                    }
                    break;
                case TcpConnectionStatus.SynAck:
                    if ((flags & TcpControlBits.Acknowledgment) != 0)
                    {
                        _status = TcpConnectionStatus.Established;
                    }

                    UpdateSentSequenceAndAcknowledgmentNumbers(rawSequenceNumber, rawAcknowledgementNumber, payloadLength);

                    break;
                case TcpConnectionStatus.Established:
                    if ((flags & TcpControlBits.Fin) != 0)
                    {
                        _status = TcpConnectionStatus.Fin;
                        SenderInitialtedClosing = true;
                    }

                    UpdateSentSequenceAndAcknowledgmentNumbers(rawSequenceNumber, rawAcknowledgementNumber, payloadLength);

                    break;
                case TcpConnectionStatus.Fin:
                    if ((flags & (TcpControlBits.Fin | TcpControlBits.Acknowledgment)) != 0)
                    {
                        if (!SenderInitialtedClosing)
                            _status = TcpConnectionStatus.FinAck;
                        //else
                          //  throw new InvalidOperationException("Receiver should send Fin Ack");
                    }
                    break;
                case TcpConnectionStatus.FinAck:
                    if ((flags & TcpControlBits.Acknowledgment) != 0)
                    {
                        if (SenderInitialtedClosing)
                            _status = TcpConnectionStatus.Closed;
                    }
                    break;
            }
        }

        public void ReportReceivedPacket(TcpControlBits flags, uint rawSequenceNumber, uint rawAcknowledgemtnNumber, uint payloadLength)
        {
            switch (Status)
            {
                case TcpConnectionStatus.None:
                    _status = TcpConnectionStatus.Established;
                    Interlocked.Exchange(ref senderInitialRawSequenceNumber, rawSequenceNumber);
                    Interlocked.Exchange(ref senderNextSequenceNumber, 0);
                    Interlocked.Exchange(ref receiverInitialRawSequenceNumber, rawAcknowledgemtnNumber);
                    Interlocked.Exchange(ref receiverNextSequnceNumber, 0);

                    UpdateReceivedSequenceAndAcknowledgmentNumbers(rawSequenceNumber, rawAcknowledgemtnNumber, payloadLength);

                    break;
                case TcpConnectionStatus.Syn:
                    if ((flags & (TcpControlBits.Synchronize | TcpControlBits.Acknowledgment)) != 0)
                    {
                        _status = TcpConnectionStatus.SynAck;

                        Interlocked.Exchange(ref receiverNextSequnceNumber, rawSequenceNumber);
                        Interlocked.Exchange(ref receiverInitialRawSequenceNumber, 1);
                    }
                    break;
                case TcpConnectionStatus.Established:
                    if ((flags & TcpControlBits.Fin) != 0)
                    {
                        _status = TcpConnectionStatus.Fin;
                        SenderInitialtedClosing = false;                      
                    }

                    UpdateReceivedSequenceAndAcknowledgmentNumbers(rawSequenceNumber, rawAcknowledgemtnNumber, payloadLength);

                    break;
                case TcpConnectionStatus.Fin:
                    if ((flags & (TcpControlBits.Fin | TcpControlBits.Acknowledgment)) != 0)
                    {
                        if (SenderInitialtedClosing)
                            _status = TcpConnectionStatus.FinAck;
                    }
                    break;
                case TcpConnectionStatus.FinAck:
                    if ((flags & TcpControlBits.Acknowledgment) != 0)
                    {
                        if (!SenderInitialtedClosing)
                            _status = TcpConnectionStatus.Closed;
                    }
                    break;
            }
        }


        private bool UpdateSentSequenceAndAcknowledgmentNumbers(uint rawSequenceNumber, uint rawAcknowledgementNumber, uint payloadLength)
        {
            bool result;
            if (rawSequenceNumber != ExpectedSenderRawSequenceNumber)
            {
                //TODO
                return false;
            }
            else
            {
                Interlocked.Add(ref senderNextSequenceNumber, payloadLength);
                result = true;
            }

            if (rawAcknowledgementNumber != ExpectedReceiverRawSequenceNumber)
            {
                //TODO
                return false;
            }
            else
            {
                return result && true;
            }
        }

        private bool UpdateReceivedSequenceAndAcknowledgmentNumbers(uint rawSequenceNumber, uint rawAcknowledgementNumber, uint payloadLength)
        {
            bool result;
            if (rawSequenceNumber != ExpectedReceiverRawSequenceNumber)
            {
                //TODO
                return false;
            }
            else
            {
                Interlocked.Add(ref receiverNextSequnceNumber, payloadLength);
                result = true;
            }
                
            if (rawAcknowledgementNumber != ExpectedReceiverRawSequenceNumber)
            {
                //TODO
                return false;
            }
            else
            {
                return result && true;
            }
        }
    }
}
