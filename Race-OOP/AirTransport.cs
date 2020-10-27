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

        protected abstract decimal GetReducedDistance(int distance);

        public override decimal Move(int distance)
        {
            return GetReducedDistance(distance) / Speed;
        }
    }
}
