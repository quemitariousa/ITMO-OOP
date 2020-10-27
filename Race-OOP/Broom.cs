using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public class Broom : AirTransport
    {
        public Broom() : base(20)
        {
        }

        protected override double GetReducedDistance(int distance)
        {
            int percent = distance / 1000;
            if (percent > 100)
            {
                percent = 100;
            }

            return (double)(distance) * (100 - percent) / 100;
        }
    }
}
