using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
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
        public static double ScalarMultiplication(double[] matrix1, double[] matrix2)
        {
            //mnożenie macierzy skalarne
            double sum = 0;
            for (int i = 0; i < matrix1.Length; i++)
            {
                sum += matrix1[i] * matrix2[i];
            }
            return sum;
        }

        public static double[] VectorsAdd(double[] matrix1, double[] matrix2)
        {
            //dodawanie macierzy
            double[] sum = new double[matrix1.Length];
            for (int i = 0; i < matrix1.Length; i++)
            {
                sum[i] = matrix1[i] + matrix2[i];
            }
            return sum;
        }
        
        
        public static double[] VectorMultiplierDouble(double multipler, double[] matrix2)
        {
            //mnożenie macierzy przez liczbe
            double[] returnTable = new double[matrix2.Length];
            for (int i = 0; i < matrix2.Length; i++)
            {
                returnTable[i] = matrix2[i] * multipler;

            }
            return returnTable;
        }
        public static void CheckList(List<double[]> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (!CompareVerse(list[0],list[i]))
                {
                    Console.WriteLine("\n Find cycle but perceptron does not stabilize!");
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine("\nPerceptron stabilize!");
            Console.ReadKey();
            return;
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

        //Main 

        static void Main(string[] args)
        {
            Console.WriteLine("Which parameters you choose (i/ii/b/c[xor])?");
            string parameters = Console.ReadLine();

            double ro = 0;
            double[] weights = new double[] { 0, 0, 0 };
            bool xor = false;
            bool b = false;
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
                case "b":
                    weights = new double[] { 1, 1, 1 };
                    b = true;
                    break;
                case "c":
                    ro = 1;
                    weights = new double[] { 1, 1, 1 };
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


            List<double[]> trains = new List<double[]>();//lista wektorow trenujacych
            trains.Add(trainsX1);
            trains.Add(trainsX2);
            trains.Add(trainsX3);
            trains.Add(trainsX4);

            double dX1 = 0;
            double dX2 = 0;
            double dX3 = 1;
            double dX4 = 0;
            if (xor == true)
            {
                dX1 = 0;
                dX2 = 1;
                dX3 = 1;
                dX4 = 0;
            }
            List<double> d = new List<double> { dX1, dX2, dX3, dX4 };//lista wartosci d

            List<double[]> listOutputX0 = new List<double[]>();//Aktualna lista
            List<double[]> listOutputX1 = new List<double[]>();//Lista wektorow z poprzedniej iteracji

            Console.WriteLine();
            Console.WriteLine();
            int indexGlobal = 0;
            if (!b)
            {
                double yn;
                int index = 0;
                while (true)
                {
                    Console.WriteLine("*****Training vector: " + (index + 1) + ", iteration: " + (indexGlobal + 1) + "*****");
                    // obliczanie wartosci y dla n-tego elementu
                    yn = ScalarMultiplication(weights, trains[index]);
                    Console.WriteLine("y(" + indexGlobal + ")=" + yn);
                    if (yn > 0)
                    {
                        yn = 1;
                    }
                    else
                    {
                        yn = 0;
                    }

                    //weights(x+1)=weights(x)+ro(d(x)-y(x))*x(x)
                    weights = VectorsAdd(VectorMultiplierDouble((ro * (d[index] - yn)), trains[index]), weights);
                    MatrixDisplay(weights, indexGlobal);

                    listOutputX0.Add(new double[] { weights[0], weights[1], weights[2] });


                    if (indexGlobal >= 4 && CompareVerse(listOutputX0[index], listOutputX1[index]))
                    {
                        listOutputX1.AddRange(listOutputX0);
                        listOutputX1.RemoveRange(0, index);
                        CheckList(listOutputX1);
                        break;
                    }


                    indexGlobal++;
                    index++;
                    if (index == 4)
                    {
                        listOutputX1 = listOutputX0;
                        listOutputX0 = new List<double[]>();
                        index = 0;
                        Console.WriteLine("===================================================");
                    }
                    Console.ReadKey();
                }
            }
            //podpunkt b
            else
            {
                List<double> y = new List<double>( new double[4]);//lista wartosci y
                while (true)
                {
                    bool stable = true;
                    for (int i = 0; i < d.Count; i++)
                    {
                        if (ScalarMultiplication(weights, trains[i])>0)
                        {
                            y[i] = 1;
                        }
                        else
                        {
                            y[i] = 0;
                        }
                    }
                    for (int i = 0; i < d.Count; i++)
                    {
                        if (y[i] != d[i])
                        {
                            stable = false;
                            if (d[i] == 1)
                            {
                                weights = VectorsAdd(weights, trains[i]);
                            }
                            else
                            {
                              weights=  VectorsAdd(weights, VectorMultiplierDouble(-1, trains[i]));
                            }
                        }
                    }
                    indexGlobal++;
                    MatrixDisplay(weights, indexGlobal);
                    if (stable)
                    {
                        Console.WriteLine("Perceptron stable!");
                        Console.ReadKey();
                        break;
                    }
                    Console.ReadKey();
                }

            }
        }

    }
}
