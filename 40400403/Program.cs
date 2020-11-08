using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace _40400403
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read file from the command prompt
            string fileName = args[0];

            // Parse the string array into int array
            int[] input = ReadFile(fileName);

            // total x and y coordianates in the file
            int totalNodes = input[0];
            int totalCoord = input[0] * 2;
            int start = totalCoord + 1;
            int end = input.Length;

            Matrix connect = new Matrix(totalNodes);
            connect.LoadMatrix(connect, input, start, end, totalNodes);

            ArrayList nodeList = initNodeList(input, totalCoord);

            Node answer = AStarAlgorithm(nodeList, connect, totalNodes);
            ArrayList answerPath = CalculatePath(answer);

            WriteFile(answerPath, fileName);
        }

        private static ArrayList CalculatePath(Node answer)
        {
            ArrayList pathList = new ArrayList();

            pathList.Add(answer);
            while(!(answer.parent == null))
            {
                pathList.Add(answer.parent);
                answer = answer.parent;
            }
            pathList.Reverse();

            return pathList;
        }

        private static void WriteFile(ArrayList pathList, string name)
        {
            if((File.Exists(name + ".csn")))
            {
                File.Delete(name + ".csn");
            }
                Console.Write("\nPath: ");
                foreach(Node item in pathList)
                {
                    Console.Write(item.id);
                    Console.Write(" ");
                    // write file path to a .csn file with the name of the arguments input
                    File.AppendAllText(name + ".csn", item.id + " ");
                }
                Console.WriteLine("\n");
        }
        private static int[] ReadFile(string name)
        {
            string file = null;
            string[] inputString = null;
            int[] input = null;

            try
            {
                file = File.ReadAllText(name + ".cav");
                // Store file as a string array
                inputString = file.Split(',');

                // Parse the string array into int array
                input = Array.ConvertAll(inputString, int.Parse);
            }
            catch (Exception)
            {
                Console.WriteLine("File is not found!");
                Environment.Exit(1);
            }
            return input;
        }

        public static Node AStarAlgorithm(ArrayList nodeList, Matrix connection, int totalNodes)
        {
            ArrayList openList = new ArrayList();
            ArrayList closeList = new ArrayList();

            Node startNode = (Node)nodeList[0];
            Node goalNode = (Node)nodeList[totalNodes - 1];
            Node currentNode = startNode;
            Node parent = null;

            int nodeNum = 0;
            openList.Add(startNode);

            while (!(openList == null))
            {
                if(openList.Count == 0)
                {
                    Console.WriteLine("fail");
                    return null;
                }
                // sort open list by fscore
                openList.Sort(new myComparer());

                // set the node with the lowest fscore to the current node being exaimined
                currentNode = (Node)openList[0];
                nodeNum = currentNode.id - 1;

                // when the current node id is equal to the id of the goal node, exit
                if (currentNode.id == goalNode.id)
                {
                    //closeList.Add(currentNode);
                    return (Node)openList[0];
                }

                // Detect connections new  nodes, add to open list
                for (int i = 0; i < totalNodes; i++)
                {
                    if (connection.getEdge(nodeNum, i))
                    {
                        Node expandNode = (Node)nodeList[i];
                        
                        

                        if(!(openList.Contains(expandNode) || closeList.Contains(expandNode)))
                        {
                            expandNode.parent = currentNode;
                            openList.Add(expandNode);
                        }
                    }
                }
                
                openList.Remove(currentNode);
                closeList.Add(currentNode);

                foreach(Node node in openList)
                {
                        parent = node.parent;

                        node.distanceParent = node.Distance(parent);
                        node.gScore = node.gScore + node.distanceParent;
                        // Distance from node to goal node
                        node.hScore = node.Distance(goalNode);
                }
            }

            return null;
        }

        public class myComparer : IComparer
        {
            int IComparer.Compare(object xx, object yy)
            {
                Node x = (Node)xx;
                Node y = (Node)yy;

                return x.fScore.CompareTo(y.fScore);
            }
        }


        public static ArrayList initNodeList(int[] input, int totalCoord)
        {
            // Used to store nodes created
            ArrayList nodeList = new ArrayList();

            // intialise the id of the first node
            int nodeID = 1;

            // iterates through all x and y coordinates and creates a node for each pair
            int idx = 1;
            while (idx < totalCoord)
            {
                int xCoordinate = input[idx];
                int yCoordinate = input[idx + 1];
                Node node = new Node(nodeID, xCoordinate, yCoordinate);
                nodeList.Add(node);

                nodeID++;
                idx = idx + 2;
            }

            return nodeList;
        }
    }
}
