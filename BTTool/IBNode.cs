using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 节点接口
    /// </summary>
    interface IBNode
    {
        /// <summary>
        /// 用于写回字节
        /// </summary>
        /// <returns></returns>
        byte[] ToBytes();

        /// <summary>
        /// 接受修改
        /// </summary>
        /// <param name="visitor"></param>
        void Accept(IVisitor visitor);

        /// <summary>
        /// 子节点
        /// </summary>
        List<IBNode> Child { get; set; }
    }
}
