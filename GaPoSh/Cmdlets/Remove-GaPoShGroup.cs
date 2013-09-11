using System;
using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "GaPoShGroup")]
    public class RemoveGaPoShGroup : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var service = request.DirectoryService.Groups.Delete(GroupId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete Group!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}