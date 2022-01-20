namespace NetSnifferLib.General
{
    abstract class BaseContext<T> : IContext where T: IAddress
    {
        readonly T _source;
        readonly T _destination;

        public BaseContext() { }

        public BaseContext(T source, T destination)
        {
            _source = source;
            _destination = destination;
        }

        public IAddress Source => _source;

        public IAddress Destination => _destination;
    }
}
