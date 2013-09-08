using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShMobileDevice")]
    public class GetGaPoShMobileDevice : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DirectoryService.Mobiledevices.List("my_customer");

            //Query Parameters
            service.MaxResults = 500;
            service.OrderBy = MobiledevicesResource.ListRequest.OrderByEnum.Name;
            service.SortOrder = MobiledevicesResource.ListRequest.SortOrderEnum.ASCENDING;

            var mobileDevices = service.Execute();

            if (mobileDevices == null) return;

            Int64 totalresults = 0;
            var allMobileDevices = new List<MobileDevice>();

            //Single Page Results
            if (String.IsNullOrEmpty(mobileDevices.NextPageToken))
            {
                service.PageToken = mobileDevices.NextPageToken;

                allMobileDevices.AddRange(mobileDevices.MobiledevicesValue);

                totalresults = (totalresults + mobileDevices.MobiledevicesValue.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(mobileDevices.NextPageToken))
            {
                service.PageToken = mobileDevices.NextPageToken;

                allMobileDevices.AddRange(mobileDevices.MobiledevicesValue);

                totalresults = (totalresults + mobileDevices.MobiledevicesValue.Count);

                Console.Write(totalresults + "...");
                mobileDevices = service.Execute();

                if (String.IsNullOrEmpty(mobileDevices.NextPageToken))
                {
                    service.PageToken = mobileDevices.NextPageToken;

                    allMobileDevices.AddRange(mobileDevices.MobiledevicesValue);

                    totalresults = (totalresults + mobileDevices.MobiledevicesValue.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allMobileDevices);
        }
    }
}