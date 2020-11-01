using System;
using System.Collections.Generic;
using System.Text;

namespace _40400403
{
    class Node
    {
        private int x;
        private int y;
        private int id;
        private Node lastNode;
        private int totalCost;

        public Node(int xCoordinate, int yCoordinate, int nodeID)
        {
            x = xCoordinate;
            y = yCoordinate;
            id = nodeID;
        }

        public double Distance(Node nextCave)
        {
            return 0;
        }
    }
}
