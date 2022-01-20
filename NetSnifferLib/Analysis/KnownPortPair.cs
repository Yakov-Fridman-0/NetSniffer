namespace NetSnifferLib.Analysis
{
    sealed class KnownPortPair : IPortsMatch
    {
        readonly ushort _port1;
        readonly ushort _port2;

        public KnownPortPair(ushort port1, ushort port2)
        {
            _port1 = port1;
            _port2 = port2;
        }

        public bool PortsMatch(ushort port1, ushort port2)
        {
            return (_port1 == port1 && _port2 == port2) || (_port1 == port2 && _port2 == port1);
        }
    }
}
