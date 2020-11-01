using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40400403
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read file from the command prompt
            string fileName = args[0];
            //string fileName = "generated30-1";
            string file = ReadFile(fileName);

            // Store file as a string array
            string[] inputString = file.Split(',');
            // Parse the string array into int array
            int[] input = Array.ConvertAll(inputString, int.Parse);

            int size = input[0];
            Console.WriteLine(size);
        }

        private static string ReadFile(string name)
        {
            string file = null;
            try
            {
                string filePath = "testfiles/generated30-1/";
                file = File.ReadAllText(filePath + name + ".cav");                
            }
            catch (Exception)
            {
                Console.WriteLine("File is not found!");
            }
            return file;
        }
    }
}