using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShUserPhoto")]
    public class GetGaPoShUserPhoto : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = false)]
        public string UserId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DirectoryService.Users.Photos.Get(UserId);

            var userPhoto = service.Execute();

            WriteObject(userPhoto);
        }
    }
}