using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using nsClearConsole;

namespace ConsoleApplication1
{
    public class Program
    {

        List<Account> acctList = new List<Account>();
        ClearConsole myCC = new ClearConsole();
        string dir = @".\test.out";
        //String aName;
        //Account account = new Account();

        public static void Main(string[] args)
        {
            Program myATM = new Program();
            
            myATM.readAccount();
            //myATM.createAccount();
            myATM.atmMenu();
            myATM.writeAccount();
        }

        public void readAccount()
        {
            try
            {
                using (Stream stream = File.Open(dir, FileMode.Open))
                {
                    var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    acctList = (List<Account>)bFormatter.Deserialize(stream);
                    stream.Close();
                    if(acctList.Count <= 0)
                    {
                        createAccount();
                    }
                }
            }
            catch(Exception e)
            {
                createAccount();
            }
        }

        public void atmMenu()
        {
            int input;
            int number;
            string str;

            do
            {
                myCC.Clear();
                Console.WriteLine("\t ATM MENU");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1. Create a new account");
                Console.WriteLine("2. Remove an old account");
                Console.WriteLine("3. Make a transaction");
                Console.WriteLine("4. Exit");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Please choose one: ");
                str = Console.ReadLine();
                myCC.Clear();               

                if (!int.TryParse(str, out number) || string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("Please input [1-4] to pick an option.");
                    Console.WriteLine();
                }
                else
                {
                    input = Convert.ToInt32(str);
                    if (input == 1)
                    {
                        createAccount();
                        Console.WriteLine();
                    }
                    else if (input == 2)
                    {
                        removeAccount();
                        Console.WriteLine();
                    }
                    else if (input == 3)
                    {
                        makeTransaction();
                        Console.WriteLine();

                    }
                    else if (input == 4)
                    {
                        Console.WriteLine("Goodbye!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.WriteLine();
                    }
                }
            } while (!int.TryParse(str, out number) || string.IsNullOrEmpty(str) || Convert.ToInt32(str) != 4);
        }

        public void createAccount()
        {
            String aName;
            bool accountAlreadyExists = false;

            Console.WriteLine("Please enter a name for your new account: ");
            aName = Convert.ToString(Console.ReadLine());
            Thread.Sleep(1 * 1000);
            myCC.Clear();


            if (string.IsNullOrEmpty(aName))
            {
                Console.WriteLine("Account name is required. Please try again.");
                Console.WriteLine();
                createAccount();
            }
            else
            {
                for (int i = 0; i < acctList.Count; i++)
                {
                    if (aName.Equals(acctList[i].Acct))
                    {
                        accountAlreadyExists = true;
                    }
                }

                if (accountAlreadyExists)
                {
                    Console.WriteLine("Sorry, there is already an account associated with this name.");
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Hit ENTER to try again...");
                    Console.ReadLine();
                    myCC.Clear();
                }
                else
                {
                    acctList.Add(new Account(100.00, aName));
                    Console.WriteLine("Account " + "|" + aName.ToUpper() + "|" + " was successfully created.");
                    Thread.Sleep(1 * 1000);
                    myCC.Clear();
                }
                Console.WriteLine();
            }
        }

        public void removeAccount()
        {
            int acctNumber;
            string str;
            String accountName;

            if(acctList.Count > 0)
            {
                Console.WriteLine("CURRENT ACCOUNTS");
                Console.WriteLine("----------------");
                for(int i = 0; i < acctList.Count; i++)
                {
                    Console.WriteLine("Account " + (i + 1) + ": " + acctList[i].Acct.ToUpper());
                }

                Console.WriteLine("----------------");
                Console.WriteLine("Please enter the number of the account you want to remove.");
                str = Console.ReadLine();
                Thread.Sleep(1 * 1000);
                myCC.Clear();
                int number;

                if (!int.TryParse(str, out number) || string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("Please input a number.");
                    Console.WriteLine();
                    removeAccount();
                }
                else
                {
                    acctNumber = Convert.ToInt32(str);
                    if (acctNumber <= acctList.Count && acctNumber > 0)
                    {
                        for (int i = 0; i < acctList.Count; i++)
                        {
                            if (acctNumber == (i + 1))
                            {
                                accountName = acctList[i].Acct;
                                acctList.RemoveAt(i);
                                Console.WriteLine();
                                Console.WriteLine("Account " + "|" + accountName.ToUpper() + "|" + " was successfully removed.");
                                Thread.Sleep(1 * 1000);
                                myCC.Clear();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no account associated with that number. Please try again.");
                        Console.WriteLine();
                        removeAccount();

                    }
                }
            }
            else
            {
                Console.WriteLine("There are currently no accounts in the system.");
                Console.WriteLine("\n\n");
                Console.WriteLine("Hit ENTER to go back to main MENU...");
                Console.ReadLine();
                myCC.Clear();
            }
        }

        public void makeTransaction()
        {
            int input;
            string str;

            if(acctList.Count > 0)
            {
                Console.WriteLine("CURRENT ACCOUNTS");
                Console.WriteLine("----------------");
                for (int i = 0; i < acctList.Count; i++)
                {
                    Console.WriteLine("Account " + (i + 1) + ": " + acctList[i].Acct.ToUpper());
                }

                Console.WriteLine("----------------");
                Console.WriteLine("Please enter the account number: ");
                str = Console.ReadLine();
                Thread.Sleep(1 * 1000);
                myCC.Clear();
                int number;

                if (!int.TryParse(str, out number) || string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("Please input a number.");
                    Console.WriteLine();
                    makeTransaction();
                }
                else
                {
                    input = Convert.ToInt32(str);
                    if (input <= acctList.Count && input > 0)
                    {
                        for (int i = 0; i < acctList.Count; i++)
                        {
                            if (input == (i + 1))
                            {
                                acctList[i].acctMenu();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no account associated with that number.");
                        Console.WriteLine();
                        makeTransaction();
                    }
                }
            }
            else
            {
                Console.WriteLine("There are currently no accounts in the system.");
                Console.WriteLine("\n\n");
                Console.WriteLine("Hit ENTER to go back to main MENU...");
                Console.ReadLine();
                myCC.Clear();
            }
        }

        public void writeAccount()
        {           
            //string serializationFile = Path.Combine(dir);
            try
            {
                using (Stream stream = File.Open(dir, FileMode.Create))
                {
                    var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bFormatter.Serialize(stream, acctList);
                    stream.Flush();
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
