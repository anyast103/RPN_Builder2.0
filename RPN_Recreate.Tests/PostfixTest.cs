using NUnit.Framework;
using System.Linq;
using System.Text.RegularExpressions;

namespace RPN_Recreate.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [TestCase("1+2", ExpectedResult = new string[] { "1", "2", "+" })]
        [TestCase("1*2+3", ExpectedResult = new string[] { "1", "2", "*", "3", "+" })]
        [TestCase("5*6+(2-9)", ExpectedResult = new string[] { "5", "6", "*", "2", "9", "-", "+" })]
        public string[] TestFunction(string expression )
        {
            return RPN_Recreate.Postfix.GetExpression(Regex.Split(expression, @"-?([\W])"));
        }
    }
}