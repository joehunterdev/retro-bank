using System;
using System.Collections.Generic;
using JoeBank.Entities;
using JoeBank.BusinessLogicLayer;
using JoeBank.BusinessLogicLayer.BALContracts;
namespace JoeBank.Presentation
{
    static class CustomersPresentation // make static to be easier to use in switch
    {

        //create an object of customer
        internal static void AddCustomer()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {
                Customer customer = new Customer();
                //read all details from the user
                op.WriteLine("********ADD CUSTOMER*************");
                op.Write("Customer Name: ");
                customer.CustomerName = Console.ReadLine();
                op.Write("Address: ");
                customer.Address = Console.ReadLine();
                op.Write("Landmark: ");
                customer.Landmark = Console.ReadLine();
                op.Write("City: ");
                customer.City = Console.ReadLine();
                op.Write("Country: ");
                customer.Country = Console.ReadLine();
                op.Write("Mobile: ");
                customer.Mobile = Console.ReadLine();

                //Create BL Object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                Guid newGuid = customersBusinessLogicLayer.AddCustomer(customer); //returns new guid

                //long customerCode = customer.CustomerCode; // always reutrns 1001 need to do logic as per below


                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerID == newGuid);

                if (matchingCustomers.Count >= 1)
                {
                    op.WriteLine("New Customer Code: " + matchingCustomers[0].CustomerCode);
                    op.WriteLine("Customer Added.\n");
                }
                else
                {
                    op.WriteLine("Customer Not added");
                }
                //op.WriteLine("Customer Added with Id: " + customerCode); // will return back to the top

            }
            catch (Exception ex) //have to speciy type of exception
            {
                op.WriteLine(ex.Message); //caught in bll
                Console.WriteLine(ex.GetType());
                throw;
            }
   
            //exceptions need to be caugh

        }

        internal static void ViewCustomers()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {
                // Create BL Object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                //List of type customers assign to allCustomers is equal to the Bl Object we created above access method
                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();

                op.WriteLine("**********ALL CUSTOMERS*************");

                foreach (var item in allCustomers)
                {

                    op.WriteLine("Customer Code: " + item.CustomerCode);
                    op.WriteLine("Customer Name: " + item.CustomerName);
                    op.WriteLine("Address: " + item.Address);
                    op.WriteLine("Landmark: " + item.Landmark);
                    op.WriteLine("City: " + item.City);
                    op.WriteLine("Country: " + item.Country);
                    op.WriteLine("Mobile: " + item.Mobile);
                    op.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                op.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
                throw;
            }

            //read all customers in loop and print the output

        }

        internal static void EditCustomer()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {

                op.WriteLine("**********UPDATE CUSTOMER AREA*************");


                // Create BL Object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                op.WriteLine("**********Enter a customer code*************");

                long customerCode = Convert.ToInt32(Console.ReadLine());

                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerCode == customerCode);
 

                ////Find customer 
                if (matchingCustomers.Count >= 1)
                {
                    op.WriteLine("Customer Found : " + matchingCustomers[0].CustomerName);

                    op.WriteLine("**********UPDATE CUSTOMER*************");
                    op.Write("Customer Name: ");
                    matchingCustomers[0].CustomerName = Console.ReadLine();
                    op.Write("Address: ");
                    matchingCustomers[0].Address = Console.ReadLine();
                    op.Write("Landmark: ");
                    matchingCustomers[0].Landmark = Console.ReadLine();
                    op.Write("City: ");
                    matchingCustomers[0].City = Console.ReadLine();
                    op.Write("Country: ");
                    matchingCustomers[0].Country = Console.ReadLine();
                    op.Write("Mobile: ");
                    matchingCustomers[0].Mobile = Console.ReadLine();

                    if (customersBusinessLogicLayer.UpdateCustomer(matchingCustomers[0])) {

                        op.WriteLine("Customer Updated Successfully");

                    }  


                }
                else {

                    op.WriteLine("No Customer found");
                }


            }
            catch (Exception ex)
            {
                op.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
                throw;
            }

        }

        internal static void DeleteCustomer()
        {
            ConsoleOutputManager op = new ConsoleOutputManager();

            try
            {

                op.Write("**********DELETE CUSTOMER AREA*************");


                // Create BL Object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                op.Write("**********Enter a customer code*************");

                long customerCode = Convert.ToInt32(Console.ReadLine());

                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerCode == customerCode);


                ////Find customer 
                if (matchingCustomers.Count >= 1 && customersBusinessLogicLayer.DeleteCustomer(matchingCustomers[0].CustomerID))
                {
                    
                   op.WriteLine("Customer Deleted Successfully");

                }
                else
                {

                    op.WriteLine("No Customer found");
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
