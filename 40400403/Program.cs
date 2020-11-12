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
            string fileName = null;

            try
            {
                // Read file from the command prompt
                fileName = args[0];
            }
            catch
            {
                Console.WriteLine("Please enter the file name");
                Environment.Exit(1);
            }
            

            // Parse the string array into int array
            int[] input = ReadFile(fileName);

            // total x and y coordianates in the file
            int totalNodes = input[0];
            int totalCoord = input[0] * 2;
            int matrixStart = totalCoord + 1;
            int matrixEnd = input.Length;

            Matrix matrix = new Matrix(totalNodes);
            matrix.LoadMatrix(matrix, input, matrixStart, matrixEnd, totalNodes);

            ArrayList nodeList = initNodeList(input, totalCoord);

            Node answer = AStarAlgorithm(nodeList, matrix, totalNodes);
            ArrayList answerPath = CalculatePath(answer);

            //Console.WriteLine("\nNumber of caves: " + answerPath.Count);
            //Console.WriteLine("Length: " + Math.Round(answer.gScore, 2));

            WriteFile(answerPath, fileName);
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

        private static void WriteFile(ArrayList pathList, string name)
        {
            if ((File.Exists(name + ".csn")))
            {
                File.Delete(name + ".csn");
            }

            //Console.Write("\nPath: ");
            foreach (Node item in pathList)
            {
                // Console.Write(item.id);
                // Console.Write(" ");
                // write file path to a .csn file with the name of the arguments input
                File.AppendAllText(name + ".csn", item.id + " ");
            }
            //Console.WriteLine("\n");
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

        public static Node AStarAlgorithm(ArrayList nodeList, Matrix matrix, int totalNodes)
        {
            ArrayList openList = new ArrayList();
            ArrayList closeList = new ArrayList();

            Node startNode = (Node)nodeList[0];
            Node goalNode = (Node)nodeList[totalNodes - 1];
            Node currentNode = startNode;

            openList.Add(startNode);

            while (!(openList == null))
            {
                 // sort open list by fscore
                openList.Sort(new NodeComparer());

                // set the node with the lowest fscore to the current node being exaimined
                currentNode = (Node)openList[0];

                // when the current node id is equal to the id of the goal node, exit
                if (currentNode.id == goalNode.id)
                {
                    //closeList.Add(currentNode);
                    return (Node)openList[0];
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);

                // Detect new connection nodes, add to open list
                for (int i = 0; i < totalNodes; i++)
                {
                    if (matrix.getEdge(currentNode.id - 1, i))
                    {
                        Node expandNode = (Node)nodeList[i];
                        Node parent = currentNode.parent;

                        if (!(openList.Contains(expandNode) || closeList.Contains(expandNode)))
                        {
                            expandNode.parent = currentNode;
                            expandNode.gScore = currentNode.gScore + expandNode.Distance(currentNode);

                            // Distance from node to goal node
                            expandNode.hScore = expandNode.Distance(goalNode);

                           // Console.WriteLine(currentNode.id + " " + expandNode.id + "      |     " + expandNode.fScore);

                            openList.Add(expandNode); 
                        }
                    }
                }
                
                if (openList.Count == 0)
                {
                    Node noPath = new Node(0, -1, -1);
                    return noPath;
                }
            }
            return null;
        }

        private static ArrayList CalculatePath(Node answer)
        {
            ArrayList pathList = new ArrayList();

            pathList.Add(answer);
            while (!(answer.parent == null))
            {
                pathList.Add(answer.parent);
                answer = answer.parent;
            }
            pathList.Reverse();

            return pathList;
        }
    }
}