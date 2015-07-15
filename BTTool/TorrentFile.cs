using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTTool
{
    /// <summary>
    /// 抽象BT文件，做为IBNode和客户端之间的桥梁，减少客户端的不必要依赖
    /// 把一些公共的代码提取出来放在这里
    /// 同时也是为了优化性能
    /// </summary>
    class TorrentFile
    {
        private TreeNode _tRootNode = null;
        private IAnalyser _btAnalyser = null;
        private IBNode _bRootNode = null;
        private List<TreeNode> _treeNodeList = null;

        private void ConstructTree(TreeNode tParent, IBNode bParent)
        {
            tParent.Text = bParent.ToString();
            bParent.Child.ForEach(bNode =>
            {
                TreeNode tNode = new TreeNode();
                _treeNodeList.Add(tNode);
                tNode.Text = bNode.ToString();
                tParent.Nodes.Add(tNode);
                ConstructTree(tNode, bNode);
            });
        }

        public TreeNode RootNode 
        { 
            get
            {
                if (_bRootNode == null)
                    return null; // 还未打开BT文件

                // 只构建一次，第二次直接返回已有的树就可以了
                if (_tRootNode == null)
                {
                    _tRootNode = new TreeNode();
                    _treeNodeList = new List<TreeNode>(); // 对应的TreeNode List
                    _treeNodeList.Add(_tRootNode);
                    ConstructTree(_tRootNode, _bRootNode);
                }

                return _tRootNode;
            }
        }
        
        /// <summary>
        /// 读入BT文件，并解析，但不生成树
        /// </summary>
        /// <param name="filename">bt文件名</param>
        public void OpenFile(string filename)
        {
            _btAnalyser = new CommonAnalyser();
            _tRootNode = null;

            // 读入BT文件
            byte[] buffer = null;
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
            }

            // 解析
            try
            {
                _bRootNode = this._btAnalyser.Analyse(buffer);
            }
            catch { throw; }

            _tRootNode = null; // 重置标志
        }

        /// <summary>
        /// 保存BT文件
        /// </summary>
        /// <param name="filename">文件保存路径</param>
        public void SaveFile(string filename)
        {
            if (_bRootNode == null)
                return;
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                byte[] buffer = _bRootNode.ToBytes();
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        public void Modify()
        {
            if (_bRootNode == null)
                return;

            KeyValueVisitor visitor = new KeyValueVisitor();
            var bNodeList = _btAnalyser.BNodeList;
            for (int i = 0; i < bNodeList.Count; ++i)
            {
                var node = bNodeList[i];
                // 只需要更新KeyValueNode的值
                if (node is KeyValueNode)
                {
                    node.Accept(visitor);
                }
            }

            _tRootNode = null;
        }
    }
}
