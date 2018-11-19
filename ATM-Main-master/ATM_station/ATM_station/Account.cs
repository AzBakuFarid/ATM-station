using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_station
{
    class Account
    {
        public Account(string n, string s, float f)
        {
            Id = ++counter;
            Name = n;
            Surname = s;
            Balance = f;
        }
        private static int counter;
        public int Id;
        public string Name{ get; set; }
        public string Surname { get; set; }
        public float Balance { get; set; }
        public Card card;
        public string Telefon { get; set; }

        public void creatCard(string t, long n, int cvv, int pin) {
            card = new Card(t,n,cvv, pin);
            
        }

        public void AddMoney(float f) {
            Balance += f;
        }

        public void withdrawMoney(float f) {
            Balance -= f;
        }
        public override string ToString()
        {
            return Name + " " + Surname;
        }



    }
}
