using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate
{
    public abstract class Operation
    {
        public abstract double Calc(double[] nums);
    }
    class Plus : Operation
    {
        public override double Calc(double[] nums) => nums[1] + nums[0];
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
        public override double Calc(double[] nums) => Math.Pow(nums[1], nums[0]);
    }
}
