using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Customer Id Number");
            string id = Console.ReadLine();
            Customer customer = new Customer();
            customer.GetCustomerByIdNumber(id);


            //customer.CreateCustomer();
            Console.ReadKey();
        }
       
    }
}
    

