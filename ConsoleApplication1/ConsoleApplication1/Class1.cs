using System;
using System.Linq;
using System.Threading;
using System.Globalization;

namespace ConsoleApplication1
{
    [Serializable]
    public class Account
    {
        //ClearConsole myCC = new ClearConsole();
        //variables
        protected double balance;
        protected int firstDate;
        protected int secondDate;
        protected bool usePrevDate = false;
        //protected Calendar cal1 = new JulianCalendar();
        //protected Calendar cal2 = new JulianCalendar();
        protected Calendar cal1 = new GregorianCalendar();
        protected Calendar cal2 = new GregorianCalendar();
        protected bool dateflag = false;
        protected double rate;
        protected double accumInterest;
        protected String dispDate;
        protected String acctName;

        public string Acct
        {
            get { return acctName; }
        }

        public Account(double begin_balance, String aName)
        {
            balance = begin_balance;
            acctName = aName;
        }

        public Account()
        {
            balance = 0;
            acctName = null;
        }

        public void acctMenu()
        {
            char input;

            for (;;)
            {
                Console.WriteLine("Account Name: " + acctName.ToUpper());
                Console.WriteLine("----------------------------");
                Console.WriteLine("a. Deposit");
                Console.WriteLine("b. Withdraw");
                Console.WriteLine("c. Check Balance");
                Console.WriteLine("d. Back to Main Menu");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please choose one: ");

                String str = Convert.ToString(Console.ReadLine());
                Thread.Sleep(1 * 200);
                //myCC.Clear();
                Console.Clear();

                if (string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("Please enter [a-d] to pick an option.");
                    Console.WriteLine();

                }
                else
                {
                    input = str[0];
                    if ((input == 'a' || input == 'A') && str.Length == 1)
                    {
                        //getDate1();
                        //deposit();
                        if (dateflag)
                        {
                            getDate2();
                            if (!usePrevDate)
                            {
                                calcInterest();
                            }
                            deposit();
                            usePrevDate = false;
                        }
                        else
                        {
                            getDate1();
                            deposit();
                        }
                    }
                    else if ((input == 'b' || input == 'B') && str.Length == 1)
                    {
                        //withdraw();
                        if (dateflag)
                        {
                            getDate2();
                            if (!usePrevDate)
                            {
                                calcInterest();
                            }
                            withdraw();
                            usePrevDate = false;
                        }
                        else
                        {
                            getDate1();
                            withdraw();
                        }
                    }
                    else if ((input == 'c' || input == 'C') && str.Length == 1)
                    {
                        //checkBalance();
                        if (dateflag)
                        {
                            getDate2();
                            if (!usePrevDate)
                            {
                                calcInterest();
                            }
                            if (accumInterest > 0)
                            {
                                Console.WriteLine("Accumulated earned interest: " + accumInterest.ToString("C2"));
                            }

                            checkBalance();
                            usePrevDate = false;
                        }
                        else
                        {
                            getDate1();
                            checkBalance();
                        }
                    }
                    else if ((input == 'd' || input == 'D') && str.Length == 1)
                    {
                        break;
                    }
                    else if (str.Length > 1)
                    {
                        //Console.WriteLine();
                        Console.WriteLine("Please enter only one character at a time");
                        Console.WriteLine();

                    }
                    else
                    {
                        //Console.WriteLine();
                        Console.WriteLine("Invalid input. Please try again");
                        Console.WriteLine();
                    }
                }
            }
        }

        protected void deposit()
        {
            string str;
            int number;

            Console.WriteLine("Please enter the amount you would like to deposit: ");
            str = Console.ReadLine();
            if (!int.TryParse(str, out number) || string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Please try again.");
                Console.WriteLine();
                deposit();
            }
            else
            {
                double amountD = Convert.ToDouble(str);

                balance += amountD;
                Console.WriteLine();
                Console.WriteLine(amountD.ToString("C2") + " was deposited into your account.");
                Console.WriteLine("\n\n");
                Console.WriteLine("Hit ENTER to go back...");
                Console.ReadLine();
                Console.Clear();
                //myCC.Clear();
            }
        }

        protected void withdraw()
        {
            string str;
            int number;

            Console.WriteLine("Current balance: " + balance.ToString("C2"));
            Console.WriteLine("Please enter the amount you would like to withdraw: ");
            str = Console.ReadLine();

            if (!int.TryParse(str, out number) || string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Please try again.");
                Console.WriteLine();
                withdraw();
            }
            else
            {
                double amountW = Convert.ToDouble(str);
                if (amountW > balance)
                {
                    Console.WriteLine("Insufficient funds.");
                    Console.WriteLine();
                    withdraw();
                }
                else
                {
                    balance -= amountW;
                    Console.WriteLine();
                    Console.WriteLine(amountW.ToString("C2") + " was withdrawn from your account.");
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Hit ENTER to go back...");
                    Console.ReadLine();
                    Console.Clear();
                    //myCC.Clear();
                }
            }
        }

        protected void checkBalance()
        {
            Console.WriteLine("Current balance: " + balance.ToString("C2"));
            Console.WriteLine("\n\n");
            Console.WriteLine("Hit ENTER to go back...");
            Console.ReadLine();
            Console.Clear();
            //myCC.Clear();
        }

        protected void getDate1()
        {
            Console.WriteLine("Please enter today's date [mm/dd/yyyy]: ");
            String input = Convert.ToString(Console.ReadLine());
            dispDate = input;
            Console.WriteLine();

            try
            {
                DateTime myDate = Convert.ToDateTime(input);
                firstDate = cal1.GetDayOfYear(myDate);
                dateflag = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine();
                getDate1();

            }
        }

        protected void getDate2()
        {
            bool invalidInput = false;
            bool pastDate = false;


            Console.WriteLine("Please enter today's date [mm/dd/yyyy] or 'p' to use previous date (" + dispDate + ") : ");
            String input = Convert.ToString(Console.ReadLine());
            Console.WriteLine();

            try
            {
                if (input.ElementAt(0) == 'p' && input.Length == 1)
                {
                    usePrevDate = true;
                }
                else
                {
                    try
                    {
                        DateTime myDate = Convert.ToDateTime(input);
                        secondDate = cal2.GetDayOfYear(myDate);

                        if (firstDate > secondDate)
                        {
                            Console.WriteLine("You must enter a future date.");
                            Console.WriteLine();
                            pastDate = true;
                            getDate2();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input.");
                        Console.WriteLine();
                        invalidInput = true;
                        getDate2();
                    }
                    if (input.ElementAt(0) != 'p' && invalidInput == false && pastDate == false)
                    {
                        dispDate = input;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine();
                getDate2();
            }
        }

        protected void calcInterest()
        {
            int dateDiff = secondDate - firstDate;
            rate = .05 / 365;
            double rateTime = Math.Pow(1 + rate, dateDiff);
            balance *= rateTime;
            double amountInterest = balance - (balance / rateTime);
            accumInterest += amountInterest;
            firstDate = secondDate;
        }
    }
}

