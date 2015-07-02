using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 批量转换工具
    /// </summary>
    class BatchConverser
    {
        private List<FileInfo> _sourFilenameList;
        private string _destFolder;

        private Action<string> _callBackFunc;

        public BatchConverser(string sourceFolder, string destFolder, Action<string> callBackFunc)
        {
            _sourFilenameList = new List<FileInfo>();
            DirectoryInfo dInfo = new DirectoryInfo(sourceFolder);
            foreach (FileInfo fInfo in dInfo.GetFiles())
            {
                // 过滤出种子文件
                if (fInfo.Extension.Equals(".torrent"))
                    _sourFilenameList.Add(fInfo);
            }

            _callBackFunc = callBackFunc;
            _destFolder = destFolder;
        }

        public void BacthVonverse()
        {
            long tick = Environment.TickCount;

            IAnalyser btAnalyser = new CommonAnalyser();
            foreach (FileInfo fInfo in _sourFilenameList)
            {
                byte[] buffer = null;
                using (FileStream stream = new FileStream(fInfo.FullName, FileMode.Open))
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                }
                // 分析
                IBNode rootNode = btAnalyser.Analysis(buffer);
                // 转换
                Iterate(btAnalyser.BNodeList);
                // 生成新的文件名
                string newFilename = String.Format("{0}\\{1}", _destFolder, fInfo.Name);
                // 保存
                buffer = rootNode.ToBytes();
                using (FileStream stream = new FileStream(newFilename, FileMode.Create))
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }

            if (_callBackFunc != null)
                _callBackFunc(String.Format("转换完毕， 总用时{0} 秒", (Environment.TickCount - tick) / 1000.0));
        }

        /// <summary>
        /// 给定一个bnode列表，转换其中的bnode
        /// </summary>
        /// <param name="bNodeList"></param>
        public static void Iterate(List<IBNode> bNodeList)
        {
            KeyValueVisitor visitor = new KeyValueVisitor();
            bNodeList.ForEach(node =>
            {
                // 如果是一个KeyValue Node
                if (node is KeyValueNode)
                {
                    (node as KeyValueNode).Accept(visitor);
                }
            });
        }
    }
}
