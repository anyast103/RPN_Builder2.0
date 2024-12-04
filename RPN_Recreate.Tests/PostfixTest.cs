using NUnit.Framework;

namespace RPN_Recreate.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckRPN()
        {
            string[] func = new string[] { "2", "+", "x" };
            string[] checkTrue = new string[] { "2", "x", "+" };
            Assert.AreEqual(checkTrue, RPN_Recreate.Postfix.GetExpression(func));
        }
        [Test]
        public void CheckRPN2()
        {
            string[] func = new string[] { "8", "+", "x", "-", "2" };
            string[] checkTrue = new string[] { "8", "x", "2", "-", "+" };
            Assert.AreEqual(checkTrue, RPN_Recreate.Postfix.GetExpression(func));
        }
    }
}