using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        public static void MatrixDisplay(double[] matrix, int U)
        {
            Console.WriteLine("w(" + U + ")=");
            for (int i = 0; i < matrix.Length; i++)
            {
                Console.Write("\t" + matrix[i].ToString() + " ");
                Console.WriteLine();
            }
        }

        public static double[] MatrixTimesVector(double[,] matrix1, double[] matrix2)
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
                matrixProduct[i] = Math.Round(sum, 3);

            }
            return matrixProduct;
        }


        public static bool CompareVerse(double[] verse1, double[] verse2)
        {
            for (int i = 0; i < verse1.Length; i++)
            {
                if (verse1[i] != verse2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool FindCycle(List<double[]> list1, double[] oldValue)
        {
            if (list1[0] != oldValue)
            {
                return false;
            }
            return true;
        }

        public static void CheckList(List<double[]> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (!CompareVerse(list[0], list[i]))
                {
                    Console.WriteLine("\n Find cycle but network does not stabilize!");
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine("\nStable network!");
            Console.ReadKey();
            return;
        }


        //Main
        static void Main(string[] args)
        {

            double value = (double)2 / (double)3;// problem z waga 2/3 i -2/3

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


            double[] xInput = new double[3];
            for (int i = 0; i < 3; i++)//Dodawanie wektora z klawiatury
            {
                Console.WriteLine("The value of the vector on the index: "+i);
                string x = Console.ReadLine();
                if (x=="1"||x=="-1")
                {
                    xInput[i] = Convert.ToDouble(x);
                }
                else
                {
                    Console.WriteLine("Bad value!");
                    i--;
                }
            }
            List<double[]> listOutputX0 = new List<double[]>();//Aktualna lista
            List<double[]> listOutputX1 = new List<double[]>();//Lista wektorow z poprzednich iteracji


            int index = 0;
            int indexGlobal = 1;

            while (true)
            {
                Console.WriteLine("******Asynchronous step number: " + indexGlobal + "*****************");
                double [] matrix= MatrixTimesVector(weights, xInput);

                if (index==0)
                {
                    Console.WriteLine("******Neuron 1.************************************");
                    MatrixDisplay(matrix, indexGlobal);
                    if (matrix[0] > 0)
                    {
                        xInput[0] = 1;
                    }
                    else
                    {
                        xInput[0] = -1;
                    }
                }
                else if (index==1)
                {
                    Console.WriteLine("******Neuron 2.************************************");
                    MatrixDisplay(matrix, indexGlobal);
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
                    MatrixDisplay(matrix, indexGlobal);
                    if (matrix[2] > 0)
                    {
                        xInput[2] = 1;
                    }
                    else
                    {
                        xInput[2] = -1;
                    }
                }
                listOutputX0.Add(new double[] { xInput[0], xInput[1], xInput[2] });

                Console.WriteLine("x("+ indexGlobal + ")=[" + xInput[0] + "," + xInput[1]+","+xInput[2]+"]");

                if (indexGlobal > 3  && CompareVerse(listOutputX0[index], listOutputX1[index]))
                {
                    listOutputX1.AddRange(listOutputX0);
                    listOutputX1.RemoveRange(0, index);
                    CheckList(listOutputX1);
                    break;
                }

                index++;
                indexGlobal++;
                if (index==3)
                {
                    listOutputX1 = listOutputX0;
                    listOutputX0 = new List<double[]>();
                    index = 0;
                    Console.WriteLine("===================================================");
                }
                
                Console.ReadKey();
            }
        }
    }

}
