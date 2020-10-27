using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public class Mortar : AirTransport
    {
        public Mortar() : base(8)
        {
        }

        protected override double GetReducedDistance(int distance)
        {
            return (double) (distance * 0.94);
        }
    }
}
