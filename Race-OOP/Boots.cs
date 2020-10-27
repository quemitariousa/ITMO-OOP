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

        protected override decimal CountRestDuration(decimal time)
        {
            int restCount = decimal.ToInt32(time / RestInterval);

            if (restCount * RestInterval == time && restCount != 0)
            {
                restCount--;
            }
            decimal restDuration = 0;
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
