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
                    nums = new double[] { temp.Pop(), temp.Pop() };
                    result = ChooseOperation(result, input, nums, i);
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }
        private static double ChooseOperation(double result, string[] input, double[] nums, int i)
        {
            switch (input[i])
            {
                case "+":
                    Plus plus = new Plus();
                    result = plus.Calc(nums);
                    break;
                case "-":
                    Minus minus = new Minus();
                    result = minus.Calc(nums);
                    break;
                case "*":
                    Multiply mul = new Multiply();
                    result = mul.Calc(nums);
                    break;
                case "/":
                    Divide div = new Divide();
                    result = div.Calc(nums);
                    break;
                case "^":
                    Rank rank = new Rank();
                    result = rank.Calc(nums);
                    break;
            }
            return result;
        }
    }
    public abstract class Operation
    {
        public abstract double Calc(double[] nums);
    }
    class Plus : Operation
    {
        public override double Calc (double[] nums) => nums[1] + nums[0];
    }
    class Minus : Operation
    {
        public override double Calc(double[] nums) => nums[1] - nums[0];
    }
    class Multiply : Operation
    {
        public override double Calc(double[] nums) => nums[1] * nums[0];
    }
    class Divide : Operation
    {
        public override double Calc(double[] nums) => nums[1] / nums[0];
    }
    class Rank : Operation
    {
        public override double Calc(double[] nums) => Math.Pow(nums[1],nums[0]);
    }
}
