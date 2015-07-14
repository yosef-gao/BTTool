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

        /// <summary>
        /// 与其绑定的树节点
        /// </summary>
        Object BindObject { get; set; }

        /// <summary>
        /// 设置值的字节表示，用于编码转换
        /// </summary>
        /// <param name="value"></param>
        void SetValue(byte[] value);
    }
}
