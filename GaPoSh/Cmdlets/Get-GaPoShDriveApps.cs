using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShDriveApps")]
    public class GetGaPoShDriveApps : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DriveService.Apps.List();

            var apps = service.Execute();

            WriteObject(apps);
        }
    }
}