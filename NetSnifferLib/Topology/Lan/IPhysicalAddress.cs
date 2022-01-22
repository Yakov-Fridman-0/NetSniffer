using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    interface IPhysicalAddress
    {
        PhysicalAddress PhysicalAddress { get; }
    }
}
