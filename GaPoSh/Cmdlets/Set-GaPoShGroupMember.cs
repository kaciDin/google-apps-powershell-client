using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShGroupMember")]
    public class SetGaPoShGroupMember : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

        [Parameter(Mandatory = false)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public string Role;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var member = new Member
                    {
                        Role = String.IsNullOrEmpty(Role) ? null : Role
                    };

                var service = request.DirectoryService.Members.Update(member, GroupId, UserId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update Group Member!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}