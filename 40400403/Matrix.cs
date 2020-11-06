using System;
using System.Collections.Generic;
using System.Text;

namespace _40400403
{
    public class Matrix
    {
        Boolean[][] matrix;
        private int arraySize;


        public Matrix(int size)
        {
            this.arraySize = size;
            matrix = new Boolean[size][];

            for (int rows = 0; rows < size; rows++)
            {
                matrix[rows] = new Boolean[size];
            }
        }

        public Matrix LoadMatrix(Matrix empty, int[] input, int start, int end, int totalNodes)
        {
            int column = 0;
            int row = 0;

            while (start <= end)
            {
                if(start == end)
                {
                    break;
                }
                if (row == totalNodes)
                {                    
                    row = 0;
                    column++;
                }

                if (input[start] != 0)
                {
                    empty.removeEdge(row, column);
                }

                if (input[start] == 1)
                {
                    empty.addEdge(row, column);                 
                }

                row++;
                start++;
            }
            return empty;
        }

        // Add edges
        public void addEdge(int i, int j)
        {
            this.matrix[i][j] = true;
        }

        // Remove edges
        public void removeEdge(int i, int j)
        {
            this.matrix[i][j] = false;
        }

        public bool getEdge(int i, int j)
        {
            return matrix[i][j];
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