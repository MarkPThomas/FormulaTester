using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;


namespace Test
{
    class Program
    {
        static List<IFormula> formulas = new List<IFormula>();

        static void Main(string[] args)
        {
            int numberOfCases = 100;
            long iterations = 100000000;

            Random random = new Random();

            // Populate formula sets
            formulas = new List<IFormula>()
            {
                new Formula1(),
                new Formula2(),
                new Formula3(),
                new Formula4(),
            };

            // Parameters
            // Use jagged array so that rows can be fetched
            double[][] parametersAll = new double[numberOfCases][];

            for (int i = 0; i < numberOfCases; i++)
            {
                // Populate parameter sets
                double numA = RandomSign(random) * random.Next(100000) * Math.Pow(Math.PI, RandomSign(random) * random.Next(13));
                double numB = RandomSign(random) * random.Next(100000) * Math.Pow(Math.PI, RandomSign(random) * random.Next(13));
                parametersAll[i] = new double[2] { numA, numB };

                // Calculate results for case
                CalculateSetResults(parametersAll[i], i);
            }

            // Calculate times for each case
            List<string> caseCalculationTimes = new List<string>();
            for (int i = 0; i < numberOfCases; i++)
            {
                caseCalculationTimes.Add(TimeSetCalculations(parametersAll[i], iterations, i));
            }

            foreach (string result in caseCalculationTimes)
            {
                Console.WriteLine();
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }

        static private int RandomSign(Random random)
        {
            return random.Next(-1, 2);
        }

        public static string RoundWithTrailing(double value, int decimalPlaces)
        {
            double roundedValue = Math.Round(value, decimalPlaces);
            string formatter = "{0:f" + decimalPlaces + "}";

            return string.Format(formatter, roundedValue);
        }

        static void CalculateSetResults(double[] args, int parentIteration = 0)
        {
            parentIteration++;
            Console.WriteLine("{0}: Using a={1}, b={2}", parentIteration, args[0], args[1]);

            int subIteration = 0;

            foreach (IFormula formula in formulas)
            {
                subIteration++;
                Console.WriteLine("    " + TimeLabelLong(parentIteration, subIteration) + formula.calculate(args));
            }

            Console.WriteLine();
        }


        static string TimeSetCalculations(double[] args, long iterations, int parentIteration = 0)
        {
            parentIteration++;
            Console.WriteLine("{0}: Using a={1}, b={2}", parentIteration, args[0], args[1]);

            int subIteration = 0;
            long time = 0L;
            long minTime = -1L;
            long maxTime = 0L;
            string timeLabel = "";
            string minTimeLabel = "";
            string maxTimeLabel = "";

            foreach (IFormula formula in formulas)
            {
                subIteration++;
                time = IterateCalcuations(args, formula, iterations);
                timeLabel = TimeLabelShort(subIteration);
                PostProcessTime(timeLabel, time,
                                ref minTimeLabel, ref minTime, ref maxTimeLabel, ref maxTime);
            }

            double timeDiffRatio = 100 * ((double)minTime / maxTime);

            //return string.Format("Test Set{0}: " +
            //                    "\n    {1}% min/max ratio where " +
            //                    "\n         Min Time at {2}{3} " +
            //                    "\n         Max Time at {4}{5}",
            //                    parentIteration, RoundWithTrailing(timeDiffRatio,4), 
            //                    minTimeLabel, minTime, maxTimeLabel, maxTime);
            return string.Format("Test Set{0}: " +
                    "\n  {1}% min/max ratio where Min Time at {2}{3} & Max Time at {4}{5}",
                    parentIteration, RoundWithTrailing(timeDiffRatio, 4),
                    minTimeLabel, minTime, maxTimeLabel, maxTime);
        }

        private static string TimeLabelLong(long parentIteration, long subIteration)
        {
            return parentIteration + "." + subIteration + ":  ";
        }

        private static string TimeLabelShort(long subIteration)
        {
            return subIteration + ":  ";
        }

        private static void PostProcessTime(string timeLabel, long time,
                                                ref string minTimeLabel, ref long minTime,
                                                ref string maxTimeLabel, ref long maxTime)
        {
            Console.WriteLine("    {0} Time elapsed: {1}",
                            timeLabel, time);
            minTimeLabel = TimeMinLabel(time, minTime, timeLabel, minTimeLabel);
            minTime = TimeMin(time, minTime);
            maxTimeLabel = TimeMaxLabel(time, maxTime, timeLabel, maxTimeLabel);
            maxTime = TimeMax(time, maxTime);
        }

        private static string TimeMinLabel(long curentTime, long currentMinTime,
                                    string currentLabel, string currentMinLabel)
        {
            if (string.IsNullOrEmpty(currentMinLabel)) { return currentLabel; }
            if (curentTime < currentMinTime)
            {
                return currentLabel;
            }
            else
            {
                return currentMinLabel;
            }
        }
        private static string TimeMaxLabel(long currentTime, long currentMaxTime,
                                    string currentLabel, string currentMaxLabel)
        {
            if (currentTime > currentMaxTime)
            {
                return currentLabel;
            }
            else
            {
                return currentMaxLabel;
            }
        }
        private static long TimeMin(long currentTime, long currentMinTime)
        {
            if (currentMinTime < 0) { return currentTime; }
            if (currentTime < currentMinTime)
            {
                return currentTime;
            }
            else
            {
                return currentMinTime;
            }
        }
        private static long TimeMax(long currentTime, long currentMaxTime)
        {
            if (currentTime > currentMaxTime)
            {
                return currentTime;
            }
            else
            {
                return currentMaxTime;
            }
        }


        static long IterateCalcuations(double[] args, IFormula formula, long iterations)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (long i = 0; i <= iterations; i++)
            {
                formula.calculate(args);
            }
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }
}
