using System;
using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "GaPoShOrgUnit")]
    public class RemoveGaPoShOrgUnit : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string OrgUnitPath;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var service = request.DirectoryService.Orgunits.Delete("my_customer", OrgUnitPath);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete OrgUnit!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}