using System;
using System.Collections.Generic;
using System.Text;

namespace _40400403
{
    class Node
    {
        public int id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        
        private Node lastNode;
        private int totalCost;

        public Node(int nodeID, int xCoordinate, int yCoordinate)
        {
            id = nodeID;
            x = xCoordinate;
            y = yCoordinate;
            
        }


        public double Distance(Node nextCave)
        {
            return 0;
        }

        public string toString()
        {
            return "id: " + id.ToString() + "\t | (" + x.ToString() + "," + y.ToString() + ")";
        }
    }
}
