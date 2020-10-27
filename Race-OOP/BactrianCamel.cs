using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public class BactrianCamel : GroundTransport
    {
        public BactrianCamel() : base(10,30)
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
            if (restCount > 0)
            {
                restDuration += 5;
                restCount -= 1;
            }

            restDuration += 8 * restCount;
            return restDuration;

        }
    }
}
