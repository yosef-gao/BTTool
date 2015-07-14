using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    interface IAnalyser
    {
        List<IBNode> BNodeList { get; set; }
        IBNode Analyse(byte[] torrentStream);
    }
}
