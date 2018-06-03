using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.BusinessLogic
{
    class Matrix
    {
        private double[,] array;
        public int Rows { get; set; }
        public int Columns { get; set; }

        private Matrix()
        {

        }

        //Use ConvertArrayToMatrix() in order to make Matrix objects.
        private Matrix(double[,] array, int rows = 0, int cols = 0)
        {
            this.array = array;
            this.Rows = rows;
            this.Columns = cols;
        }

        public static double[,] MultiplyMatrices(Matrix matrixA, Matrix matrixB)
        {
            if (matrixA.Columns != matrixB.Rows)
            {
                //throw new InvalidOperationException();
                return new double[,] { {0.0D} };
            }

            int resultMatrixRows = matrixA.Rows;
            int resultMatrixColumns = matrixB.Columns;

            double[,] result = new double[resultMatrixRows, resultMatrixColumns];

            //The idea of how to make it was taken from here: bg.wikipedia.org/Matrices article.
            for (int k = 0; k < matrixA.Columns; k++)
            {
                for (int i = 0; i < resultMatrixRows; i++)
                {
                    for (int j = 0; j < resultMatrixColumns; j++)
                    {
                        result[i, j] += (matrixA.array[i, k] * matrixB.array[k, j]);
                    }
                }
            }

            return result;

        }

        public static double[,] TransponceMatrix(Matrix matrix)
        {
            double[,] transponnedArray = new double[matrix.Columns, matrix.Rows];

            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    transponnedArray[i, j] = matrix.array[j, i];
                }
            }

            return transponnedArray;
        }

        public static double[,] Inverse2By2Matrix(Matrix matrix)
        {
            int determinant = (int)FindDeterminantOf2By2Matrix(matrix);

            if (determinant == 0)
            {
                //throw new InvalidOperationException();
                return new double[,] { { 0.0D } };
            }

            int[,] adjustedQuantitiesArray = new int[,]
            {
                {
                    (int)(Math.Pow(-1, 2) * matrix.array[1, 1]),
                    (int) (Math.Pow(-1,3) * matrix.array[0, 1])
                },
                {
                    (int) (Math.Pow(-1,3) * matrix.array[1, 0]),
                    (int) (Math.Pow(-1,4) * matrix.array[0, 0])
                }

            };

            double[,] inversedMatrix = new double[2, 2];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    inversedMatrix[i, j] = adjustedQuantitiesArray[i, j] * (1.0 / determinant);
                }
            }

            return inversedMatrix;
        }

        private static double FindDeterminantOf2By2Matrix(Matrix matrix)
        {
            return (matrix.array[0, 0] * matrix.array[1, 1]) - (matrix.array[0, 1] * matrix.array[1, 0]);
        }

        public static double SumOfElementsOf1ColumnedMatrices(double[] values)
        {
            double sum = 0.0D;

            foreach (var item in values)
            {
                sum += item;
            }

            return sum;
        }

        public static Matrix ConvertArrayToMatrix(double[,] array, int rows, int columns)
        {
            return new Matrix(array, rows, columns);
        }
    }
}
