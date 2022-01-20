
namespace NetSnifferLib.General
{
    interface IContext
    {
        IAddress Source { get; }

        IAddress Destination { get; }
    }
}
