using System;
using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "GaPoShGroupMember")]
    public class RemoveGaPoShGroupMember : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

        [Parameter(Mandatory = true)]
        public string UserId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var service = request.DirectoryService.Members.Delete(GroupId, UserId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete Group Member!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}