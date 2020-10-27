using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Race_OOP
{
    public class Race<T> where T : Transport
    {
        private List<T> _transports;

        public Race()
        {
            _transports = new List<T>();
        }

        public void Register(T transport)
        {
            _transports.Add(transport);
        }

        public T Start(int distance)
        {
            if (_transports.Count == 0)
            {
                throw new InvalidOperationException("no transport in race");
            }

            T winner = _transports
               .OrderBy(x => x.Move(distance))
               .First();
            return winner;

        }
    }
}
