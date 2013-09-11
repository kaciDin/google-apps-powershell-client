using System;
using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "GaPoShUserPhoto")]
    public class RemoveGaPoShUserPhoto : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

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
                var service = request.DirectoryService.Users.Photos.Delete(UserId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete User Photo!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}