using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
//using nsClearConsole;

namespace ConsoleApplication1
{
    public class Program
    {

        List<Account> acctList = new List<Account>();
        string dir = @"C:/Users/Chau/Desktop/test.xml";

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
                using (Stream stream = File.OpenRead(dir))
                {
                    var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    acctList = (List<Account>)bFormatter.Deserialize(stream);
                    //XmlSerializer xDeserialize = new XmlSerializer(typeof(List<Account>));
                    //acctList = (List<Account>)xDeserialize.Deserialize(stream);
                    stream.Close();
                    if (acctList.Count <= 0)
                    {
                        createAccount();
                    }
                }
            }
            catch (Exception e)
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
                Console.Clear();
                Console.WriteLine("\t ATM MENU");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1. Create a new account");
                Console.WriteLine("2. Remove an old account");
                Console.WriteLine("3. Make a transaction");
                Console.WriteLine("4. Exit");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Please choose one: ");
                str = Console.ReadLine();
                Console.Clear();

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
            Console.Clear();


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
                    Console.Clear();
                }
                else
                {
                    acctList.Add(new Account(100.00, aName));
                    Console.WriteLine("Account " + "|" + aName.ToUpper() + "|" + " was successfully created.");
                    Thread.Sleep(1 * 1000);
                    Console.Clear();
                }
                Console.WriteLine();
            }
        }

        public void removeAccount()
        {
            int acctNumber;
            string str;
            String accountName;

            if (acctList.Count > 0)
            {
                Console.WriteLine("CURRENT ACCOUNTS");
                Console.WriteLine("----------------");
                for (int i = 0; i < acctList.Count; i++)
                {
                    Console.WriteLine("Account " + (i + 1) + ": " + acctList[i].Acct.ToUpper());
                }

                Console.WriteLine("----------------");
                Console.WriteLine("Please enter the number of the account you want to remove or type " + "-1" + " to go back.");
                str = Console.ReadLine();
                Thread.Sleep(1 * 1000);
                Console.Clear();
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
                    if (acctNumber == -1)
                    {
                        //Exit back to the menu without removing accounts.
                    }
                    else if (acctNumber <= acctList.Count && acctNumber > 0)
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
                                Console.Clear();
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
                Console.Clear();
            }
        }

        public void makeTransaction()
        {
            int input;
            string str;

            if (acctList.Count > 0)
            {
                Console.WriteLine("CURRENT ACCOUNTS");
                Console.WriteLine("----------------");
                for (int i = 0; i < acctList.Count; i++)
                {
                    Console.WriteLine("Account " + (i + 1) + ": " + acctList[i].Acct.ToUpper());
                }

                Console.WriteLine("----------------");
                Console.WriteLine("Please enter the account number or " + "-1" + " to go back: ");
                str = Console.ReadLine();
                Thread.Sleep(1 * 1000);
                Console.Clear();
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
                    if (input == -1)
                    {
                        //Exit back to the menu without accessing accounts.
                    }
                    else if (input <= acctList.Count && input > 0)
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
                Console.Clear();
            }
        }

        public void writeAccount()
        {
            //string serializationFile = Path.Combine(dir);
            try
            {
                using (Stream stream = File.Create(dir))
                {
                    var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bFormatter.Serialize(stream, acctList);
                    //XmlSerializer xSerialize = new XmlSerializer(typeof(List<Account>));
                    //xSerialize.Serialize(stream, acctList);
                    stream.Flush();
                    stream.Close();
                }
                //using (Stream stream = File.Create("C:/Users/Chau/Desktop/test.xml"))
                //{
                //    XmlSerializer serializer = new XmlSerializer(typeof(List<Account>));
                //    serializer.Serialize(stream, acctList);
                //    stream.Close();
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
