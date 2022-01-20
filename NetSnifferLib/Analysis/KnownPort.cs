namespace NetSnifferLib.Analysis
{
    sealed class KnownPort : IPortsMatch
    {
        readonly ushort _port;

        public KnownPort(ushort port)
        {
            _port = port;
        }

        public bool PortsMatch(ushort port1, ushort port2)
        {
            return _port == port1 || _port == port2;
        }
    }
}
