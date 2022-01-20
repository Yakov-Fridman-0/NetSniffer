namespace NetSnifferLib.General
{
    class GenericContext<T> : IContext where T: IAddress
    {
        readonly T _source;
        readonly T _destination;

        public GenericContext(T source, T destination)
        {
            _source = source;
            _destination = destination;
        }

        public IAddress Source => _source;

        public IAddress Destination => _destination;
    }
}
