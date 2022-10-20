using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Net;
using System.ServiceModel.Description;
using System.Configuration;

namespace CustomerTask
{
    static class ConnectionConfiguration
    {
        public static IOrganizationService ConnectToCrmOrganization()
        {
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string url = ConfigurationManager.AppSettings["OrgUrl"];
            IOrganizationService organizationService = null;
            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = username;
                clientCredentials.UserName.Password = password;

                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri(url), null, clientCredentials, null);
                if (organizationService != null)
                {
                    Guid gOrgId = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).OrganizationId;
                    if (gOrgId != Guid.Empty)
                    {
                        Console.WriteLine("Connection Established Successfully...");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to Established Connection!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured - " + ex.Message);
            }
            return organizationService;

        }
        public static IOrganizationService GetService(this IOrganizationService service)
        {
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string url = ConfigurationManager.AppSettings["OrgUrl"];
            IOrganizationService organizationService = null;
            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = username;
                clientCredentials.UserName.Password = password;

                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri(url), null, clientCredentials, null);
                if (organizationService != null)
                {
                    Guid gOrgId = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).OrganizationId;
                    if (gOrgId != Guid.Empty)
                    {
                        Console.WriteLine("Connection Established Successfully...");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to Established Connection!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured - " + ex.Message);
            }
            return organizationService;
        }
    }
}
