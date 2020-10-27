using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public class MagicCarpet : AirTransport
    {
        public MagicCarpet() : base(10)
        {
        }

        protected override decimal GetReducedDistance(int distance)
        {
            if (distance < 1000)
            {
                return distance;
            }
            else if (distance < 5000)
            {
                return (decimal) (distance * 0.97);
            }
            else if (distance < 10000)
            {
                return (decimal) (distance * 0.9);
            }
            else
            {
                return (decimal) (distance * 0.95);
            }
        }
    }
}
