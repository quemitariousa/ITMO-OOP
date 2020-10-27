using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public abstract class GroundTransport : Transport
    {
        public decimal RestInterval { get; }
        

        protected GroundTransport(int speed, decimal restInterval) : base(speed)
        {
            RestInterval = restInterval;
        }

        protected abstract decimal CountRestDuration(decimal time);
        public override decimal Move(int distance) 
        {
            decimal time = (decimal) distance / Speed;
            return time + CountRestDuration(time);
        }
    }
}
