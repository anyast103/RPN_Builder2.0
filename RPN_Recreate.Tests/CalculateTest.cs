using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate.Tests
{
    class CalculateTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckCalculate()
        {
            string[] func = new string[] { "2", "+", "3" };
            double checkTrue = 5;
            Assert.AreEqual(checkTrue, Calculating.Calculate(RPN_Recreate.Postfix.GetExpression(func), 1));
        }
        [Test]
        public void CheckRPN2()
        {
            string[] func = new string[] { "8", "+", "3", "-", "2" };
            double checkTrue = 7;
            Assert.AreEqual(checkTrue, Calculating.Calculate(RPN_Recreate.Postfix.GetExpression(func), 1));
        }
    }
}
