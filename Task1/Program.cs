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
                    sum += matrix1[i, k] * matrix2[k];
                }
                matrixProduct[i] = Math.Round(sum,3);

            }
            return matrixProduct;
        }
        public static bool compareList(List<int[]>list1, List<int[]> list2)
        {
            bool returnValue = true;
            for (int i = 0; i < list1.Count; i++)
            {
                for (int k = 0; k < list1[i].Length; k++)
                {
                    if (list1[i][k]!= list2[i][k])
                    {
                        returnValue = false;
                    }
                }
            }

            return returnValue;
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
            int neuron = 0;
            
            while (true)
            {
                Console.WriteLine("******Asynchronous step number: " + index + "*****************");
                double [] matrix=matrixProduct(weights, xInput);

                if (index % 3 == 1)
                {
                    neuron = 1;
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
                    neuron = 2;
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
                    neuron = 3;
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
                listOutputX0.Add(new int[] { xInput[0], xInput[1], xInput[2] });
                if (listOutputX0.Count > 3)
                {
                    listOutputX0.RemoveAt(0);
                }

                Console.WriteLine("x("+index+ ")=[" + xInput[0] + "," + xInput[1]+","+xInput[2]+"]");

                if (index > 3 && compareList(listOutputX0,listOutputX1))//Sprawdza czy sieć stabilizuje się
                {
                    Console.WriteLine("\nStable network!");
                    Console.ReadKey();
                    break;

                }
                else
                {
                    listOutputX1.Add(new int[] { xInput[0], xInput[1], xInput[2] });
                    if (listOutputX1.Count>3)
                    {
                        listOutputX1.RemoveAt(0);
                    }
                }
                if (index % 3 == 0)
                {

                    Console.WriteLine("===================================================");
                }
                index++;
                //Console.ReadKey();
            }




        }
    }

}
