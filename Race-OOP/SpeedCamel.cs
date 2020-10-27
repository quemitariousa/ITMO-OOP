using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    class SpeedCamel : GroundTransport
    {
        public SpeedCamel() : base(40, 10)
        {
        }

        protected override decimal CountRestDuration(decimal time)
        {
            int restCount = decimal.ToInt32(time / RestInterval);

            if (restCount * RestInterval == time && restCount != 0)
            {
                restCount--;
            }

            decimal restDuration = 0;
            if (restCount > 0)
            {
                restDuration += 5;
                restCount -= 1;
                restDuration += (decimal) 6.5;
                restCount -= 1;
            }

            restDuration += 8 * restCount;
            return restDuration;
        }
    }
}
