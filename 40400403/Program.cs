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

            // try-catch loop that makes sure that the user has inputed an argument into the command line
            try
            {
                // Read file from the command prompt
                fileName = args[0];
            }
            catch
            {
                // Print error to screen and stop the execution of the program
                Console.WriteLine("Please enter the file name");
                Environment.Exit(1);
            }
            
            // Parse the string array into int array
            int[] input = ReadFile(fileName);

            int totalNodes = input[0];
            int totalCoord = input[0] * 2;
            int matrixStart = totalCoord + 1;
            int matrixEnd = input.Length;

            // Intialise the adjacency matrix and use values from the input array to load the correct values
            Matrix matrix = new Matrix(totalNodes);
            matrix.LoadMatrix(matrix, input, matrixStart, matrixEnd, totalNodes);

            // Store all the nodes in the array as an array list
            ArrayList nodeList = initNodeList(input, totalCoord);

            // 
            Node answer = AStarAlgorithm(nodeList, matrix, totalNodes);
            ArrayList answerPath = CalculatePath(answer);

            WriteFile(answerPath, fileName);

            //Console.WriteLine("\nNumber of caves: " + answerPath.Count);
            Console.WriteLine("Length: " + Math.Round(answer.gScore, 2));
        }

        // Method - reads a file and returns a int array
        private static int[] ReadFile(string name)
        {
            string file = null;
            string[] inputString = null;
            int[] input = null;

            // try-catch statment that makes sure the file exists in the folder
            try
            {
                // Read file in as a string
                file = File.ReadAllText(name + ".cav");

                // Split string and store as a string array
                inputString = file.Split(',');

                // Parse the string array into int array
                input = Array.ConvertAll(inputString, int.Parse);
            }
            catch (Exception)
            {
                // Print error to screen and stop the execution of the program
                Console.WriteLine("File is not found!");
                Environment.Exit(1);
            }
            return input;
        }

        // Method - that takes the answer path and writes it to a .csn file
        private static void WriteFile(ArrayList pathList, string name)
        {
            // if the file already exists it's deleted 
            if ((File.Exists(name + ".csn")))
            {
                File.Delete(name + ".csn");
            }

            //Console.Write("\nPath: ");

            // Loops thorugh each node in order and appends them to the correct file
            foreach (Node item in pathList)
            {
                // Console.Write(item.id);
                // Console.Write(" ");

                // Writes node path to a .csn file with the name of the argument input
                File.AppendAllText(name + ".csn", item.id + " ");
            }

            //Console.WriteLine("\n");
        }

        // Method - Intialise the array list containing the nodes and the relevant information about each one (x coordinate, y coordinate, id, etc.)
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

        // Method A* algorithm that finds a path from the start node to the goal node.
        public static Node AStarAlgorithm(ArrayList nodeList, Matrix matrix, int totalNodes)
        {
            // A open list that contains all nodes that can be examined
            ArrayList openList = new ArrayList();
            // A node list that contains nodes that have been examined
            ArrayList closeList = new ArrayList();

            // First node is set and added to the open list
            Node startNode = (Node)nodeList[0];
            openList.Add(startNode);

            // Goal node is set to the last node in the node list
            Node goalNode = (Node)nodeList[totalNodes - 1];

            // Current node being examined is set to the start node
            Node currentNode = startNode;

            
            // Checks that the open list is not empty
            while (!(openList == null))
            {
                // Sort open list by fscore
                openList.Sort(new NodeComparer());

                // Set the node with the lowest fscore to the current node being exaimined
                currentNode = (Node)openList[0];

                // When the current node id is equal to the id of the goal node, return the first node in the open list (goal node)
                if (currentNode.id == goalNode.id)
                {
                    return (Node)openList[0];
                }

                // Detect new connection nodes, add them to open list
                // Loop through each node
                for (int i = 0; i < totalNodes; i++)
                {
                    // Checks if a connection exists between every node and the current node
                    if (matrix.getEdge(currentNode.id - 1, i))
                    {
                        Node expandNode = (Node)nodeList[i];

                        // If the expanded node already exists in either the open or closed list it is skipped
                        if (!(openList.Contains(expandNode) || closeList.Contains(expandNode)))
                        {
                            // Calcuates the g score for the expanded node - 
                            expandNode.parent = currentNode;
                            expandNode.gScore = currentNode.gScore + expandNode.Distance(currentNode);

                            // Distance from node to goal node
                            expandNode.hScore = expandNode.Distance(goalNode);

                            //Console.WriteLine(currentNode.id + " " + expandNode.id + "      |     " + expandNode.fScore);

                            // Adds expanded to the open list so that it can be expanded
                            openList.Add(expandNode); 
                        }
                    }
                }
                // Since the node is examined it is removed from the open list and added to the closed list 
                openList.Remove(currentNode);
                closeList.Add(currentNode);

                // If there are no values in the open list, the algorithm is unable to find a path to the goal node and return 0
                if (openList.Count == 0)
                {
                    Node noPath = new Node(0, -1, -1);
                    return noPath;
                }
            }
            return null;
        }

        // Method - Takes the goal node and uses parent nodes to find path to start node
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