using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        public static void matrixDisplay(double[,] matrix, int maxMatrixX, int maxMatrixY, int U)
        {
            Console.WriteLine("u("+U+")=");
            for (int i = 0; i < maxMatrixX; i++)
            {
                for (int j = 0; j < maxMatrixY; j++)
                {
                    Console.Write("\t"+matrix[i, j].ToString() + " ");
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

            double wartosc;
            wartosc = (double)2 / (double)3;

            double[,] weights = new double[3, 3];
            weights[0, 0] = 1;
            weights[0, 1] = -wartosc;
            weights[0, 2] = wartosc;

            weights[1, 0] = -wartosc;
            weights[1, 1] = 0;
            weights[1, 2] = -wartosc;

            weights[2, 0] = wartosc;
            weights[2, 1] = -wartosc;
            weights[2, 2] = 0;

            int bias = 0;
            int[] xInput = new int[3];
            xInput[0] = bias;
            xInput[1] = -1;
            xInput[2] = 1;


            int index = 1;

            
            while (true)
            {
                Console.WriteLine("******Asynchronous step number: " + index + ".*****************");
                double [,] matrix=matrixProduct(weights, xInput);

                if (index % 3 == 1)
                {
                    Console.WriteLine("******Neuron 1.************************************");
                    matrixDisplay(matrix, 3, 1, index);
                    if (matrix[0, 0] > 0)
                    {
                        xInput[0] = 1;
                    }
                    else
                    {
                        xInput[0] = -1;
                    }
                }
                else if (index%3==2)
                {
                    Console.WriteLine("******Neuron 2.************************************");
                    matrixDisplay(matrix, 3, 1,index);
                    if (matrix[1,0]>0)
                    {
                        xInput[1] = 1;
                    }
                    else
                    {
                        xInput[1] = -1;
                    }
                }
                else
                {
                    Console.WriteLine("******Neuron 3.************************************");
                    matrixDisplay(matrix, 3, 1,index);
                    if (matrix[2, 0] > 0)
                    {
                        xInput[2] = 1;
                    }
                    else
                    {
                        xInput[2] = -1;
                    }
                }
                Console.WriteLine("x("+index+ ")=[" + xInput[0] + "," + xInput[1]+","+xInput[2]+"]");
                if (index % 3 == 0)
                {
                    Console.WriteLine("===================================================");
                }
                index++;
                Console.ReadKey();
            }




        }
    }

}
