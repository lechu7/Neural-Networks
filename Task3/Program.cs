using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        public static double Fi(double t)
        {
            return 1 / (1 + Math.Pow(Math.E, -t));
        }

        public static void MatrixDisplay(double[,] matrix, int indexGlobal)
        {
            Console.WriteLine("w(" + indexGlobal + ")=");
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                {
                    Console.Write( matrix[i,j].ToString() + "  ");
                }
                Console.WriteLine();
            }
        }

        public static double[,] MatrixAdd(double[,] matrix1, double[,] matrix2)
        {
            //dodawanie macierzy
            double[,] sum = new double[3,3];
            for (int i = 0; i <Math.Sqrt(matrix1.Length); i++)
            {
                for (int j = 0; j <Math.Sqrt(matrix1.Length); j++)
                {                    
                    sum[i,j] = matrix1[i,j] + matrix2[i,j];
                }
            }
            return sum;
        }

        //Main
        static void Main(string[] args)
        {

            bool totalEnergy = false;

            Console.WriteLine("Which tryb you choose (i/ii)?");
            string parameters = Console.ReadLine();
            switch (parameters)
            {
                case "i":
                    totalEnergy = false;
                    break;
                case "ii":
                    totalEnergy = true;
                    break;
                default:
                    Console.WriteLine("Invalid parameters provided.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }


            double[,] weights = new double[3, 3];        
            weights[0, 0] = 0.86;
            weights[0, 1] = -0.16;
            weights[0, 2] = 0.28;

            weights[1, 0] = 0.83;
            weights[1, 1] = -0.51;
            weights[1, 2] = -0.86;

            weights[2, 0] = 0.04;
            weights[2, 1] = -0.43;
            weights[2, 2] = 0.48;


            double[,] deltaWeights = new double[3, 3];

            double dX1 = 0;
            double dX2 = 1;
            double dX3 = 1;
            double dX4 = 0;
            List<double> d = new List<double> { dX1, dX2, dX3, dX4 };//lista wartosci d

            List<double[]> listOutputX0 = new List<double[]>();//Aktualna lista
            List<double[]> listOutputX1 = new List<double[]>();//Lista wektorow z poprzedniej iteracji

            Console.WriteLine();
            Console.WriteLine();
            int indexGlobal = 0;
            int index = 0;
            List<double> listEnergy = new List<double>();
            List<double> oldEnergy= new List<double>() { 0,0,0,0};
            double teta = 2;
            if (totalEnergy)
            {

            }
            else
            {
                //Wektory trenujące
                double[] trainsX1 = new double[] { 1, 0, 0 };
                double[] trainsX2 = new double[] { 1, 0, 1 };
                double[] trainsX3 = new double[] { 1, 1, 0 };
                double[] trainsX4 = new double[] { 1, 1, 1 };

                List<double[]> trains = new List<double[]>{trainsX1, trainsX2, trainsX3,trainsX4 };//lista wektorow trenujacych

                while (true)
                {
                    double X21 = Fi(weights[0, 0] * trains[index][0] + weights[0, 1] * trains[index][1] + weights[0, 2] * trains[index][2]); // potencjał na wyjsciu X2(1)
                    double X22 = Fi(weights[1, 0] * trains[index][0] + weights[1, 1] * trains[index][1] + weights[1, 2] * trains[index][2]); // potencjał na wyjsciu X2(2)
                    double X31 = Fi(weights[2, 0] * 1 + weights[2, 1] * X21 + weights[2, 2] * X22); // potencjał na wyjsciu X3(1)
                    
                    double ro31 = X31 * (1 - X31) * (d[index] - X31);
                    double ro21 = X21 * (1 - X21) *weights[2,1] * ro31;
                    double ro22 = X22 * (1 - X22) * weights[2,2] * ro31;

                    double delta00 = teta * trains[index][0] * ro21;
                    double delta01 = teta * trains[index][1] * ro21;
                    double delta02 = teta * trains[index][2] * ro21;
                    double delta10 = teta * trains[index][0] * ro22;
                    double delta11 = teta * trains[index][1] * ro22;
                    double delta12 = teta * trains[index][2] * ro22;
                    double delta20 = teta * 1 * ro31;
                    double delta21 = teta * X21 * ro31;
                    double delta22 = teta * X22 * ro31;
                    
                    deltaWeights[0, 0] = delta00;
                    deltaWeights[0, 1] = delta01;
                    deltaWeights[0, 2] = delta02;
                    deltaWeights[1, 0] = delta10;
                    deltaWeights[1, 1] = delta11;
                    deltaWeights[1, 2] = delta12;
                    deltaWeights[2, 0] = delta20;
                    deltaWeights[2, 1] = delta21;
                    deltaWeights[2, 2] = delta22;

                    Console.WriteLine();
                   Console.WriteLine("Energy: " + Math.Pow((d[index]- X31),2));
                    listEnergy.Add(Math.Pow((d[index] - X31), 2));

                    weights = MatrixAdd(weights, deltaWeights);

                    Console.WriteLine();
                    Console.WriteLine();
                    indexGlobal++;
                    index++;
                    MatrixDisplay(weights, indexGlobal);

                    if (indexGlobal%20==0)
                    {
                        bool stable = true;
                        for (int i = 0; i < listEnergy.Count; i++)
                        {
                            if (oldEnergy[i] - listEnergy[i] < 0.01 && oldEnergy[i] - listEnergy[i] > 0 && listEnergy[i]<0.1)
                            { }
                            else
                            {
                                stable = false;
                                break;
                            }
                        }
                        if (stable)
                        {
                            Console.WriteLine("Algorythm is stable");
                            break;
                        }
                        else
                        {
                            oldEnergy = listEnergy;
                            listEnergy = new List<double>();
                        }
                    }


                    if (index == 4)
                    {
                        listOutputX1 = listOutputX0;
                        listOutputX0 = new List<double[]>();
                        index = 0;
                        listEnergy = new List<double>();
                           Console.WriteLine("===================================================");
                    }
                    // Console.ReadKey();
                }
                Console.ReadKey();
            }
        }
    }
}
