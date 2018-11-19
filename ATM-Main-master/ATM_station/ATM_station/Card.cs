using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_station
{
    class Card
    {
        public Card(string t, long n, int cvv, int p)
        {
            Type = t;
            Number = n;
            CVV = cvv;
            PIN = p;
        }
        public string Type{ get; set; }
        public long Number { get; private set; }
        public int CVV { get; private set; }
        public int PIN { get;  set; }

    }
}
