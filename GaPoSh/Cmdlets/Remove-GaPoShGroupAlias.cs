using System;
using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "GaPoShGroupAlias")]
    public class RemoveGaPoShGroupAlias : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

        [Parameter(Mandatory = true)]
        public string Alias;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var service = request.DirectoryService.Groups.Aliases.Delete(GroupId, Alias);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete Group Alias!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}