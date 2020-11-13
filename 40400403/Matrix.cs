using System;
using System.Collections.Generic;
using System.Text;

namespace _40400403
{
    public class Matrix
    {
        Boolean[][] matrix;
        private int arraySize;

        // Constructor
        public Matrix(int size)
        {
            this.arraySize = size;
            matrix = new Boolean[size][];

            for (int i = 0; i < arraySize; i++)
            {
                matrix[i] = new Boolean[size];
            }
        }

        // Method - Finds the connection between each node
        public Matrix LoadMatrix(Matrix matrix, int[] input, int start, int end, int totalNodes)
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

                if (input[start] == 0)
                {
                    matrix.removeEdge(row, column);
                }

                if (input[start] == 1)
                {
                    matrix.addEdge(row, column);                 
                }

                row++;
                start++;
            }
            return matrix;
        }

        // Method - Add edge
        public void addEdge(int i, int j)
        {
            this.matrix[i][j] = true;
        }

        // Method - Remove edge
        public void removeEdge(int i, int j)
        {
            this.matrix[i][j] = false;
        }

        // Method - return if there is a connection between two nodes
        public bool getEdge(int i, int j)
        {
            return matrix[i][j];
        }
        
    }
}