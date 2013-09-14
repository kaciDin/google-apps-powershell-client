using System;
using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShUserLicense")]
    public class GetGaPoShUserLicense : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = true)] public string ProductId;

        [Parameter(Mandatory = true)] public string SkuId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.LicensingService.LicenseAssignments.Get(ProductId, SkuId, UserId);

            var license = service.Execute();

            if (license == null) return;

            WriteObject(license);
        }
    }
}