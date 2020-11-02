using System;

namespace Race_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Race<GroundTransport> airRace = new Race<GroundTransport>();
            BactrianCamel bar = new BactrianCamel();
            airRace.Register(bar);
            Centaur cen = new Centaur();
            airRace.Register(cen);
            
        }
    }
}
