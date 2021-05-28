using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate
{
    public class Calculating
    {
        public static List<double> ResultList { get; private set; }
        public static List<double> XList { get; private set; }
        public static List<string> XRange { get; private set; }
        public Calculating(string[] input)
        {
            ResultList = new List<double>();
            XList = new List<double>();
            XRange = new List<string> { Function.Start.ToString(), Function.End.ToString() };
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
            double[] nums;
            
            for (int i = 0; i < input.Length; i++) //перебираем выражение
            {
                string a = string.Empty;

                if (!Postfix.IsOperator(input[i])) //Заносим в стэк НЕоперации
                {
                    a += input[i] == "x" ? x.ToString() : input[i];
                    i++;
                    if (i == input.Length)
                        break;
                    temp.Push(double.Parse(a)); i--;

                }
                else if (Postfix.IsOperator(input[i])) //Заносим в стэк операции, выбираем нужную, пушим результат
                {
                    nums = new double[] { temp.Pop(), temp.Pop() };
                    result = ChooseOperation(result, input, nums, i);
                    temp.Push(result);
                }
            }
            return temp.Peek(); //Достаём результат
        }
        private static double ChooseOperation(double result, string[] input, double[] nums, int i) //возвращаем операцию
        {
            switch (input[i])
            {
                case "+":
                    result = new Plus().Calc(nums);
                    break;
                case "-":
                    result = new Minus().Calc(nums);
                    break;
                case "*":
                    result = new Multiply().Calc(nums);
                    break;
                case "/":
                    result = new Divide().Calc(nums);
                    break;
                case "^":
                    result = new Rank().Calc(nums);
                    break;
            }
            return result;
        }
    }
}
