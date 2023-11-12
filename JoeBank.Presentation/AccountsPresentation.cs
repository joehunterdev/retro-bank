using System;
using System.Collections.Generic;
using JoeBank.Entities;
using JoeBank.BusinessLogicLayer;
using JoeBank.BusinessLogicLayer.BALContracts;
namespace JoeBank.Presentation
{
    static class AccountsPresentation // make static to be easier to use in switch
    {
        //create an object of account
        internal static void AddAccount()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {
                Account account = new Account();

                //read all details from the user
                op.WriteLine("********ADD ACCOUNT*************");
                op.Write("Account Holder Name:"); //should do a check here against existing names
                account.AccountOwnerName = Console.ReadLine();
                op.Write("Account Pin Number:");
                account.AccountPin = Convert.ToInt32(Console.ReadLine());
                op.Write("Account type: credit, debit, savings ? ");
                account.AccountType = Console.ReadLine();

                //Create BL Object
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();
                Guid newGuid = accountsBusinessLogicLayer.AddAccount(account); //returns new guid

                List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountID == newGuid);

    

                if (matchingAccounts.Count >= 1)
                {
                    op.WriteLine("Account Added.\n");
                    op.WriteLine("New Account Number created: " + matchingAccounts[0].AccountNumber);
                }
                else
                {
                    op.WriteLine("Account Not added");
                }
                //op.WriteLine("Account Added with Id: " + accountCode); // will return back to the top

            }
            catch (Exception ex) //have to speciy type of exception
            {
                Console.WriteLine(ex.Message); //caught in bll
                Console.WriteLine(ex.GetType());

             }
   
            //exceptions need to be caugh

        }

        internal static void ViewAccounts()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {
                // Create BL Object
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                //List of type accounts assign to allAccounts is equal to the Bl Object we created above access method
                List<Account> allAccounts = accountsBusinessLogicLayer.GetAccounts();

                op.WriteLine("**********ALL ACCOUNTS*************");

                foreach (var item in allAccounts)
                {

                    op.WriteLine("Account Number: " + item.AccountNumber);
                    op.WriteLine("Account Holder Name: " + item.AccountOwnerName);
                    op.WriteLine("Account Type: " + item.AccountType);
                    op.WriteLine("Account Balance: " + item.AccountBalance);

                    if (item.Transfers != null)
                    {
                        op.WriteLine("Total Transfers: " + item.Transfers.Count);

                        foreach (var item2 in item.Transfers)
                        {
                            op.WriteLine("Transfer: Amount " + item2.TransferAmount + " Date:" + item2.TransferDate);

                        }
                    }


                    op.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                op.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
                throw;
            }

            //read all accounts in loop and print the output

        }

        internal static void EditAccount()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {

                op.Write("**********UPDATE ACCOUNT*************");

                // Create BL Object
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                op.Write("Enter a account number:");

                long accountCode = Convert.ToInt32(Console.ReadLine());

                List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountNumber == accountCode);
 

                ////Find account 
                if (matchingAccounts.Count >= 1)
                {
                    op.WriteLine("Account : " + matchingAccounts[0].AccountNumber + " found");
                    op.WriteLine("Update Details: ");
                    op.Write("Account Owner Name: ");
                    matchingAccounts[0].AccountOwnerName = Console.ReadLine();
                    op.Write("Account Type: ");
                    matchingAccounts[0].AccountType = Console.ReadLine();
  
                    if (accountsBusinessLogicLayer.UpdateAccount(matchingAccounts[0])) {

                        op.WriteLine("Account Updated Successfully");

                    }  


                }
                else {

                    op.WriteLine("No Account found");
                }


            }
            catch (Exception ex)
            {
                op.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
                throw;
            }

        }

        internal static void DeleteAccount()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {

                op.WriteLine("**********DELETE ACCOUNT AREA*************");


                // Create BL Object
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                op.Write("Enter a account code:");

                long accountCode = Convert.ToInt32(Console.ReadLine());

                List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountNumber == accountCode);


                ////Find account 
                if (matchingAccounts.Count >= 1 && accountsBusinessLogicLayer.DeleteAccount(matchingAccounts[0].AccountID))
                {
                    
                   op.WriteLine("Account Deleted Successfully");

                }
                else
                {

                    op.WriteLine("No Account found");
                }


            }
            catch (Exception ex)
            {
                op.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
                throw;
            }

        }

        internal static void ViewStatement()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {

                op.WriteLine("**********ACCOUNT STATEMENT*************");

                // Create BL Object
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                op.Write("Enter a account number:");

                long accNumber = Convert.ToInt32(Console.ReadLine());

                var matchingAccount = accountsBusinessLogicLayer.GetAccountByCondition(item => item.AccountNumber == accNumber);


                // Account found 
                if (matchingAccount!= null)
                {
    
                    op.WriteLine("Account Number: " + matchingAccount.AccountNumber);
                    op.WriteLine("Account Holder Name: " + matchingAccount.AccountOwnerName);
                    op.WriteLine("Account Type: " + matchingAccount.AccountType);
                    op.WriteLine("Account Balance: " + matchingAccount.AccountBalance);

                    if (matchingAccount.Transfers != null)
                    {
                        op.WriteLine("Total Transfers: " + matchingAccount.Transfers.Count);

                        foreach (var item in matchingAccount.Transfers)
                        {
                           // op.WriteLine("Transfer: Amount " + item.TransferAmount + " Date:" + item.TransferDate);
                            op.WriteLine($"Date: {item.TransferDate} - Amount: {item.TransferAmount} - From: {item.FromAccountNumber} - To: {item.ToAccountNumber}");


                        }
                    } else
                    {

                        op.WriteLine("No Transfers found");
                    }



                }
                else
                {

                    op.WriteLine("No Account found");
                }


            }
            catch (Exception ex)
            {
                op.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
                throw;
            }

        }


    }
}
