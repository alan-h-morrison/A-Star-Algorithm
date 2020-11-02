using System;
using System.Collections.Generic;
using System.Text;

namespace _40400403
{
    public class Matrix
    {
        bool[,] matrix;
        private int numEdges;

        public Matrix(int numEdges)
        {
            this.numEdges = numEdges;
            matrix = new bool[numEdges, numEdges];
        }

        // Add edges
        public void addEdge(int i, int j)
        {
            matrix[i, j] = true;
            // matrix[j, i] = true;

            Console.WriteLine(matrix[i, j]);
        }

        // Remove edges
        public void removeEdge(int i, int j)
        {
            matrix[i, j] = false;
            // matrix[j, i] = false;

            Console.WriteLine(matrix[i, j]);
        }

        public bool getEdge(int i, int j)
        {
            return matrix[i, j];
        }
    }
}


// Print the matrix
/*
public String toString()
{
    StringBuilder s = new StringBuilder();
    for (int i = 0; i < numVertices; i++)
    {
        s.append(i + ": ");
        for (boolean j : matrix[i])
        {
            s.append((j ? 1 : 0) + " ");
        }
        s.append("\n");
    }
    return s.toString();
}
*/