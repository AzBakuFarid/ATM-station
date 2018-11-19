using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_station
{
    class Program
    {
        public static void showTransactions(List<Transaction> tr)
        {
            if (tr.Count != 0)
            {
                byte counter = 1;
                Console.WriteLine("These are last 5 withdraws from your card");
                Console.WriteLine("#  date \t ammount");
                float sum = 0;
                for (int i = 0; i < tr.Count; i++)
                {
                    if (i < 5)
                    {
                        Console.WriteLine($"{counter++}  {tr[i].date} \t {tr[i].amount}");
                        sum += tr[i].amount;
                    }
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"   Total \t {sum}");
                Console.ResetColor();
            }

        }

        public static void showTransactions(Account a, List<Transaction> tr)
        {
           
            if (tr.Count != 0)
            {
                byte counter = 1;
                string action = "";
                float amount = 0;
                Console.WriteLine();
                Console.WriteLine("# date \t action \t ammount");
                float totalIncome = 0;
                float totalWithdraw = 0;
                foreach (var item in tr)
                {
                    if (item.fromAccount == a) { action = "cash"; amount = item.amount * -1; totalWithdraw += item.amount; }
                    if (item.toAccount == a) { action = "money adding"; amount = item.amount; totalIncome += item.amount; }
                    if (item.operation == "transfer")
                    {
                        if (item.fromAccount == a)
                        {
                            action = "MTFA";  // money transfered from account
                        }
                        if (item.toAccount == a)
                        {
                            action = "MTTA"; // money transfered to account
                        }
                    }
                    Console.WriteLine($"{counter++}  {item.date} \t {action}\t {amount}");
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"   Total income \t {totalIncome}");
                Console.WriteLine($"   Total withdraw \t {totalWithdraw}");
                Console.ResetColor();
            }
        }

            static void Main(string[] args)
            {
            Console.WriteLine("accountlarin pin codlari randomdan secilir.");
            Console.WriteLine("evvelde yazilan 4 setir reqemler hemin  kodlardi (helelik 4 user var)");
            Console.WriteLine("sonradan pinkodu deyismek imkani var, amma baslangicda onlari bir kagiza yazib sonra davam ele");
            Console.WriteLine("bu yazilari birinci defe oxuyandan sonra bu yazilari cixardan kodlari ve koddaki novbeti setiri, yeni readline setirini de sil ki her defe bezdirmesin");
            Console.WriteLine("cunki indi readline konsoldan daxiletme gozleyir... istenilen duymesini bas ki proqrama gire bilesen..");
            string dayanmaq = Console.ReadLine(); // yuxarida dediyim setir budu

            // real is burdan baslayir
            List<Account> accounts = new List<Account>();
                accounts.Add(new Account("Farid", "Zeynalli", 5000));
                accounts.Add(new Account("Fuad", "Eliyev", 3000));
                accounts.Add(new Account("Ruslan", "Qurbanov", 1000));
                accounts.Add(new Account("Elnur", "Ehmedov", 2000));
                Random r = new Random();

                accounts[0].creatCard("Maestro", 1234567887654321, 354, r.Next(1001, 9999));
                accounts[1].creatCard("Visa", 6234567887654321, 124, r.Next(1001, 9999));
                accounts[2].creatCard("Master", 1234567877654321, 543, r.Next(1001, 9999));
                accounts[3].creatCard("Visa", 1234567187654321, 765, r.Next(1001, 9999));
                foreach (var item in accounts)
                { 
                    Console.WriteLine(item.card.PIN);
                }
                List<Transaction> transactions = new List<Transaction>();
                while (true) //not to let the programm to close
                {
                    try
                    {
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("Enter the card number");
                        string input = Console.ReadLine();
                        byte pincounter = 1;
                        Account ac = null;
                        if (input.Length == 16)
                        {
                            ac = accounts.FirstOrDefault(f => f.card.Number == long.Parse(input));
                        }
                        else
                        {
                            throw new Exception("You entered invalid card number. Card number must be 16 digits");
                        }
                        if (ac != null)
                        { Console.WriteLine($"Wellcome, Dear {ac.ToString()} "); }
                        else throw new Exception("You entered invalid card number. There is not such account!");
                        while (true) //pincode validating
                        {
                            Console.WriteLine($"Please enter your PIN code (attempt {pincounter})");
                            Console.WriteLine("You have to enter 4 digits");
                            input = Console.ReadLine();
                            if ((input.Length == 4) && (int.Parse(input) == ac.card.PIN))
                            {
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Incorrect");
                            Console.ResetColor();
                            pincounter++;
                            if (pincounter > 3)
                            {
                                throw new Exception("You entered invalid pincode 3 times..... and are logging off");
                            }
                        }
                        //show different options
                        options:
                        Console.WriteLine();
                        Console.WriteLine("For information press -1-");
                        Console.WriteLine("to add money press -2-");
                        Console.WriteLine("to get cash press -3-");
                        Console.WriteLine("to transfer money to another account press -4-");
                        Console.WriteLine("to transfer money to your account from others press -5-");
                        Console.WriteLine("to change your pincode press -6-");
                        Console.WriteLine("to add mobile number to your account press -7-");
                        Console.WriteLine("for detailed information about account select -8-");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1": //show information
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"Your Balans is : {ac.Balance} ");
                                    showTransactions(transactions.Where(w => w.fromAccount == ac).OrderByDescending(o=>o.Id).ToList());
                                    Console.WriteLine("if you want to continue with your account press -1-. to quit press any key");
                                    input = Console.ReadLine();
                                    if (input == "1") goto options;
                                }
                                break;
                            case "2":// add money
                                {
                                    Console.Write("Enter the amount of money you want to add: ");
                                    input = Console.ReadLine();
                                    if (float.Parse(input) > 0)
                                    {
                                        transactions.Add(new Transaction(ac, float.Parse(input), "add"));
                                    }
                                    else { Console.WriteLine("You entered inaccessable amount"); }
                                    Console.WriteLine("if you want to continue with your account press -1-. to quit press any key");
                                    input = Console.ReadLine();
                                    if (input == "1") goto options;
                                }
                                break;
                            case "3": // cash
                                {
                                    string day = "Today"; // burda eslinde Date olmalidi... amma helelik bununla yetinirik :)
                                    float cashAmount = 0;
                                    Console.Write("Select the amount of money you want to cash: ");
                                    Console.WriteLine("But pay attention that Bank has a limit of max $1000 in one transaction.");
                                    Console.WriteLine("1. 10$  \t\t 4. 100$");
                                    Console.WriteLine("2. 20$  \t\t 5. 500$");
                                    Console.WriteLine("3. 50$  \t\t 6. other");
                                    input = Console.ReadLine();
                                    switch (float.Parse(input))
                                    {
                                        case 1: { cashAmount = 10; } break;
                                        case 2: { cashAmount = 20; } break;
                                        case 3: { cashAmount = 50; } break;
                                        case 4: { cashAmount = 100; } break;
                                        case 5: { cashAmount = 500; } break;
                                        case 6:
                                            {
                                                Console.WriteLine("Enter the amount of money");
                                                cashAmount = float.Parse(Console.ReadLine());
                                            }
                                            break;
                                    default: { Console.WriteLine("your selection was incorrect"); goto options; }
                                    }
                                    if (cashAmount > 1000)
                                    {
                                        Console.WriteLine("Dedim axi, 1000-den balaca reqem yaz!!!");
                                        Console.WriteLine("You do not have a permission to cash more than $1000 per a transaction ");
                                        goto options;
                                    }
                                    if (cashAmount <= 0) { Console.WriteLine("Invalid amount"); goto options; }
                                    if (transactions.Count(w => w.fromAccount == ac && w.date == day && w.operation != "transfer") == 10)
                                    {
                                        Console.WriteLine("You have exceeded cash limit for today.... Please try tomorrow, or contact to your Bank");
                                        goto options;
                                    }
                                    if (cashAmount > ac.Balance)
                                    {
                                    Console.WriteLine("You don't have enough money");
                                    goto options;
                                }
                                    transactions.Add(new Transaction(ac, cashAmount, "withdraw"));
                                    Console.WriteLine("if you want to continue with your account press -1-. to quit press any key");
                                    input = Console.ReadLine();
                                    if (input == "1") goto options;
                                }
                                break;
                            case "4": // money transfer
                                {
                                    Account toAccount = null;
                                    Console.WriteLine("Enter the card number of account to which you want to transfer money");
                                    input = Console.ReadLine();
                                    if (input.Length == 16)
                                    {
                                        toAccount = accounts.FirstOrDefault(f => f.card.Number == long.Parse(input));
                                    }
                                    else
                                    {
                                    Console.WriteLine("You entered invalid card number. Card number must be 16 digits");
                                    goto options;

                                }
                                if (toAccount == null || toAccount==ac)
                                    { Console.WriteLine("You selected your own accout or there is not such account!"); goto options; }
                                    else
                                    {
                                        Console.Write("Enter the amount of money you want to transfer: ");
                                        input = Console.ReadLine();
                                        if (float.Parse(input) <= 0) { Console.WriteLine("Invalid amount"); goto options; }
                                        if (float.Parse(input) > ac.Balance)
                                        {
                                        Console.WriteLine("You don't have enough money");
                                        goto options;
                                        }
                                        transactions.Add(new Transaction(ac, float.Parse(input), toAccount));
                                        Console.WriteLine("if you want to continue with your account press -1-. to quit press any key");
                                        input = Console.ReadLine();
                                        if (input == "1") goto options;
                                    }
                                }
                                break;
                            case "5": //illegal option
                                {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine("You thought you can get easy money? unfortunatle it is imposible.....");
                                    Console.WriteLine("Our Bank does not provide any functionality for this option...but you can ask hakers :)... ");
                                    Console.WriteLine("Now, please select more realistic option!");
                                Console.ResetColor();
                                    goto options;
                                }
                            case "6": //change pincode
                                {
                                    Console.WriteLine("Enter new pincode");
                                    string newPin = Console.ReadLine();
                                    if (newPin.Length != 4)
                                    {
                                        Console.WriteLine("Entered pin is incorrect");
                                    }
                                    else { Console.WriteLine("please reenter your new pincode"); }
                                    input = Console.ReadLine();
                                    if (newPin == input)
                                    {
                                        ac.card.PIN = int.Parse(newPin);
                                        Console.WriteLine("Your pincode has successfully changed");
                                    }
                                    else
                                    {
                                        Console.WriteLine("you reentered new pincode incorrectly");
                                        goto options;
                                    }
                                    Console.WriteLine("if you want to continue with your account press -1-. to quit press any key");
                                    input = Console.ReadLine();
                                    if (input == "1") goto options;
                                }
                                break;
                            case "7": //add mobile number
                                {
                                    if (ac.Telefon == null)
                                        Console.WriteLine("Enter the mobile number to secure your account (9 digits)");
                                    else Console.WriteLine("Enter new mobile number (9 digits)");
                                    input = Console.ReadLine();
                                    if (input.Length != 9)
                                    {
                                        Console.WriteLine("Entered mobile number is invalid");
                                        goto options;
                                    }
                                    else
                                    {
                                        ac.Telefon = "+994" + input;
                                        Console.WriteLine("Your mobile number has succesfully changed");
                                    }
                                    Console.WriteLine("if you want to continue with your account press -1-. to quit press any key");
                                    input = Console.ReadLine();
                                    if (input == "1") goto options;
                                }
                                break;
                            case "8": // detailed information
                                {
                                    Console.WriteLine($"Your current balance is : {ac.Balance}");
                                    if (ac.Telefon != null)
                                        Console.WriteLine($"Your mobile number is : {ac.Telefon}"); //  telefonu deyismek ucun opsiyani bura da copy etmek olardi. eyni shey burda yazmaga hovselem catmadi.. 
                                    showTransactions(ac, transactions.AsParallel().Where(w => w.fromAccount == ac || w.toAccount == ac).OrderBy(o => o.Id).ToList());
                                    Console.WriteLine("if you want to continue with your account press -1-. to quit press any key");
                                    input = Console.ReadLine();
                                    if (input == "1") goto options;
                                }
                                break;
                            default: { throw new Exception("There is not any option for the key you pressed... Pay attention next time"); }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
            }
        }
    }
