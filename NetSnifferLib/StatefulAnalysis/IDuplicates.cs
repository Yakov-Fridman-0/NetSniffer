using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis
{
    interface IDuplicates<T> where T: IDuplicates<T>
    {
        T DuplicateOf { get; }

        List<T> Duplicates { get; } 
    }
}
