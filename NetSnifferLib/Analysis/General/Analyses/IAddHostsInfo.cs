namespace NetSnifferLib.General
{
    interface IAddHostsInfo<T> where T: IAddress
    {
        public void AddHostsInfo(T source, T destination);
    }
}
