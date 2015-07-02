using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// 用于修改特定键值对节点的值
    /// </summary>
    class KeyValueVisitor:IVisitor
    {
        private string[] tabooString = { "name", "name.utf-8", "path", "path.utf-8", "comment", "comment.utf-8",
                                       "publisher", "publisher-url", "publisher-url.utf-8", "publisher.utf-8"};
        public void Visit(KeyValueNode keyValueNode)
        {
            string key = keyValueNode.Key;
            foreach (string name in tabooString)
            {
                if (key.Equals(name))
                {
                    // 普通键值对
                    if (keyValueNode.Child.Count == 0)
                        keyValueNode.SetValue(Encoding.UTF8.GetBytes("somename"));
                    else // 列表项，通常是文件名
                    {
                        string value = ((ListItemNode)keyValueNode.Child[0]).Value;
                        int startIndex = value.LastIndexOf(".");
                        value = String.Format("{0}.{1}", "somename", value.Substring(startIndex+1));
                        ((ListItemNode)keyValueNode.Child[0]).SetValue(Encoding.UTF8.GetBytes(value));
                    }
                    break;
                }
            }
        }
    }
}
