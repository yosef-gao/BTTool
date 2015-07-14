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
                if (!fInfo.Extension.Equals(".torrent"))
                    continue; // 过滤非BT文件
                TorrentFile torrentFile = new TorrentFile();
                torrentFile.OpenFile(fInfo.FullName);
                torrentFile.Modify();
                string newFilename = String.Format("{0}\\{1}", _destFolder, fInfo.Name);
                torrentFile.SaveFile(newFilename);
            }

            if (_callBackFunc != null)
                _callBackFunc(String.Format("转换完毕， 总用时{0} 秒", (Environment.TickCount - tick) / 1000.0));
        }
    }
}
