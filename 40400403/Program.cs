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
            //read in caves
            string fileName = args[0];
            string filePath = "testfiles/generated30-1/";

            string file = System.IO.File.ReadAllText(filePath + fileName + ".cav");

            Console.WriteLine(file);

            //TextWriter output = new StreamWriter("testfiles/" + fileName + ".csv");
            //output.WriteLine(file);
        }
    }
}