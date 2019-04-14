using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssign_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //User Inputs

            //interval [a,b]
            //number of subintervals - n
            //which equation

            double intervalA = 0;
            double intervalB = 0;

            int panels = 0;
            int problem;

            Console.WriteLine(String.Format("Select which problem you would like to solve for: \n1 - 1 Trapezoid\n2 - 2 Trapezoid \n3 - 1 Simpsons \n4 - 2 Simpsons"));
            Console.WriteLine("---------------------------------------");

            problem = Convert.ToInt16(Console.ReadLine());
            problem--;
            Console.WriteLine("Interval A");
            intervalA = Double.Parse(Console.ReadLine());

            Console.WriteLine("Interval B");
            intervalB = Double.Parse(Console.ReadLine());

            Console.WriteLine("Number of intervals");
            panels = Int16.Parse(Console.ReadLine());
            

            switch (problem)
            {
                case 0:
                    CompTrapezoid trapezoid1 = new CompTrapezoid(intervalA, intervalB, panels, 1);
                    Console.WriteLine($"Trapezoidal \nApproximation: {trapezoid1.approx} \nInterval: [{trapezoid1.a}, {trapezoid1.b}] \nNumber of Intervals: {trapezoid1.interval} \nH: [{trapezoid1.h}]");
                    break;

                case 1:
                    CompTrapezoid trapezoid2 = new CompTrapezoid(intervalA, intervalB, panels, 2);
                    Console.WriteLine($"Trapezoidal \nApproximation: {trapezoid2.approx} \nInterval: [{trapezoid2.a}, {trapezoid2.b}] \nNumber of Intervals: {trapezoid2.interval} \nH: [{trapezoid2.h}]");
                    break;

                case 2:
                    CompSimpson simpson1 = new CompSimpson(intervalA, intervalB, panels, 1);
                    Console.WriteLine($"Trapezoidal \nApproximation: {simpson1.approx} \nInterval: [{simpson1.a}, {simpson1.b}] \nNumber of Intervals: {simpson1.interval} \nH: [{simpson1.h}]");
                    break;

                case 3:
                    CompSimpson simpson2 = new CompSimpson(intervalA, intervalB, panels, 2);
                    Console.WriteLine($"Trapezoidal \nApproximation: {simpson2.approx} \nInterval: [{simpson2.a}, {simpson2.b}] \nNumber of Intervals: {simpson2.interval} \nH: [{simpson2.h}]");
                    break;
            }

            Console.ReadLine();

            //Output

            //approx
            //Which method was used
            //interval
            //number of intervals used
            //value of h
        }


    }
    public class CompSimpson
    {
        public static double Y1(double x)
        {
            // x^2 * ln(x)
            double answer = /*Math.Pow(x, 2) **/ Math.Log(x);

            if (double.IsNaN(answer))
            {
                return 0;
            }

            return answer;
        }

        public static double Y2(double x)
        {
            // x / sqrt(x^2 + 9)
            return (x / (Math.Sqrt(Math.Pow(x, 2) + 9)));
        }

        public double a, b;
        public int interval, selectedEqu;
        public double h;
        public List<double> range;
        public double approx;

        public CompSimpson(double _a, double _b, int i, int sel)
        {
            this.a = _a;
            this.b = _b;
            this.interval = i;
            this.selectedEqu = sel;
            range = new List<double>();

            Get_h();
            GetRange();

            if (this.selectedEqu == 1)
            {
                approx = GetApproxY1();
            }
            else
            {
                approx = GetApproxY2();
            }
        }


        private void GetRange()
        {
            bool isFinished = false;
            int count = 0;
            while (!isFinished)
            {
                if (range.Count <= 0)
                {
                    range.Add(a);
                    count++;
                }
                else if (range[count - 1] < this.b)
                {
                    range.Add(range[count - 1] + h);
                    count++;
                }
                else
                {
                    isFinished = true;
                }
            }
        }

        private void Get_h()
        {
            this.h = (this.b - this.a) / (this.interval * 2);
        }


        public double GetApproxY1()
        {
            double answer = 0;

            for (int i = 0; i < range.Count; i++)
            {
                if (i == 0 || i == range.Count - 1)
                {
                    answer += Y1(range[i]);
                }
                else if (i % 2 == 0)
                {
                    answer += 2 * Y1(range[i]);
                }
                else
                {
                    answer += 4 * Y1(range[i]);
                }
            }
            return (h / 3) * (answer);
        }

        private double GetApproxY2()
        {
            double answer = 0;

            for (int i = 0; i < range.Count; i++)
            {
                if (i == 0 || i == range.Count - 1)
                {
                    answer += Y2(range[i]);
                }
                else if (i % 2 == 0)
                {
                    answer += 2 * Y2(range[i]);
                }
                else
                {
                    answer += 4 * Y2(range[i]);
                }
            }

            return (h / 3) * (answer);
        }
    }

    public class CompTrapezoid
    {
        public static double Y1(double x)
        {
            // x^2 * ln(x)
            double answer = Math.Pow(x, 2) * Math.Log(x);

            if (double.IsNaN(answer))
            {
                return 0;
            }

            return answer;
        }

        public static double Y2(double x)
        {
            // x / sqrt(x^2 + 9)
            return (x / (Math.Sqrt(Math.Pow(x, 2) + 9)));
        }

        public double a, b;
        public int interval, selectedEqu;
        public double h;
        public List<double> range;
        public double approx;

        public CompTrapezoid(double _a, double _b, int i, int sel)
        {
            this.a = _a;
            this.b = _b;
            this.interval = i;
            this.selectedEqu = sel;
            range = new List<double>();

            Get_h();
            GetRange();

            if (this.selectedEqu == 1)
            {
                approx = GetApproxY1();
            }
            else
            {
                approx = GetApproxY2();
            }
        }


        private void GetRange()
        {
            bool isFinished = false;
            int count = 0;
            while (!isFinished)
            {
                if (range.Count <= 0)
                {
                    range.Add(a);
                    count++;
                }
                else if (range[count - 1] < this.b)
                {
                    range.Add(range[count - 1] + h);
                    count++;
                }
                else
                {
                    isFinished = true;
                }
            }
        }

        private void Get_h()
        {
            this.h = (this.b - this.a) / this.interval;
        }


        public double GetApproxY1()
        {
            double answer = 0;

            for (int i = 0; i < range.Count; i++)
            {
                if (i == 0 || i == range.Count - 1)
                {
                    answer += Y1(range[i]);
                }
                else
                {
                    answer += 2 * Y1(range[i]);
                }
            }
            return (h/2) * (answer);
        }

        private double GetApproxY2()
        {
            double answer = 0;

            for (int i = 0; i < range.Count; i++)
            {
                if (i == 0 || i == range.Count - 1)
                {
                    answer += Y2(range[i]);
                }
                else
                {
                    answer += 2 * Y2(range[i]);
                }
            }

            return (h / 2) * (answer);
        }
    }

}
