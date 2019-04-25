using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        public static void matrixDisplay(double[,] matrix, int maxMatrixX, int maxMatrixY, int U)
        {
            Console.WriteLine("u(" + U + ")=");
            for (int i = 0; i < maxMatrixX; i++)
            {
                for (int j = 0; j < maxMatrixY; j++)
                {
                    Console.Write("\t" + matrix[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }
        public static double[,] matrixProduct(double[,] matrix1, int[] matrix2)
        {
            //mnożenie macierzy przez wektor
            double[,] matrixProduct = new double[3, 1];
            for (int i = 0; i < 3; i++)
            {
                double sum = 0;
                int k = 0;
                for (k = 0; k < 3; k++)
                {
                    sum += matrix1[i, k] * matrix2[i];
                }
                matrixProduct[i, 0] = sum;

            }
            return matrixProduct;
        }
        static void Main(string[] args)
        {

            Console.ReadKey();
        }
    }
}
