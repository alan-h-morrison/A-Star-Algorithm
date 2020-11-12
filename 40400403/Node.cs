using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _40400403
{
    public class Node
    {
        public int id { get; set; }
        public double x { get; set; }
        public double y { get; set; }

        public Node parent { get; set; }
        public double gScore { get; set; }
        public double hScore { get; set; }
        public double fScore 
        {
            set { fScore = value; }
            get { return gScore + hScore; }
        }
       
        public Node(int nodeID, double xCoordinate, double yCoordinate)
        {
            id = nodeID;
            x = xCoordinate;
            y = yCoordinate;
        }

        public double Distance(Node node)
        {
            return Math.Sqrt(Math.Pow(this.x - node.x, 2) + Math.Pow(this.y - node.y, 2));
        }

        public string toString()
        {
            return "id: " + id.ToString() + "\t | " + parent.id.ToString();
        }
    }
}
