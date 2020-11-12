using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _40400403
{
    public class NodeComparer : IComparer
    {
        int IComparer.Compare(object firstNode, object secondNode)
        {
            Node first = (Node)firstNode;
            Node second = (Node)secondNode;

            return first.fScore.CompareTo(second.fScore);
        }
    }
}
