using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_station
{
    class Transaction
    {
        public Transaction(Account ac, float s, string action)
        {
            Id = ++counter;
            date = "Today";
            amount = s;
            if (action == "add") {
                ac.AddMoney(s);
                fromAccount = null;
                toAccount = ac;
            }
            else {
                ac.withdrawMoney(s);
                fromAccount = ac;
                toAccount = null;

            }
            Console.WriteLine("transaction successed");
        }
        public Transaction(Account from, float s, Account to)
        {
            Id = ++counter;
            from.withdrawMoney(s);
            to.AddMoney(s);
            fromAccount = from;
            toAccount = to;
            date = "Today";
            amount = s;
            Console.WriteLine("transaction successed");
            operation = "transfer";

        }
        private static int counter;
        public int Id;
        public string date { get; set; }
        public Account fromAccount;
        public Account toAccount;
        public float amount;
        public string operation = null;





    }
}
