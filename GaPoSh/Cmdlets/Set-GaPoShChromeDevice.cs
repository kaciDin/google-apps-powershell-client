using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShChromeDevice")]
    public class SetGaPoShChromeDevice : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string DeviceId;

        [Parameter(Mandatory = false)]
        public string Projection;

        [Parameter(Mandatory = false)] public string AnnotatedLocation;

        [Parameter(Mandatory = false)] public string AnnotatedUser;

        [Parameter(Mandatory = false)] public string Notes;

        [Parameter(Mandatory = false)] public string OrgUnitPath;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var device = new ChromeOsDevice
                    {
                        AnnotatedLocation = String.IsNullOrEmpty(AnnotatedLocation) ? null : AnnotatedLocation,
                        AnnotatedUser = String.IsNullOrEmpty(AnnotatedUser) ? null : AnnotatedUser,
                        Notes = String.IsNullOrEmpty(Notes) ? null : Notes
                    };
                
                var service = request.DirectoryService.Chromeosdevices.Update(device, "my_customer", DeviceId);
                service.Projection = ChromeosdevicesResource.UpdateRequest.ProjectionEnum.FULL;
                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update Chrome Device!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}