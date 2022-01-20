namespace NetSnifferLib.General
{
    class BaseAnalysisWithHosts<T> : BaseAnalysis<T, EmptyContext>, IAddHostsInfo<T> where T: class, IAddress
    {
        public BaseAnalysisWithHosts()
        {
            _hostsSupplied = false;
        }

        public void AddHostsInfo(T source, T destination)
        {
            AddHostsInfoCore(source, destination);
        }
    }
}
