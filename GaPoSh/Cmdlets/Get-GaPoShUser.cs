using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Admin.Directory.directory_v1;


namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShUser" )]
    public class GetGaPoShUser : PSCmdlet
    {

        [Parameter(Mandatory = true)] public Instance Session;
        [Parameter(Mandatory = false)] public bool All;
        [Parameter(Mandatory = false)] public bool DomainOnly;
        [Parameter(Mandatory = false)] public bool IncludeDeleted;
        [Parameter(Mandatory = false)] public string Email;
        [Parameter(Mandatory = false)] public string GivenName;
        [Parameter(Mandatory = false)] public string FamilyName;
        
        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DirectoryService.Users.List();
            
            //Query Parameters
            //Search Single Domain or All Domains
            if (DomainOnly)
            {
                service.Domain = request.Domain;
            }
            else
            {
                service.Customer = "my_customer";
            }
            
            //Email?
            if (!String.IsNullOrEmpty(Email))
            {
                service.Query = "email:" + Email + "*";
            }

            //familyName?
            if (!String.IsNullOrEmpty(FamilyName))
            {
                service.Query = "familyName:" + FamilyName + "*";
            }

            //givenName?
            if (!String.IsNullOrEmpty(GivenName))
            {
                service.Query = "givenName:" + GivenName + "*";
            }
            
            //includeDeleted?
            service.ShowDeleted = IncludeDeleted ? "true" : "false";

            //All, Override Query Parameters
            if (All)
            {
                service.Query = null;
                service.ShowDeleted = "false";
                service.OrderBy = UsersResource.ListRequest.OrderByEnum.FamilyName;
                service.SortOrder = UsersResource.ListRequest.SortOrderEnum.ASCENDING;
                service.MaxResults = 500;
            }
            
            //No Parameters Defined
            if(All == false && service.ShowDeleted == "false" && service.Query == null)
            {
                Console.WriteLine("Error: Invalid Parameters!");
                Console.WriteLine("Specify -All, -ShowDeleted, -Email, -FamilyName, or -GivenName");
                return;
            }

            var users = service.Execute();

            if (users == null) return;
            
            Int64 totalresults = 0;
            var allUsers = new List<User>();

            //Single Page Results
            if (String.IsNullOrEmpty(users.NextPageToken))
            {
                service.PageToken = users.NextPageToken;

                allUsers.AddRange(users.UsersValue);

                totalresults = (totalresults + users.UsersValue.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(users.NextPageToken))
            {
                service.PageToken = users.NextPageToken;

                allUsers.AddRange(users.UsersValue);

                totalresults = (totalresults + users.UsersValue.Count);

                Console.Write(totalresults + "...");
                users = service.Execute();

                if (String.IsNullOrEmpty(users.NextPageToken))
                {
                    service.PageToken = users.NextPageToken;

                    allUsers.AddRange(users.UsersValue);

                    totalresults = (totalresults + users.UsersValue.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allUsers);
        }

    }
}