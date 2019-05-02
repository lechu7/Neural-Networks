using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        public static void matrixDisplay(double[] matrix, int U)
        {
            Console.WriteLine("w(" + U + ")=");
            for (int i = 0; i < matrix.Length; i++)
            {
                Console.Write("\t" + matrix[i].ToString() + " ");
                Console.WriteLine();
            }
        }
        public static double scalarMultiplication(double[] matrix1, double[] matrix2)
        {
            //mnożenie macierzy skalarne
            double sum = 0;
            for (int i = 0; i < matrix1.Length; i++)
            {
                sum += matrix1[i] * matrix2[i];

            }
            return sum;
        }
        public static double[] matrixProduct(double[] matrix1, double[] matrix2)
        {
            //mnożenie macierzy 
            double[] returnTable = new double[matrix1.Length];
            for (int i = 0; i < matrix1.Length; i++)
            {
                returnTable[i] = matrix1[i] * matrix2[i];

            }
            return returnTable;
        }
        public static double[] matrixSum(double[] matrix1, double[] matrix2)
        {
            //dodawanie macierzy
            double[] sum = new double[matrix1.Length];
            for (int i = 0; i < matrix1.Length; i++)
            {
                sum[i] = matrix1[i] + matrix2[i];

            }
            return sum;
        }
        public static double[] matrixSumDouble(double x, double[] matrix2)
        {
            //dodawanie double macierzy - liczba
            double[] sum = new double[3];
            for (int i = 0; i < 3; i++)
            {
                sum[i] = x + matrix2[i];

            }
            return sum;
        }
        public static double[] matrixMultiplier(double multipler, double[] matrix2)
        {
            //mnożenie macierzy przez liczbe
            double[] returnTable = new double[matrix2.Length];
            for (int i = 0; i < matrix2.Length; i++)
            {
                returnTable[i] = matrix2[i] * multipler;

            }
            return returnTable;
        }
        public static bool compareList(List<double[]> list1, List<double[]> list2)
        {
            bool returnValue = true;
            for (int i = 0; i < list1.Count; i++)
            {
                for (int k = 0; k < list1[i].Length; k++)
                {
                    if (list1[i][k] != list2[i][k])
                    {
                        returnValue = false;
                    }
                }
            }

            return returnValue;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Which parameters you choose (i/ii/c[xor])?");
            string parameters = Console.ReadLine();

            double ro;
            double[] weights = new double[] { 0, 0, 0 };
            ro = 0;
            bool xor = false;
            switch (parameters)
            {
                case "i":
                    ro = 1;
                    weights = new double[] { 1, 1, 1 };
                    break;
                case "ii":
                    ro = 0.1;
                    weights = new double[] { -0.12, 0.4, 0.65 };
                    break;
                case "c":
                    ro = 1;
                    weights = new double[] { 1, 1};
                    xor = true;
                    break;
                default:
                    Console.WriteLine("Invalid parameters provided.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }
            //Wektory trenujące
            double[] trainsX1 = new double[] { 1, 0, 0 };
            double[] trainsX2 = new double[] { 1, 0, 1 };
            double[] trainsX3 = new double[] { 1, 1, 0 };
            double[] trainsX4 = new double[] { 1, 1, 1 };
            if (xor==true)
            {
                trainsX1 = new double[] { 0, 0};
                trainsX2 = new double[] { 0, 1 };
                trainsX3 = new double[] { 1, 0 };
                trainsX4 = new double[] { 1, 1 };
            }
            List<double[]> trains = new List<double[]>();//lista wektorow trenujacych
            trains.Add(trainsX1);
            trains.Add(trainsX2);
            trains.Add(trainsX3);
            trains.Add(trainsX4);
            double dX1 = 0;
            double dX2 = 0;
            double dX3 = 1;
            double dX4 = 0;
            if (xor==true)
            {
                dX1 = 0;
                dX2 = 1;
                dX3 = 1;
                dX4 = 0;
            }
            List<double> d = new List<double>();//lista d
            d.Add(dX1);
            d.Add(dX2);
            d.Add(dX3);
            d.Add(dX4);

            List<double[]> listOutputX0 = new List<double[]>();//Aktualna lista
            List<double[]> listOutputX1 = new List<double[]>();//Lista wektorow z poprzednich trzech iteracji
            if (xor == false)
            {
                listOutputX0.Add(new double[] { 0, 0, 0 });
                listOutputX0.Add(new double[] { 0, 0, 0 });
                listOutputX0.Add(new double[] { 0, 0, 0 });
                listOutputX0.Add(new double[] { 0, 0, 0 });

                listOutputX1.Add(new double[] { 0, 0, 0 });
                listOutputX1.Add(new double[] { 0, 0, 0 });
                listOutputX1.Add(new double[] { 0, 0, 0 });
                listOutputX1.Add(new double[] { 0, 0, 0 });
            }
            else
            {
                listOutputX0.Add(new double[] { 0, 0 });
                listOutputX0.Add(new double[] { 0, 0 });
                listOutputX0.Add(new double[] { 0, 0 });
                listOutputX0.Add(new double[] { 0, 0 });

                listOutputX1.Add(new double[] { 0, 0 });
                listOutputX1.Add(new double[] { 0, 0});
                listOutputX1.Add(new double[] { 0, 0});
                listOutputX1.Add(new double[] { 0, 0 });
            }

            double wx;
            int indexGlobal = 0;
            int index = 0;
            Console.WriteLine();
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("*****Training vector: " + (index + 1) + ", iteration: " + (indexGlobal + 1) + "*****");
                wx = scalarMultiplication(weights, trains[index]);
                Console.WriteLine("y(" + indexGlobal + ")=" + wx);
                if (wx > 0)
                {
                    wx = 1;
                }
                else
                {
                    wx = 0;
                }

                //weights(x+1)=weights(x)+ro(d(x)-y(x))*x(x)
                weights = matrixSum(matrixMultiplier((ro * (d[index] - wx)), weights), trains[index]);
                matrixDisplay(weights, indexGlobal);
                if (xor == false)
                {
                    listOutputX0[index] = (new double[] { weights[0], weights[1], weights[2] });
                }
                else
                {
                    listOutputX0[index] = (new double[] { weights[0], weights[1] });
                }
                if (indexGlobal > 4 && compareList(listOutputX0, listOutputX1))
                {
                    Console.WriteLine("\nWeight stabilization!");
                    Console.ReadKey();
                    break;
                }
                else

                {
                    if (xor == false)
                    {
                        listOutputX1[index] = (new double[] { weights[0], weights[1], weights[2] });
                    }
                    else
                    {
                        listOutputX1[index] = (new double[] { weights[0], weights[1] });
                    }
                }


                indexGlobal++;
                index++;
                if (index == 4)
                {
                    index = 0;
                    Console.WriteLine("===================================================");
                }
                Console.ReadKey();
            }
        }
    }
}
