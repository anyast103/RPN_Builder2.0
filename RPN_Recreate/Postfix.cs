using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate
{
    public class Postfix
    {
        static public bool IsOperator(string c)
        {
            if (("+-/*^()".IndexOf(c) != -1))
                return true;
            return false;
        }
        static private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(": return 0;
                case ")": return 1;
                case "+": return 2;
                case "-": return 3;
                case "*": return 4;
                case "/": return 4;
                case "^": return 5;
                
                default: return 6;
            }
        }
        static public string[] GetExpression(string[] function)
        {
            Queue<string> queue = new Queue<string>();
            Stack<string> operStack = new Stack<string>();

            for (int i = 0; i < function.Length; i++)
            {
                if (function[i] == "x" || !IsOperator(function[i]))
                {
                    while (!IsOperator(function[i]))
                    {
                        queue.Enqueue( function[i]);
                        i++;

                        if (i == function.Length) break;
                    }                
                    i--;
                }
                if (IsOperator(function[i]))
                {
                    if (function[i] == "(")
                        operStack.Push(function[i]);
                    else if (function[i] == ")")
                    {
                        
                        string s = operStack.Pop();

                        while (s != "(")
                        {
                           queue.Enqueue( s.ToString());
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(function[i]) <= GetPriority(operStack.Peek()))
                                queue.Enqueue( operStack.Pop());
                        operStack.Push(function[i]);
                    }
                }
            }
            while (operStack.Count > 0)
                queue.Enqueue(operStack.Pop());
            return queue.ToArray();
        }
    }
}
