using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Admin.Directory.directory_v1;


namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShGroup" )]
    public class GetGaPoShGroup : PSCmdlet
    {

        [Parameter(Mandatory = true)] public Instance Session;
        [Parameter(Mandatory = false)] public string UserId;
        [Parameter(Mandatory = false)] public bool DomainOnly;
        
        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DirectoryService.Groups.List();
            
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

            service.MaxResults = 500;

            //Query by Member
            if (!String.IsNullOrEmpty(UserId))
            {
                service.UserKey = UserId;
            }

            var groups = service.Execute();
            
            if (groups == null) return;
            
            Int64 totalresults = 0;
            var allGroups = new List<Group>();

            //Single Page Results
            if (String.IsNullOrEmpty(groups.NextPageToken))
            {
                service.PageToken = groups.NextPageToken;

                allGroups.AddRange(groups.GroupsValue);

                totalresults = (totalresults + groups.GroupsValue.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(groups.NextPageToken))
            {
                service.PageToken = groups.NextPageToken;

                allGroups.AddRange(groups.GroupsValue);

                totalresults = (totalresults + groups.GroupsValue.Count);

                Console.Write(totalresults + "...");
                groups = service.Execute();

                if (String.IsNullOrEmpty(groups.NextPageToken))
                {
                    service.PageToken = groups.NextPageToken;

                    allGroups.AddRange(groups.GroupsValue);

                    totalresults = (totalresults + groups.GroupsValue.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allGroups);
        } 

    }
}