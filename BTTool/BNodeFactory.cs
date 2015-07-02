using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    /// <summary>
    /// BNode工厂类，用于产生需要的IBNode 对象，并把这些BNode记录在list中
    /// </summary>
    class BNodeFactory
    {
        private List<IBNode> _bNodeList;

        public BNodeFactory(List<IBNode> bNodeList)
        {
            this._bNodeList = bNodeList;
        }

        public IBNode GetBNode(char type)
        {
            IBNode node = null;
            switch (type)
            {
                case 'l': // listitem node
                    node = new ListItemNode();
                    break;
                case 'd': // dict node
                    node = new DictNode();
                    break;
                case 'k': // key value node
                    node = new KeyValueNode();
                    break;
            }
            _bNodeList.Add(node);
            return node;
        }
    }
}
