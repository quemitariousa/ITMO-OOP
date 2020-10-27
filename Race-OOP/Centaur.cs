﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public class Centaur : GroundTransport
    {
        public Centaur() : base(15, 8)
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
                restDuration += 2;
                restCount -= 1;
            }

            restDuration += 2 * restCount;
            return restDuration;
        }
    }
}