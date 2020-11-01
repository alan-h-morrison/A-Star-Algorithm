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
            //string fileName = args[0];
            string fileName = "generated30-1";

            // Parse the string array into int array
            int[] input = ReadFile(fileName);

            Console.WriteLine("No. of elements in file: " + input.Length + "\n");
 
            int nodeID = 1;
            int totalCoord = input[0] * 2;
            
            ArrayList nodeList = new ArrayList();

            int idx = 1;
            while(idx < totalCoord)
            {
                int xCoordinate = input[idx];
                int yCoordinate = input[idx + 1];

                Node node = new Node(nodeID, xCoordinate, yCoordinate);
                nodeList.Add(node);

                nodeID++;
                idx = idx + 2;
            }

            int matrixSize = (input[0] * input[0]) /2;
            int start = (totalCoord + 2);
            int end = matrixSize + start;

            /*
            Graph g = new Graph(matrixSize);
            Console.WriteLine("\nArray starts at: " + start);
            Console.WriteLine("Array ends at: " + end + "\n");

            int row = 1;
            while (start <= end)
            {
                if (input[start] == 0 || input[start + 1] == 0)
                {
                    g.removeEdge(input[start] , row);
                }

                if (input[start] == 1 || input[start + 1] == 1)
                {
                    g.addEdge(input[start], row);
                }

                start = start + 2;
            }

            Console.WriteLine("row: " + row);
            foreach (Node item in nodeList)
            {
                Console.WriteLine(item.toString());
            }


            Console.WriteLine("\nNumber of nodes: " + nodeList.Count);
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
    }
}


            /*
            int index = 1;
            foreach(int item in input)
            {
                Console.WriteLine(index + "    |    " + item);
                index++;
            }
            */