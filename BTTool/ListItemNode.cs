using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 列表节点
    /// </summary>
    class ListItemNode:IBNode
    {
        public int ListIndex { get; set; }

        public char ValueType { get; set; }

        private byte[] _value;

        public string Value
        {
            get { return Encoding.UTF8.GetString(_value); }
        }

        // 统一用byte数组保存，防止转码的时候发生问题
        public void SetValue(byte[] value)
        {
            this._value = value;
        }

        public ListItemNode()
        {
            Child = new List<IBNode>();
        }


        public byte[] ToBytes()
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] buffer;
            if (Child.Count > 0)
            {
                if (ValueType == 'l')
                {
                    memoryStream.WriteByte((byte)'l');
                }
                else if (ValueType == 'd')
                {
                    memoryStream.WriteByte((byte)'d');
                }

                Child.ForEach(node =>
                {
                    buffer = node.ToBytes();
                    memoryStream.Write(buffer, 0, buffer.Length);
                });
                memoryStream.WriteByte((byte)'e');
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(String.Format("{0}:", _value.Length));
                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Write(_value, 0, _value.Length);
            }

            return memoryStream.ToArray(); 
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string retString = null;
            if (Child.Count > 0)
                retString = String.Format("ITEM{0}({1})[{2}]", ListIndex, ValueType, Child.Count);
            else
                retString = String.Format("{0}", Value);

            return retString;
        }


        public List<IBNode> Child { get; set; }
    }
}
