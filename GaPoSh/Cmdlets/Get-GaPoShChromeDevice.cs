using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Admin.Directory.directory_v1;


namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShChromeDevice" )]
    public class GetGaPoShChromeDevice : PSCmdlet
    {

        [Parameter(Mandatory = true)] public Instance Session;
        
        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DirectoryService.Chromeosdevices.List("my_customer");
            
            //Query Parameters
            service.MaxResults = 500;
            service.OrderBy = ChromeosdevicesResource.ListRequest.OrderByEnum.SerialNumber;
            service.SortOrder = ChromeosdevicesResource.ListRequest.SortOrderEnum.ASCENDING;

            var chromeDevices = service.Execute();
            
            if (chromeDevices == null) return;
            
            Int64 totalresults = 0;
            var allChromeDevices = new List<ChromeOsDevice>();

            //Single Page Results
            if (String.IsNullOrEmpty(chromeDevices.NextPageToken))
            {
                service.PageToken = chromeDevices.NextPageToken;

                allChromeDevices.AddRange(chromeDevices.ChromeosdevicesValue);

                totalresults = (totalresults + chromeDevices.ChromeosdevicesValue.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(chromeDevices.NextPageToken))
            {
                service.PageToken = chromeDevices.NextPageToken;

                allChromeDevices.AddRange(chromeDevices.ChromeosdevicesValue);

                totalresults = (totalresults + chromeDevices.ChromeosdevicesValue.Count);

                Console.Write(totalresults + "...");
                chromeDevices = service.Execute();

                if (String.IsNullOrEmpty(chromeDevices.NextPageToken))
                {
                    service.PageToken = chromeDevices.NextPageToken;

                    allChromeDevices.AddRange(chromeDevices.ChromeosdevicesValue);

                    totalresults = (totalresults + chromeDevices.ChromeosdevicesValue.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allChromeDevices);
        } 

    }
}