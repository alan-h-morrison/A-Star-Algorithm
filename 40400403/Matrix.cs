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

            for (int i = 0; i < size; i++)
            {
                matrix[i] = new Boolean[size];
            }
        }

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