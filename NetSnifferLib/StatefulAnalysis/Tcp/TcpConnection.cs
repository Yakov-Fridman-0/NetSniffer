using System;
using System.Collections.Generic;
using System.Net;

using PcapDotNet.Packets.Transport;

namespace NetSnifferLib.StatefulAnalysis.Tcp
{
    public sealed class TcpConnection
    {
        readonly object statusLock = new();

        TcpConnectionStatus _status;

        public TcpConnectionStatus Status
        {
            get => _status;
            set
            {
                lock (statusLock)
                    _status = value;
            }
        }

        //readonly List<(uint, uint)> 

        public IPEndPoint ConnectorEndPoint { get; private set; }
        
        uint connetorInitialRawSequenceNumber;

        uint connectorNextSequenceNumber = 1;

        uint ExpectedListenerRawSequenceNumber => connetorInitialRawSequenceNumber + connectorNextSequenceNumber;

        //public uint DataSentByConnector => connectorNextSequenceNumber - 1;
        public uint DataSentByConnector { get; private set; } = 0;


        public IPEndPoint ListenerEndPoint { get; private set; }

        uint listenerInitialRawSequenceNumber;

        uint listenerNextSequnceNumber = 1;

        uint ExpectedReceiverRawSequenceNumber => listenerInitialRawSequenceNumber + listenerNextSequnceNumber;

        //public uint DataSentByListener => listenerNextSequnceNumber - 1;
        public uint DataSentByListener { get; private set; } = 0;

        public bool WasDetectedAtCreation { get; private set; }

        private TcpConnection()
        {

        }

        public static TcpConnection CreateConnection(IPEndPoint senderIPEndPoint, IPEndPoint receiverIPEndPoint, TcpControlBits flags, uint rawSequenceNumber, uint rawAcknowledgementNumber, uint payloadLength)
        {
            var connection = new TcpConnection();

            if ((flags & TcpControlBits.Synchronize) != 0)
            {
                //SYN
                if ((flags & TcpControlBits.Acknowledgment) == 0)
                {
                    connection.WasDetectedAtCreation = true;
                    connection.Status = TcpConnectionStatus.Syn; 

                    connection.ConnectorEndPoint = senderIPEndPoint;
                    connection.ListenerEndPoint = receiverIPEndPoint;
                    connection.DataSentByConnector += payloadLength;

                    connection.connetorInitialRawSequenceNumber = rawSequenceNumber;
                }
                //SYN-ACK
                else
                {
                    connection.WasDetectedAtCreation = true;
                    connection.Status = TcpConnectionStatus.SynAck;

                    connection.ListenerEndPoint = senderIPEndPoint;
                    connection.ConnectorEndPoint = receiverIPEndPoint;
                    connection.DataSentByListener += payloadLength;

                    connection.connetorInitialRawSequenceNumber = rawAcknowledgementNumber;
                    connection.listenerInitialRawSequenceNumber = rawSequenceNumber;
                }
            }
            else if ((flags & TcpControlBits.Fin) != 0)
            {
                //FIN
                if ((flags & TcpControlBits.Acknowledgment) == 0)
                {
                    connection.WasDetectedAtCreation = false;
                    connection.Status = TcpConnectionStatus.Fin;
                    // arbitary choice
                    connection.ConnectorEndPoint = senderIPEndPoint;
                    connection.connetorInitialRawSequenceNumber = rawAcknowledgementNumber;
                    connection.DataSentByConnector += payloadLength;

                    connection.ListenerEndPoint = receiverIPEndPoint;
                    connection.listenerInitialRawSequenceNumber = rawSequenceNumber;
                }
                //FIN-ACK
                else
                {
                    connection.WasDetectedAtCreation = false;
                    connection.Status = TcpConnectionStatus.FinAck;
                    // arbitary choice
                    connection.ConnectorEndPoint = senderIPEndPoint;
                    connection.connetorInitialRawSequenceNumber = rawAcknowledgementNumber;
                    connection.DataSentByConnector += payloadLength;

                    connection.ListenerEndPoint = receiverIPEndPoint;
                    connection.listenerInitialRawSequenceNumber = rawSequenceNumber;
                }
            }            //ACK ot PSH or URG
            else //if ((flags & TcpControlBits.Acknowledgment) != 0)
            {
                connection.WasDetectedAtCreation = false;
                // arbitary choice
                connection.ConnectorEndPoint = senderIPEndPoint;
                connection.connetorInitialRawSequenceNumber = rawAcknowledgementNumber;
                connection.DataSentByConnector += payloadLength;

                connection.ListenerEndPoint = receiverIPEndPoint;
                connection.listenerInitialRawSequenceNumber = rawSequenceNumber;

                if ((flags & TcpControlBits.Reset) != 0)
                {
                    connection.Status = TcpConnectionStatus.Rst;
                }
                else
                {
                    connection.Status = TcpConnectionStatus.Established;
                }
            }
            //else
            //{
            //    throw new ArgumentException($"{nameof(flags)} pakcet is no fin, syn or ack");
            //}

            return connection;
        }

        public void AnalyzeConnectorPacket(TcpControlBits flags, uint rawSequenceNumber, uint rawAcknowledgementNumber, uint payloadLength)
        {
            if ((flags & TcpControlBits.Synchronize) != 0)
            {
                //SYN
                /*                if ((flags & TcpControlBits.Acknowledgment) == 0)
                                {

                                }*/
                //SYN-ACK
                //else
                //{

                //}
                _status = TcpConnectionStatus.Syn;
            }
            else if ((flags & TcpControlBits.Fin) != 0)
            {
                //FIN
                if ((flags & TcpControlBits.Acknowledgment) == 0) 
                {
                    _status = TcpConnectionStatus.Fin;
                }
                //FIN-ACK
                else
                {
                    _status = TcpConnectionStatus.FinAck;
                }
            }
            //ACK
            else if ((flags & TcpControlBits.Acknowledgment) != 0)
            {
                if (_status == TcpConnectionStatus.SynAck)
                    _status = TcpConnectionStatus.Established;
                else if (_status == TcpConnectionStatus.FinAck)
                    _status = TcpConnectionStatus.Closed;

            }

            //connectorNextSequenceNumber = rawSequenceNumber + payloadLength;
            DataSentByConnector += payloadLength;
/*            else
            {
                throw new ArgumentException($"{nameof(flags)} pakcet is no fin, syn or ack");
            }*/
        }

        public void AnalyzeListenerPacket(TcpControlBits flags, uint rawSequenceNumber, uint rawAcknowledgementNumber, uint payloadLength)
        {
            if ((flags & TcpControlBits.Synchronize) != 0)
            {
                //SYN
                /*                if ((flags & TcpControlBits.Acknowledgment) == 0)
                                {

                                }*/
                //SYN-ACK
                /*                else
                                {

                                }*/
                _status = TcpConnectionStatus.SynAck;
            }
            else if ((flags & TcpControlBits.Fin) != 0)
            {
                //FIN
                if ((flags & TcpControlBits.Acknowledgment) == 0)
                {
                    _status = TcpConnectionStatus.Fin;
                }
                //FIN-ACK
                else
                {
                    _status = TcpConnectionStatus.FinAck;
                }
            }
            //ACK
            else if ((flags & TcpControlBits.Acknowledgment) != 0)
            {
                //listenerNextSequnceNumber = rawSequenceNumber + payloadLength;
                if (_status == TcpConnectionStatus.SynAck)
                    _status = TcpConnectionStatus.Established;
                else if (_status == TcpConnectionStatus.FinAck)
                    _status = TcpConnectionStatus.Closed;
            }

            DataSentByListener += payloadLength;
/*            else if ()
            {
                throw new ArgumentException($"{nameof(flags)} pakcet is no fin, syn or ack");
            }*/
        }

        /*        public void ReportReceivedPacket(TcpControlBits flags, uint rawSequenceNumber, uint rawAcknowledgemtnNumber, uint payloadLength)
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
                    if (rawSequenceNumber != ExpectedListenerRawSequenceNumber)
                    {
                        //TODO
                        return false;
                    }
                    else
                    {
                        Interlocked.Add(ref connectorNextSequenceNumber, payloadLength);
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
                        Interlocked.Add(ref listenerNextSequnceNumber, payloadLength);
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
                }*/
    }
}
