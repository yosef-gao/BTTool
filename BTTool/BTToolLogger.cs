using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 日志记录
    /// </summary>
    class BTToolLogger
    {
        private static long startTime;
        public static string Start(char type, string extraMessage = null)
        {
            string retString = null;
            switch (type)
            {
                case 'f': // 读文件
                    retString = String.Format("[{0}][BDecode]Start:{1}", DateTime.Now.ToLongTimeString(), extraMessage);
                    break;
                case 's': // 更新树状图
                    retString = String.Format("[{0}][TreeView Fill]Start", DateTime.Now.ToLongTimeString());
                    break;
                case 'r': // 单文件智能转换
                    retString = String.Format("[{0}][Auto Replace]Start", DateTime.Now.ToLongTimeString());
                    break;
                case 'b': // 批量智能转换
                    retString = String.Format("[{0}][Batch Conversion]Start", DateTime.Now.ToLongTimeString());
                    break;
                case 'e': // 错误
                    retString = String.Format("[{0}][Error]Failed to decode the file \"{1}\"", DateTime.Now.ToLongTimeString(), extraMessage);
                    break;
                default:
                    break;
            }
            startTime = Environment.TickCount;
            return retString;
        }

        public static string End(char type)
        {
            string retString = null;
            double span = (Environment.TickCount - startTime) / 1000.0;
            switch (type)
            {
                case 'f': // 读文件
                    retString = String.Format("[{0}][BDecode]End:({1} seconds)", DateTime.Now.ToLongTimeString(), span);
                    break;
                case 's': // 更新树状图
                    retString = String.Format("[{0}][TreeView Fill]End:({1} seconds)", DateTime.Now.ToLongTimeString(), span);
                    break;
                case 'r': // 单文件智能转换
                    retString = String.Format("[{0}][Auto Replace]End:({1} seconds)", DateTime.Now.ToLongTimeString(), span);
                    break;
                case 'b': // 批量智能转换
                    retString = String.Format("[{0}][Batch Conversion]End:({1} seconds)", DateTime.Now.ToLongTimeString(), span);
                    break;
                default:
                    break;
            }
            return retString + Environment.NewLine;
        }
    }
}
