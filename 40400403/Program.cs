using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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

            // counts the number of elements in an int array
            Console.WriteLine("No. of elements in file: " + input.Length + "\n");

            // total x and y coordianates in the file
            int totalCoord = input[0] * 2;

            ArrayList nodeList = initNodeList(input, totalCoord);

            int size = (input[0] * input[0]) / 2;
            int start = totalCoord + 1;
            int end = input.Length;

            Matrix matrixList = initMatrix(input, size, start, end);
            
            // displays the nodes
            foreach (Node item in nodeList)
            {
                Console.WriteLine(item.toString());
            }
            Console.WriteLine("\nNumber of nodes: " + nodeList.Count);
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
        
        public static Matrix initMatrix(int[] input, int size, int start, int end)
        {
            int num = 1;
            int column = 0;
            int row = 0;

            Matrix g = new Matrix(size);
            Console.WriteLine("\nArray starts at: " + start);
            Console.WriteLine("Array ends at: " + end + "\n");


            while (start < end)
            {
                if (input[start] == 0)
                {
                    Console.Write(num + " |   ");
                    g.removeEdge(row, column);

                    num++;
                }

                if (input[start] == 1)
                {
                    Console.Write(num + "   |   ");
                    g.addEdge(row, column);

                    num++;
                }

                if (row == size)
                {
                    row = 0;
                    column++;
                }

                start++;
            }
            return g;
        }
    }
}
