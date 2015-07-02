using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 一个最简单的BT文件分析器
    /// </summary>
    class CommonAnalyser:IAnalyser
    {
        private byte[] torrentStream = null;
        private int index = 0;
        private BNodeFactory _bNodeFactory = null;

        public CommonAnalyser() 
        {
            torrentStream = null;
            BNodeList = new List<IBNode>();
            _bNodeFactory = new BNodeFactory(BNodeList);
            index = 0;
        }

        public IBNode Analysis(byte[] torrentStream)
        {
            // 清空上一次处理的信息
            BNodeList = new List<IBNode>();
            _bNodeFactory = new BNodeFactory(BNodeList);
            index = 0;

            this.torrentStream = torrentStream;
            // bt文件一定是一个字典开始的

            DictNode rootNode = _bNodeFactory.GetBNode('d') as DictNode;
            AnalysisDictionary(rootNode);
            return rootNode;
        }

        /// <summary>
        /// 取出当前字符，并指针后移
        /// </summary>
        /// <returns></returns>
        private char GetCurrentCharMove()
        {
            return (char)torrentStream[index++];
        }

        private char GetCurrentChar()
        {
            return (char)torrentStream[index];
        }

        private void AnalysisDictionary(IBNode parent)
        {
            // 字典一定是d开始的
            if (GetCurrentCharMove() != 'd')
                return;

            // 循环分析键值对
            do
            {
                KeyValueNode keyValueNode = _bNodeFactory.GetBNode('k') as KeyValueNode;
                // 键值对，键一定是string
                keyValueNode.SetKey(AnalysisString());
                // 值
                switch (GetCurrentChar())
                {
                    case 'i': // 数字
                        keyValueNode.SetValue(AnalysisInteger());
                        keyValueNode.ValueType = 'i';
                        break;
                    case 'd': // 字典
                        AnalysisDictionary(keyValueNode);
                        keyValueNode.ValueType = 'd';
                        break;
                    case 'l': // 列表
                        AnalysisList(keyValueNode);
                        keyValueNode.ValueType = 'l';
                        break;
                    default:
                        keyValueNode.SetValue(AnalysisString());
                        keyValueNode.ValueType = 's';
                        break;
                }
                parent.Child.Add(keyValueNode);
            } while (GetCurrentChar() != 'e');
            GetCurrentCharMove();
        }

        private void AnalysisList(IBNode parent)
        {
            // 列表一定是l开始的
            if (GetCurrentCharMove() != 'l')
                return;

            int count = 0;
            // 循环读入列表项
            do
            {
                ListItemNode listItemNode = _bNodeFactory.GetBNode('l') as ListItemNode;
                switch (GetCurrentChar())
                {
                    case 'i': // 数字
                        listItemNode.SetValue(AnalysisInteger());
                        listItemNode.ValueType = 'i';
                        break;
                    case 'd': // 字典
                        AnalysisDictionary(listItemNode);
                        listItemNode.ValueType = 'd';
                        break;
                    case 'l': // 列表
                        AnalysisList(listItemNode);
                        listItemNode.ValueType = 'l';
                        break;
                    default:
                        listItemNode.SetValue(AnalysisString());
                        listItemNode.ValueType = 's';
                        break;
                }
                listItemNode.ListIndex = count++;
                parent.Child.Add(listItemNode);
            } while (GetCurrentChar() != 'e');
            GetCurrentCharMove();
        }

        // 由于有些数字太大，用string来代替int
        private byte[] AnalysisInteger()
        {
            // 数字一定是i开始e结尾的
            if (GetCurrentCharMove() != 'i')
                return null;

            //StringBuilder builder = new StringBuilder();
            List<byte> integerByte = new List<byte>();
            char currentChar = ' ';
            while ((currentChar = GetCurrentCharMove()) != 'e')
            {
                //builder.Append(currentChar);
                integerByte.Add((byte)currentChar);
            }

            return integerByte.ToArray();
        }

        private byte[] AnalysisString()
        {
            char currentChar = GetCurrentCharMove();
            // 字符串一定是数字开始开始
            if (currentChar < '0' || currentChar > '9')
                return null;

            StringBuilder builder = new StringBuilder();

            do
            {
                builder.Append(currentChar);
                currentChar = GetCurrentCharMove();
            } while (currentChar >= '0' && currentChar <= '9');

            // 中间必须为：
            if (currentChar != ':')
                return null;

            int length = Int32.Parse(builder.ToString());
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; ++i)
            {
                buffer[i] = torrentStream[index++];
                //builder.Append(GetCurrentCharMove());
            }

            return buffer;
        }

        public List<IBNode> BNodeList { get; set; }
    }
}
