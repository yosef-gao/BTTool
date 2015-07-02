using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    interface IVisitor
    {
        /// <summary>
        /// 用于对keyValueNode类型节点的访问
        /// </summary>
        /// <param name="keyValueNode"></param>
        void Visit(KeyValueNode keyValueNode);
    }
}
