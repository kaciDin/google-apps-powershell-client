using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Admin.Directory.directory_v1;


namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShGroupMember" )]
    public class GetGaPoShGroupMember : PSCmdlet
    {

        [Parameter(Mandatory = true)] public Instance Session;
        [Parameter(Mandatory = true)] public string GroupId;
        [Parameter(Mandatory = false)] public string Roles;
        [Parameter(Mandatory = false)] public bool DomainOnly;
        
        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {   
            var service = request.DirectoryService.Members.List(GroupId);
            
            //Query Parameters
            service.MaxResults = 500;
            service.Roles = Roles;

            var members = service.Execute();
            
            if (members == null) return;
            
            Int64 totalresults = 0;
            var allMembers = new List<Member>();

            //Single Page Results
            if (String.IsNullOrEmpty(members.NextPageToken))
            {
                service.PageToken = members.NextPageToken;

                allMembers.AddRange(members.MembersValue);

                totalresults = (totalresults + members.MembersValue.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(members.NextPageToken))
            {
                service.PageToken = members.NextPageToken;

                allMembers.AddRange(members.MembersValue);

                totalresults = (totalresults + members.MembersValue.Count);

                Console.Write(totalresults + "...");
                members = service.Execute();

                if (String.IsNullOrEmpty(members.NextPageToken))
                {
                    service.PageToken = members.NextPageToken;

                    allMembers.AddRange(members.MembersValue);

                    totalresults = (totalresults + members.MembersValue.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allMembers);
        } 

    }
}