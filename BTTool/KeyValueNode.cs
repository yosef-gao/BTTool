using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 键值对节点
    /// </summary>
    class KeyValueNode:IBNode
    {
        public char ValueType { get; set; }

        private byte[] _key;

        public string Key
        {
            get { return Encoding.UTF8.GetString(_key); }
        }

        // 统一用byte数组保存，防止转码的时候发生问题
        public void SetKey(byte[] key)
        {
            this._key = key;
        }

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

        public KeyValueNode()
        {
            Child = new List<IBNode>();
        }

        public byte[] ToBytes()
        {
            MemoryStream memoryStream = new MemoryStream();
            // 写入key
            byte[] buffer = Encoding.UTF8.GetBytes(String.Format("{0}:", _key.Length));
            memoryStream.Write(buffer, 0, buffer.Length);
            memoryStream.Write(_key, 0, _key.Length);

            // 写入value
            switch (ValueType)
            {
                case 's': 
                    buffer = Encoding.UTF8.GetBytes(String.Format("{0}:", _value.Length));
                    memoryStream.Write(buffer, 0, buffer.Length);
                    memoryStream.Write(_value, 0, _value.Length);
                    break;
                case 'i': 
                    buffer = Encoding.UTF8.GetBytes(String.Format("i{0}e", Value));
                    memoryStream.Write(buffer, 0, buffer.Length);
                    break;
                case 'd':
                    memoryStream.WriteByte((byte)'d');
                    foreach (IBNode node in Child)
                    {
                        buffer = node.ToBytes();
                        memoryStream.Write(buffer, 0, buffer.Length);
                    }
                    memoryStream.WriteByte((byte)'e');
                    break;
                case 'l':
                    memoryStream.WriteByte((byte)'l');
                    foreach (IBNode node in Child)
                    {
                        buffer = node.ToBytes();
                        memoryStream.Write(buffer, 0, buffer.Length);
                    };
                    memoryStream.WriteByte((byte)'e');
                    break;
            }

            return memoryStream.ToArray();
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            string retString = null;
            switch (ValueType)
            {
                case 's': retString = String.Format("{0}(s)[{1}]={2}", Key, Value.Length, Value); break;
                case 'i': retString = String.Format("{0}(i)={1}", Key, Value); break;
                case 'd': retString = String.Format("{0}({1})[{2}]", Key, ValueType, Child.Count); break;
                case 'l': retString = String.Format("{0}({1})[{2}]", Key, ValueType, Child.Count); break;
            }
            return retString;
        }


        public List<IBNode> Child { get; set; }

        public Object BindObject { get; set; }
    }
}
