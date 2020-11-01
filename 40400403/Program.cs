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

            int size = input[0] * 2;
            int nodeID = 1;
            ArrayList nodeList = new ArrayList();

            int idx = 1;
            while(idx < size)
            {
                int xCoordinate = input[idx];
                int yCoordinate = input[idx + 1];

                Node node = new Node(nodeID, xCoordinate, yCoordinate);
                nodeList.Add(node);

                nodeID++;
                idx = idx + 2;
            }

            foreach(Node item in nodeList)
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
    }
}