using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public class Centaur : GroundTransport
    {
        public Centaur() : base(15, 8)
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
                restDuration += 2;
                restCount -= 1;
            }

            restDuration += 2 * restCount;
            return restDuration;
        }
    }
}
