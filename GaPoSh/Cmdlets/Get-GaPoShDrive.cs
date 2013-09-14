using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShDrive")]
    public class GetGaPoShDrive : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DriveService.About.Get();

            var driveRequest = service.Execute();

            WriteObject(driveRequest);
        }
    }
}