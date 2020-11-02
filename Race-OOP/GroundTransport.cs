using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public abstract class GroundTransport : Transport
    {
        public double RestInterval { get; }
        

        protected GroundTransport(int speed, double restInterval) : base(speed)
        {
            RestInterval = restInterval;
        }

        protected abstract double CountRestDuration(double time);
        public override double Move(int distance) 
        {
            double time = (double) distance / Speed;
            return time + CountRestDuration(time);
        }
    }
}
