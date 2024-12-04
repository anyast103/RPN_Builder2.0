using System;
using System.Collections.Generic;
using System.Text;
using RPN_Recreate;
using System.Linq;

namespace RPN_Recrate.Consolee
{
    
    public class Table
    {
        private static string[] Function = new string[] { "2", "+", "x" };

        public static void GetRPN()
        {
            string[] postfixed = RPN_Recreate.Postfix.GetExpression(Function);
            string rpn = "";
            foreach(var symbols in postfixed)
            {
                rpn += symbols;
            }
            Console.WriteLine(rpn);
        }
    }
}
