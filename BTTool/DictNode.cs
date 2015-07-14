using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 字典节点
    /// </summary>
    class DictNode:IBNode
    {
        public DictNode()
        {
            Child = new List<IBNode>();
        }

        public byte[] ToBytes()
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.WriteByte((byte)'d');
            byte[] buffer;
            foreach (IBNode node in Child)
            {
                buffer = node.ToBytes();
                memoryStream.Write(buffer, 0, buffer.Length);
            }
            memoryStream.WriteByte((byte)'e');

            return memoryStream.ToArray();
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return String.Format("ROOT(d)[{0}]", Child.Count);
        }


        public List<IBNode> Child { get; set; }

        public Object BindObject { get; set; }


        public void SetValue(byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}
