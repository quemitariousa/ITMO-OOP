using System;
using System.Collections.Generic;
using System.Text;

namespace Race_OOP
{
    public abstract class Transport
    {
        public int Speed { get; }

        protected Transport(int speed)
        {
            Speed = speed;
        }

        public abstract double Move(int distance);

    }
}
