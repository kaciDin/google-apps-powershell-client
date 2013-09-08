using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "GaPoShGroupMember")]
    public class NewGaPoShGroupMember : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = true)]
        public string Role;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var member = new Member {Email = null, Role = null};

                var service = request.DirectoryService.Members.Insert(member, GroupId);
                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Add Group Member!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}