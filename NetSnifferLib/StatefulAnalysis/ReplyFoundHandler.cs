using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis
{
    delegate void ReplyFoundHandler(List<int> requestIds, List<int> replyIds);
}
