using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        public static void matrixDisplay(double[] matrix, int maxMatrixX, int U)
        {
            Console.WriteLine("u("+U+")=");
            for (int i = 0; i < maxMatrixX; i++)
            {
                Console.Write("\t"+matrix[i].ToString() + " ");
                Console.WriteLine();
            }
        }
        public static double[] matrixProduct(double[,] matrix1, int[] matrix2)
        {
            //mnożenie macierzy przez wektor
            double[] matrixProduct = new double[3];
            for (int i = 0; i < 3; i++)
            {
                double sum = 0;
                int k = 0;
                for (k = 0; k < 3; k++)
                {
                    sum += matrix1[i, k] * matrix2[i];
                }
                matrixProduct[i] = Math.Round(sum,3);

            }
            return matrixProduct;
        }

        static void Main(string[] args)
        {

            double value;
            value = (double)2 / (double)3;// problem z waga 2/3 i -2/3

            double[,] weights = new double[3, 3];
            weights[0, 0] = 0;
            weights[0, 1] = -value;
            weights[0, 2] =value;//+

            weights[1, 0] = -value;
            weights[1, 1] = 0;
            weights[1, 2] = -value;

            weights[2, 0] = value;//+
            weights[2, 1] = -value;
            weights[2, 2] = 0;


            int[] xInput = new int[3];
            for (int i = 0; i < 3; i++)//Dodawanie wektora z klawiatury
            {
                Console.WriteLine("The value of the vector on the index: "+i);
                string x = Console.ReadLine();
                if (x=="1"||x=="-1")
                {
                    xInput[i] = Convert.ToInt32(x);
                }
                else
                {
                    Console.WriteLine("Bad value!");
                    i--;
                }
            }
            List<int[]> listOutputX0 = new List<int[]>();//Aktualna lista
            List<int[]> listOutputX1 = new List<int[]>();//Lista wektorow z poprzednich trzech iteracji



            int index = 1;

            
            while (true)
            {
                Console.WriteLine("******Asynchronous step number: " + index + "*****************");
                double [] matrix=matrixProduct(weights, xInput);

                if (index % 3 == 1)
                {
                    Console.WriteLine("******Neuron 1.************************************");
                    matrixDisplay(matrix, 3,  index);
                    if (matrix[0] > 0)
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
                    matrixDisplay(matrix, 3,index);
                    if (matrix[1]>0)
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
                    matrixDisplay(matrix, 3, index);
                    if (matrix[2] > 0)
                    {
                        xInput[2] = 1;
                    }
                    else
                    {
                        xInput[2] = -1;
                    }
                }
                listOutputX0.Add(xInput);
                Console.WriteLine("x("+index+ ")=[" + xInput[0] + "," + xInput[1]+","+xInput[2]+"]");
                if (index % 3 == 0)
                {
                    if (index>3 && listOutputX1 == listOutputX0)//Sprawdza czy sieć stabilizuje się
                    {
                        Console.WriteLine("Stable network!");
                        Console.ReadKey();
                        break;

                    }
                    else
                    {
                        listOutputX1 = listOutputX0;
                        listOutputX0 = new List<int[]>();
                    }
                    Console.WriteLine("===================================================");
                }
                index++;
                Console.ReadKey();
            }




        }
    }

}
