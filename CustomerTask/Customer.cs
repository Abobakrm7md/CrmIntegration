using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace CustomerTask
{
    public class Customer
    {
        #region Customer prop
        public string FirestName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public int Title { get; set; }
        #endregion

        #region Create customer
        public void CreateCustomer()
        {
            try
            {
                IOrganizationService OrgService = ConnectionConfiguration.ConnectToCrmOrganization();
                Entity newCustomer = IntializeCustomer();
                Guid customerId = OrgService.Create(newCustomer);
                Console.WriteLine("Customer created with id : " + customerId);

                Console.Write("Are you need to retrive entity? [y/n] ");
                ConsoleKey response = Console.ReadKey(false).Key;
                Console.WriteLine();
                if (response == ConsoleKey.Y)
                {
                    Entity customer = OrgService.Retrieve("contact", customerId, new ColumnSet("ph_idnumber"));
                    GetCustomerByIdNumber(customer[CustomerEntity.IdNumber].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Entity IntializeCustomer()
        {
            Entity customer = new Entity("contact");
            customer[CustomerEntity.Fullname] = "Abobakr Mohammed Ahmed-a";
            customer[CustomerEntity.FirstName] = "abobakr";
            customer[CustomerEntity.LastName] = "Mohammed Ahmed";
            customer[CustomerEntity.IdNumber] = "501236547896541";
            customer[CustomerEntity.Phone] = "01236547896";
            customer[CustomerEntity.Title] = new OptionSetValue(881980000);
            customer[CustomerEntity.Email] = "a2@a.com";
            customer[CustomerEntity.BirthDate] = new DateTime(1993, 12, 23);
            customer[CustomerEntity.Nationality] = new EntityReference("ph_nationality" , new Guid("ce2b26ee-9fbb-eb11-b836-005056aa850a"));
            return customer;

        }
        #endregion

        #region Get Customer
        public void GetCustomerByIdNumber(string IdNumber)
        {
            IOrganizationService OrgService = null;//= IOrganizationService//ConnectionConfiguration.ConnectToCrmOrganization();
            OrgService =  OrgService.GetService();
            QueryExpression query = new QueryExpression();
            query.EntityName = "contact";
            query.Criteria.AddCondition(new ConditionExpression("ph_idnumber", ConditionOperator.Equal, IdNumber));
            query.ColumnSet = new ColumnSet(CustomerEntity.Fullname, CustomerEntity.FirstName,
                CustomerEntity.LastName, CustomerEntity.Email,
                CustomerEntity.Nationality, CustomerEntity.BirthDate,
                CustomerEntity.Phone, CustomerEntity.IdNumber,
                CustomerEntity.Title);
            EntityCollection customers = OrgService.RetrieveMultiple(query);
            Entity customerEntity = customers[0];

            Customer customer = SerializeCustomerData(customerEntity);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(customer.ToString());
        }
        private static Customer SerializeCustomerData(Entity customerEntity)
        {
            Customer customer = new Customer();

            customer.FullName = customerEntity[CustomerEntity.Fullname] != null ? 
                customerEntity[CustomerEntity.Fullname].ToString() : "Not Exist";
            customer.FirestName = customerEntity[CustomerEntity.FirstName] != null ? 
                customerEntity[CustomerEntity.FirstName].ToString() : "Not Exist";
            customer.LastName = customerEntity[CustomerEntity.LastName] != null ? 
                customerEntity[CustomerEntity.LastName].ToString() : "Not Exist" ;
            customer.Email = customerEntity[CustomerEntity.Email] != null ? 
                customerEntity[CustomerEntity.Email].ToString() : "Not Exist";
            EntityReference nationalityEntityReferance = (EntityReference)customerEntity.Attributes[CustomerEntity.Nationality];
            customer.Nationality = nationalityEntityReferance.Name;
            customer.BirthDate = customerEntity[CustomerEntity.BirthDate] != null ? 
                (DateTime)customerEntity[CustomerEntity.BirthDate] : new DateTime();
            customer.IdNumber = customerEntity[CustomerEntity.IdNumber] != null ? 
                customerEntity[CustomerEntity.IdNumber].ToString() : "Not Exist";
            customer.PhoneNumber = customerEntity[CustomerEntity.Phone] != null ? 
                customerEntity[CustomerEntity.Phone].ToString() : "Not Exist";
            OptionSetValue optionSet = (OptionSetValue)customerEntity.Attributes[CustomerEntity.Title];
            customer.Title = optionSet.Value;
            return customer;
        }
        public override string ToString()
        {
            string customer = $"FullName = {this.FullName} \n " +
                $"FirstName = {this.FirestName} -- LastName = {this.LastName} \n" +
                $"IdNumber = {this.IdNumber} \n " +
                $"Email = {this.Email} \n" +
                $"Nationality = {this.Nationality} \n" +
                $"MobilePhone = {this.PhoneNumber} \n " +
                $"BirthDate = {this.BirthDate} \n" +
                $"Title = {this.Title}";
            return customer;
        }
       enum CustomerColumnsName
        {
            fullname  ,
            firstname ,
            lastname,
            emailaddress1,
            ph_nationalityref,
            birthdate,
            ph_idnumber,
            mobilephone,
            ph_ellakab
        }
        #endregion

    }
}
