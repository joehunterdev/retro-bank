using System;
using JoeBank.Presentation;

class Program
{

    static void Main()
    {

        ConsoleOutputManager op = new ConsoleOutputManager();

        op.WriteLine("                           $$$$$\\                           $$$$$$$\\                      $$\\                               \r\n                           \\__$$ |                          $$  __$$\\                     $$ |                              \r\n                              $$ | $$$$$$\\   $$$$$$\\        $$ |  $$ | $$$$$$\\  $$$$$$$\\  $$ |  $$\\                         \r\n$$$$$$\\ $$$$$$\\ $$$$$$\\       $$ |$$  __$$\\ $$  __$$\\       $$$$$$$\\ | \\____$$\\ $$  __$$\\ $$ | $$  |$$$$$$\\ $$$$$$\\ $$$$$$\\ \r\n\\______|\\______|\\______|$$\\   $$ |$$ /  $$ |$$$$$$$$ |      $$  __$$\\  $$$$$$$ |$$ |  $$ |$$$$$$  / \\______|\\______|\\______|\r\n                        $$ |  $$ |$$ |  $$ |$$   ____|      $$ |  $$ |$$  __$$ |$$ |  $$ |$$  _$$<                          \r\n                        \\$$$$$$  |\\$$$$$$  |\\$$$$$$$\\       $$$$$$$  |\\$$$$$$$ |$$ |  $$ |$$ | \\$$\\                         \r\n                         \\______/  \\______/  \\_______|      \\_______/  \\_______|\\__|  \\__|\\__|  \\__|                        \r\n                                                                                                                            \r\n                                                                                                                 ");
        op.WriteLine("-----------Login---------");
        op.WriteLine("");


        string userName = null, password = null;

        op.Write("Username: ");
        //Console.SetCursorPosition(10, 10);

        userName = Console.ReadLine();


        if (userName != "")
        {
            op.Write("Password: ");

            password = Console.ReadLine();
        } 

        if (userName == "system" && password == "iloveyou" )
        {

            int mainMenuChoice = -1; // should be accessible outside do while block

            do
            {

                Console.WriteLine(" ");
                op.WriteLine(":::Main Menu:::");
                op.WriteLine("1. Customers");
                op.WriteLine("2. Accounts");
                op.WriteLine("3. Transfer ");
                op.WriteLine("8. Generate Some Data");

                op.WriteLine("0. Exit");

                op.Write("Enter Choice: ");

                mainMenuChoice = int.Parse(Console.ReadLine());

                switch (mainMenuChoice)
                {
                    case 1:
                        CustomerMenu();
                        break;
                    case 2:
                        AccountsMenu();
                        break;

                    case 3:
                        TransferMenu();
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 8:
                        FactoryPresentation.GenerateData();
                        break;
                    case 0:
                        break;
                }


            } while (mainMenuChoice != 0);
        }
        else
        {

            op.WriteLine("Invalid");

        }

        op.WriteLine("Invalid Credentials");
        Console.ReadKey();


        static void CustomerMenu()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            int customerMenuChoice = -1;

            do
            {
                Console.WriteLine(" ");

                op.WriteLine(":::Customer:::");
                op.WriteLine("1. Add");
                op.WriteLine("2. Delete");
                op.WriteLine("3. Edit");
                op.WriteLine("4. Search");
                op.WriteLine("5. View All");
                op.WriteLine("0. Exit");

                op.Write("Enter Choice: ");

                customerMenuChoice = Convert.ToInt32(Console.ReadLine().Trim()); // Two ways to convert to int

                switch (customerMenuChoice)
                {
                    case 1: CustomersPresentation.AddCustomer(); break;
                    case 2: CustomersPresentation.DeleteCustomer(); break; 
                    case 3: CustomersPresentation.EditCustomer(); break; 
                    case 5: CustomersPresentation.ViewCustomers(); break;  

                }

            } while (customerMenuChoice != 0);


        }

        static void AccountsMenu()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            int accountsMenuChoice = -1;

            do
            {
                Console.WriteLine(" ");
                op.WriteLine(":::Accounts:::");
                op.WriteLine("1. Add");
                op.WriteLine("2. Delete");
                op.WriteLine("3. Edit");
                op.WriteLine("4. Search");
                op.WriteLine("5. View All");
                op.WriteLine("6. Statement");

                op.WriteLine("0. Exit");

                op.Write("Enter Choice: ");

                //accountsMenuChoice = int.Parse(Console.ReadLine().Trim());
                accountsMenuChoice = Convert.ToInt32(Console.ReadLine().Trim()); // Two ways to convert to int

                switch (accountsMenuChoice)
                {
                    case 1: AccountsPresentation.AddAccount(); break;
                    case 2: AccountsPresentation.DeleteAccount(); break; //need to make collection static for this to work
                    case 3: AccountsPresentation.EditAccount(); break; 
                    case 5: AccountsPresentation.ViewAccounts(); break; 
                    case 6: AccountsPresentation.ViewStatement(); break; 

                }


            } while (accountsMenuChoice != 0);

        }



        static void TransferMenu()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            int transfersMenuChoice = -1;

            do
            {
                Console.WriteLine(" ");
                op.WriteLine(":::Transfer:::");
                op.WriteLine("1. Add Transfer");
                op.WriteLine("2. View Transfers");
                op.WriteLine("0. Exit");
                op.Write("Enter Choice: ");

                transfersMenuChoice = Convert.ToInt32(Console.ReadLine().Trim());

                switch (transfersMenuChoice)
                {
                    case 1: TransfersPresentation.AddTransfer(); break;
                    case 2: TransfersPresentation.ViewTransfers(); break;

                }


            } while (transfersMenuChoice != 0);

        }

    }

}