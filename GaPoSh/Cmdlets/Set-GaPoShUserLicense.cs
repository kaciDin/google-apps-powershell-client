using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Licensing.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserLicense")]
    public class SetGaPoShUserLicense : PSCmdlet
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
            var userLicense = new LicenseAssignmentInsert
                {
                    UserId = UserId
                };

            try
            {
                var service = request.LicensingService.LicenseAssignments.Insert(userLicense, ProductId, SkuId);

                var license = service.Execute();

                if (license == null) return;

                WriteObject(license);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User License!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}