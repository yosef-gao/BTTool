using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTTool
{
    class BNodeIterator:IIterateable
    {
        private IBNode _firstNode;
        private IBNode _currentNode;
        public BNodeIterator(IBNode rootNode)
        {
 
        }

        public IBNode First()
        {
            return _firstNode;
        }

        public IBNode Next()
        {
            throw new NotImplementedException();
        }

        public IBNode CurrentNode()
        {
            return _currentNode;
        }
    }
}
