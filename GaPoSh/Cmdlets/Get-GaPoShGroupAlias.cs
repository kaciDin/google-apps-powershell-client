using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShGroupAlias")]
    public class GetGaPoShGroupAlias : PSCmdlet
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
            var service = request.DirectoryService.Groups.Aliases.List(GroupId);

            var aliases = service.Execute();

            if (aliases == null) return;

            WriteObject(aliases);
        }
    }
}