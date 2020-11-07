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
            // string fileName = "generated30-1";

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


            ArrayList answer = AStarAlgorithm(nodeList, connect, totalNodes);


            /*
                        // displays the nodes
                        foreach (Node item in nodeList)
                        {
                            Console.WriteLine(item.toString());
                        }
                        Console.WriteLine("\nNumber of nodes: " + nodeList.Count);

                        Console.WriteLine();


                        int counter = 1;
                        for (int i = 0; i < totalNodes; i++)
                        {
                            for (int j = 0; j < totalNodes; j++)
                            {
                                Console.WriteLine(counter + "   |   " + "(" + i + ", " + j + ")" + "   |   " + connect.getEdge(i, j));
                                counter++;
                            }
                            Console.WriteLine();
                        }

                        Console.WriteLine();

                        // counts the number of elements in an int array
                        Console.WriteLine("No. of elements in file: " + input.Length);
                        Console.WriteLine("Array starts at: " + start);
                        Console.WriteLine("Array ends at: " + end + "\n");
                        */
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

        public static ArrayList AStarAlgorithm(ArrayList nodeList, Matrix connection, int totalNodes)
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
                    Console.WriteLine("IT WORKS ");
                    return openList;
                }

                // Detect connections new  nodes, add to open list
                for (int i = 0; i < totalNodes; i++)
                {
                    if (connection.getEdge(nodeNum, i))
                    {
                        Node expandNode = (Node)nodeList[i];

                        expandNode.parent = currentNode;

                        if(!(openList.Contains(expandNode) || closeList.Contains(expandNode)))
                        {
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

            return openList;
            // compare to method = f cost of cave 1 to another cave
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
