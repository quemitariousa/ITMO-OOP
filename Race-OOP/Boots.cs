using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public class Boots : GroundTransport
    {
        public Boots() : base(6, 60)
        {
        }

        protected override double CountRestDuration(double time)
        {
            int restCount = (int)(time / RestInterval);

            if (restCount * RestInterval == time && restCount != 0)
            {
                restCount--;
            }
            double restDuration = 0;
            if (restDuration > 0)
            {
                restDuration += 10;
                restCount -= 1;
            }

            restDuration += 5 * restCount;
            return restDuration;
        }
    }
}
