using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public abstract class AirTransport : Transport
    {
        protected AirTransport(int speed) : base(speed)
        {
        }

        protected abstract double GetReducedDistance(int distance);

        public override double Move(int distance)
        {
            return GetReducedDistance(distance) / Speed;
        }
    }
}
