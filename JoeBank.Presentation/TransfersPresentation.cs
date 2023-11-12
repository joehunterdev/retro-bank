using System;
using System.Collections.Generic;
using JoeBank.Entities;
using System.Linq;
using JoeBank.BusinessLogicLayer;
using JoeBank.BusinessLogicLayer.BALContracts;
namespace JoeBank.Presentation
{
    static class TransfersPresentation // make static to be easier to use in switch
    {
      
        internal static void AddTransfer()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {
                //Create BL Object
                ITransfersBusinessLogicLayer transfersBusinessLogicLayer = new TransfersBusinessLogicLayer();
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer   = new AccountsBusinessLogicLayer();

                Transfer transfer = new Transfer();

                //read all details from the user
                op.WriteLine("********ADD TRANSFER*************");

                //TODO: Return To/From Accounts as object 
                op.Write("From Acc No: ");
                long fromAccNum = long.Parse(Console.ReadLine());

                //Check to see if account exists
                Account fromAcc = accountsBusinessLogicLayer.GetAccountByCondition(a => a.AccountNumber == fromAccNum);

                if (fromAcc != null)
                {
                    transfer.FromAccount = fromAcc;

                    op.WriteLine("Account Holder found: " + fromAcc.AccountOwnerName);

                    op.Write("Which account number do you wish to transfer to ?: " );

                    long toAccNum = long.Parse(Console.ReadLine());

                    Account toAcc = accountsBusinessLogicLayer.GetAccountByCondition(a => a.AccountNumber == toAccNum);

                    if (toAcc != null)
                    {
                        transfer.ToAccount = toAcc;
                        transfer.FromAccountNumber = fromAccNum;
                        transfer.ToAccountNumber = toAccNum;

                        op.WriteLine("To account found: " + toAcc.AccountOwnerName);

                        op.Write("How much do you want to transfer ? :");

                         transfer.TransferAmount = double.Parse(Console.ReadLine());

                        var processed = transfersBusinessLogicLayer.ProcessTransfer(transfer);

                        if (processed)
                        {

                            op.WriteLine("Transfer Processed Sucessfully");

                        } else
                        {
                            op.WriteLine("We couldnt process that transfer");

                        }


                    } else
                    {
                        op.WriteLine("No To Account found with that Account Number");
                    }


                } else
                {
                  
                     op.WriteLine("No From Account found with that Account Number");

                }


            }
            catch (Exception ex)  
            {

                op.WriteLine(ex.Message); //caught in bll
                Console.WriteLine(ex.GetType());

                throw;
            }
             

        }

        internal static void ViewTransfers()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            op.WriteLine("********VIEW TRANSFERS*************");

            //Get date from user start/end
            //Init transfer bll
            //Call GetTransfers by condition (predicate should handle to and from range)
            //Output all transfers

            op.Write("Enter Start Date (dd/MM/yyyy):");

            string startDate = Console.ReadLine();
            DateTime validStartDate;
            while (!DateTime.TryParseExact(startDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out validStartDate))
            {
                op.WriteLine("Invalid start date, please retry");
                startDate = Console.ReadLine();
            }

            op.Write("Enter End Date (dd/MM/yyyy):");

            string endDate = Console.ReadLine();

            DateTime validEndDate;
            while (!DateTime.TryParseExact(endDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out validEndDate))
            {
                op.WriteLine("Invalid end date, please retry");
                endDate = Console.ReadLine();
            }

            try
            {
                ITransfersBusinessLogicLayer transfersBusinessLogicLayer = new TransfersBusinessLogicLayer();
                 List<Transfer> matchingTransfers = transfersBusinessLogicLayer.GetTransfersByCondition(t => t.TransferDate >= validStartDate && t.TransferDate <= validEndDate); //      Where(c => start <= c.end && end >= c.start) i.StartDate <= date && i.EndDate >= date

                if (matchingTransfers != null)
                {
                    int count = matchingTransfers.Count();

                    op.WriteLine($"Found {count} matching transfers from: {validStartDate} to {validEndDate} ");

                    foreach (var item in matchingTransfers)
                    {
                        //TODO: Fix date format coming out. Figure out why FromAccount and To account not coming out 
                        op.WriteLine($"Date: {item.TransferDate} - Amount: {item.TransferAmount} - From: {item.FromAccountNumber} - To: {item.ToAccountNumber}");
                        op.WriteLine("Acc" + item.FromAccount);
                        op.WriteLine(string.Join(", ", item));

                    }

                } else
                {

                    op.WriteLine($"No matching transfers from: {validStartDate} to {validEndDate} ");

                }
            }


            catch (Exception ex) //have to speciy type of exception
            {

                op.WriteLine(ex.Message); //caught in bll
                Console.WriteLine(ex.GetType());
                throw;
            }
        }


    }
}
