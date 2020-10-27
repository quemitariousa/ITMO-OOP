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

        protected override decimal GetReducedDistance(int distance)
        {
            return (decimal) (distance * 0.94);
        }
    }
}
