using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate
{
    public class Calculating
    {
        public static List<double> ResultList { get; private set; }
        public static List<double> XList { get; private set; }

        private static List<string> XRange = new List<string> { Function.Start.ToString(), Function.End.ToString() };
        public Calculating(string[] input)
        {
            ResultList = new List<double>();
            XList = new List<double>();

            for (double i = double.Parse(XRange[0]); i <= double.Parse(XRange[1]); i += Function.Step)
            {
                XList.Add(i);

                ResultList.Add(Calculate(input, i));
            }
        }
        public static double Calculate(string[] input, double x)
        {
            double result = 0;
            Stack<double> temp = new Stack<double>();
            
            for (int i = 0; i < input.Length; i++)
            {
                string a = string.Empty;
                if (!Postfix.IsOperator(input[i]))
                {
                    a += input[i] == "x" ? x.ToString() : input[i];
                    i++;
                    if (i == input.Length) 
                        break;
                    temp.Push(double.Parse(a)); i--;
                }
                else if (Postfix.IsOperator(input[i]))
                {
                    double first = temp.Pop();
                    double second = temp.Pop();

                    switch (input[i])
                    {
                        case "+": result = second + first; break;
                        case "-": result = second - first; break;
                        case "*": result = second * first; break;
                        case "/": result = second / first; break;
                        case "^": result = double.Parse(Math.Pow(double.Parse(second.ToString()), double.Parse(first.ToString())).ToString()); break;
                        
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }
    }
}
