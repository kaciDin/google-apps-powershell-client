using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Groupssettings.v1;
using Google.Apis.Groupssettings.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShGroupSettings")]
    public class GetGaPoShGroupSettings : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = false)]
        public string groupId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.GroupSettingsService.Groups.Get(groupId);

            //Query Parameters
            service.Alt = GroupssettingsBaseServiceRequest<Groups>.AltEnum.Json;

            var settings = service.Execute();

            WriteObject(settings);
        }
    }
}